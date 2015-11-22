(function (app) {
    'use strict';

    app.controller('personRelationshipsCtrl', personRelationshipsCtrl);

    personRelationshipsCtrl.$inject = ['$scope', '$rootScope', '$uibModalInstance', '$uibModal', '$routeParams', 'apiService', 'notificationService'];

    function personRelationshipsCtrl($scope, $rootScope, $uibModalInstance, $uibModal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-persons';
        $scope.loadingPersons = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Persons = [];
        $scope.Relationships = [];
        $scope.RelationshipID = 0;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.updatePerson = updatePerson;
        $scope.deletePerson = deletePerson;
        $scope.cancel = cancel;
        $scope.addItem = addItem;

        $scope.data = {
            availableOptions: [
              { id: '0', name: '-- Select --' },
              { id: '1', name: 'Father' },
              { id: '2', name: 'Mother' },
            ],
            selectedOption: { id: '0', name: '-- Select --' } 
        };

        // To store the selected parents of this person that were typed in using auto complete.
        $scope.selectedPersonId = -1;
        $scope.selectedPerson = selectedPerson;

        function selectedPerson($item) {
            if ($item) {
                $scope.selectedPersonId = $item.originalObject.ID;
                $scope.selectedPersonName = $item.title;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedPersonId = -1;
                $scope.isEnabled = false;
            }
        }

        function addItem(index) {
            $scope.Relationships.push({
                relationshipPersonId: $scope.selectedPersonId,
                relationshipName: $scope.selectedPersonName,
                relationshipTypeId: $scope.data.selectedOption.id,
                relationshipTypeName: $scope.data.selectedOption.name
            });
            $scope.selectedPersonName = "";
            $scope.RelationshipID = "";
            $scope.data.selectedOption.id = "0";
            $scope.$broadcast('angucomplete-alt:clearInput');
        }

        $scope.showTableFormat = true;

        $scope.toggleView = function () {
            $scope.showTableFormat = $scope.showTableFormat === true ? false : true;
        };

        function search(page) {
            page = page || 0;

            $scope.loadingPersons = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterPersons
                }
            };

            apiService.get('/api/persons/search/' + $routeParams.id, config,
            personsLoadCompleted,
            personsLoadFailed);
        }

        function updatePersonLoadFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }

        function deletePerson(person) {
            person.deleted = true;
            openEditDialog(person);
        }

        function updatePerson(person) {
            if (person == null) {
                $scope.newPerson = true;
                person = {};
            }
            else {
                $scope.newPerson = false;
            }
            person.deleted = false;
            openEditDialog(person);
        }

        function openEditDialog(person) {
            $scope.EditedPerson = person;
            $uibModal.open({
                templateUrl: 'scripts/spa/persons/personEditModal.html',
                controller: 'personEditCtrl',
                backdrop: 'static',
                scope: $scope,
                keyboard: 'true',
                windowClass: 'app-modal-window'
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
                clearSearch();
            });
        }

        function personsLoadCompleted(result) {
            $scope.Persons = result.data.Items;
            console.log('personloadcomplete');
            console.log($scope.Persons);
            $scope.FamilyName = $scope.Persons[0].FamilyName;
            console.log($scope);

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingPersons = false;

            if ($scope.filterPersons && $scope.filterPersons.length) {
                notificationService.displayInfo(result.data.Items.length + ' people found');
            }
        }

        function personsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterPersons = '';
            search();
        }

        function cancel() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        $scope.search();

    }
})(angular.module('phoenix'));