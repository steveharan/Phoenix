(function (app) {
    'use strict';

    app.controller('personRelationshipsCtrl', personRelationshipsCtrl);

    personRelationshipsCtrl.$inject = ['$scope', '$rootScope', '$uibModalInstance', '$uibModal', '$routeParams', 'apiService', 'notificationService'];

    function personRelationshipsCtrl($scope, $rootScope, $uibModalInstance, $uibModal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-persons';
        $scope.loadingPersonRelationships = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Persons = [];
        $scope.Relationships = [];
        $scope.RelationshipID = 0;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.updatePersonRelationships = updatePersonRelationships;
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
            console.log('scope on add item');
            console.log($scope);
            $scope.Relationships.push({
                relationWithPersonId: $scope.selectedPersonId,
                personId: $scope.$parent.EditedPerson.ID,
                RelationshipName: $scope.selectedPersonName,
                RelationshipTypeId: $scope.data.selectedOption.id,
                RelationshipTypeName: $scope.data.selectedOption.name
            });
            $scope.selectedPersonName = "";
            $scope.RelationshipID = "";
            $scope.data.selectedOption.id = "0";
            $scope.$broadcast('angucomplete-alt:clearInput');
        }

        function updatePersonLoadFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }

        function updatePersonRelationships() {
            console.log('$scope.Relationships');
            console.log($scope.Relationships);
            apiService.post('/api/personRelationships/createall', $scope.Relationships,
            updatePersonRelationshipCompleted,
            updatePersonRelationshipFailed);
        }

        function updatePersonRelationshipCompleted(response) {
            console.log($scope);
            notificationService.displaySuccess('Success');
            $uibModalInstance.dismiss();
        }

        function updatePersonRelationshipFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }

        function search(page) {
            page = page || 0;

            $scope.loadingPersonRelationships = true;

            apiService.get('/api/personRelationships/' + $scope.$parent.EditedPerson.ID, null,
            personRelationshipsLoadCompleted,
            personRelationshipsLoadFailed);
        }

        function personRelationshipsLoadCompleted(result) {
            console.log('personRelationshipsLoadCompleted - result');
            console.log(result.data);
            $scope.Relationships = result.data;
            console.log('$scope.Relationships');
            console.log($scope.Relationships);

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingPersonRelationships = false;

            if ($scope.filterPersons && $scope.filterPersons.length) {
                notificationService.displayInfo(result.data.Items.length + ' people found');
            }
        }

        function personRelationshipsLoadFailed(response) {
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