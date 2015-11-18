(function (app) {
    'use strict';

    app.controller('personsCtrl', personsCtrl);

    personsCtrl.$inject = ['$scope', '$rootScope', '$modal', '$routeParams', 'apiService', 'notificationService'];

    function personsCtrl($scope, $rootScope, $modal, $routeParams, apiService, notificationService) {
        $scope.pageClass = 'page-persons';
        $scope.loadingPersons = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Persons = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.updatePerson = updatePerson;
        $scope.deletePerson = deletePerson;

        $scope.showTableFormat = false;

        $scope.toggleView = function () {
            $scope.showTableFormat = $scope.showTableFormat === false ? true : false;
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
            $modal.open({
                templateUrl: 'scripts/spa/persons/personEditModal.html',
                controller: 'personEditCtrl',
                backdrop: 'static',
                scope: $scope,
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

        $scope.search();

    }
})(angular.module('phoenix'));