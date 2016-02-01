(function (app) {
    'use strict';

    app.controller('testsCtrl', testsCtrl);

    testsCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', 'apiService', 'notificationService', '$location'];

    function testsCtrl($scope, $rootScope, $uibModal, $routeParams, apiService, notificationService, $location) {

        $scope.pageClass = 'page-tests';
        $scope.loadingPersons = true;
        $scope.loadingTree = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Persons = [];
        var apiItem = "";
        var apiItems = [];
        var objParents = "";
        var objSpouses = "";

        $scope.search = search;
        $scope.loadTree = loadTree;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.updatePerson = updatePerson;
        $scope.deletePerson = deletePerson;
        $scope.manageRelations = manageRelations;
        $scope.callFamilyTree = callFamilyTree;
        $scope.tests = tests;

        $scope.data = {
            availableOptions: [
              { id: '0', name: '-- Select --' },
              { id: '1', name: 'Father' },
              { id: '2', name: 'Mother' },
              { id: '3', name: 'Spouse' }
            ],
            selectedOption: { id: '0', name: '-- Select --' }
        };

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
            notificationService.displayError(response.data);
        }

        function tests(person) {
            $location.path('/tests/' + person.id);
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

        function manageRelations(person) {
            $scope.EditedPerson = person;
            $uibModal.open({
                templateUrl: 'scripts/spa/personRelationships/personRelationshipsModal.html',
                controller: 'personRelationshipsCtrl',
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
                $scope.loadTree();
            }, function () {
                clearSearch();
                $scope.loadTree();
            });
        }

        function personsLoadCompleted(result) {
            $scope.Persons = result.data.Items;
            $scope.FamilyName = $scope.Persons[0].FamilyName;
            $scope.FamilyId = $scope.Persons[0].FamilyID;

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

        function callFamilyTree(person) {
            $location.path("/familyTree/" + $scope.FamilyId);
        }

        function loadTree() {
                $scope.loadingTree = true;
                apiService.get('/api/personRelationships/getfamilytree/' + $routeParams.id, null,
                familyTreeLoadCompleted,
                familyTreeLoadFailed);
        }

        function familyTreeLoadCompleted(result) {
            console.log('tree load complete');
            console.log(result.data.Items);
            $scope.loadingTree = false;
            apiItems = [];
            angular.forEach(result.data.Items, function (value, key) {
                objParents = angular.fromJson(value.parents);
                objSpouses = angular.fromJson(value.spouses);
                var colour = "";
                if (value.gender == 'F') {
                    colour = primitives.common.Colors.LavenderBlush;
                } else {
                    colour = primitives.common.Colors.RoyalBlue;
                }

                apiItem = new primitives.famdiagram.ItemConfig({
                    id: value.id,
                    title: value.title,
                    description: value.description,
                    parents: objParents,
                    spouses: objSpouses,
                    itemTitleColor: colour,
                    groupTitle: value.gender,
                    groupTitleColor: colour,
                    deceased: value.deceased,
                    gender: value.gender,
                    dob: value.dateOfBirth,
                    registered: value.firstRegisteredDate
                });
                apiItems.push(apiItem);
            });
            $rootScope.items = apiItems;
        }

        function familyTreeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.search();
        $scope.loadTree();

    }
})(angular.module('phoenix'));