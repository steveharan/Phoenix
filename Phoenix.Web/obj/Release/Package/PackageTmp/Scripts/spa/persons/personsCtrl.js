(function (app) {
    'use strict';

    app.controller('personsCtrl', personsCtrl);

    personsCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', 'apiService', 'notificationService', '$location'];

    function personsCtrl($scope, $rootScope, $uibModal, $routeParams, apiService, notificationService, $location) {
        $scope.pageClass = 'page-persons';
        $scope.loadingPersons = true;
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
            }, function () {
                clearSearch();
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
            //$rootScope.itemsHard = [
            //    new primitives.famdiagram.ItemConfig({
            //        id: 0,
            //        title: "Scott Aasrud",
            //        description: "Root",
            //        phone: "1 (416) 001-4567",
            //        email: "scott.aasrud@mail.com",
            //        image: "demo/images/photos/a.png",
            //        itemTitleColor: primitives.common.Colors.RoyalBlue
            //    }),
            //     new primitives.famdiagram.ItemConfig({
            //         id: 10,
            //         title: "Scott Aasrud 2",
            //         description: "Root",
            //         phone: "1 (416) 001-4567",
            //         email: "scott.aasrud@mail.com",
            //         image: "demo/images/photos/a.png",
            //         itemTitleColor: primitives.common.Colors.RoyalBlue
            //     }),
            //    new primitives.famdiagram.ItemConfig({
            //        id: 1,
            //        parents: [0, 10],
            //        title: "Ted Lucas",
            //        description: "Left",
            //        phone: "1 (416) 002-4567",
            //        email: "ted.lucas@mail.com",
            //        image: "demo/images/photos/b.png",
            //        itemTitleColor: primitives.common.Colors.RoyalBlue
            //    }),
            //    new primitives.famdiagram.ItemConfig({
            //        id: 2,
            //        parents: [0, 10],
            //        title: "Joao Stuger",
            //        description: "Right",
            //        phone: "1 (416) 003-4567",
            //        email: "joao.stuger@mail.com",
            //        image: "demo/images/photos/c.png",
            //        itemTitleColor: primitives.common.Colors.RoyalBlue
            //    }),
            //    new primitives.famdiagram.ItemConfig({
            //        id: 3,
            //        parents: [2],
            //        title: "Hidden Node",
            //        phone: "1 (416) 004-4567",
            //        email: "hidden.node@mail.com",
            //        description: "Dotted Node",
            //        image: "demo/images/photos/e.png",
            //        itemTitleColor: primitives.common.Colors.PaleVioletRed
            //    })
            //];

            //            loadTree();
            $location.path("/familyTree/" + $scope.FamilyId);

            //$location.path("/familyTree/" + $scope.FamilyId);
        }

        function loadTree() {
                apiService.get('/api/personRelationships/getfamilytree/' + $routeParams.id, null,
                familyTreeLoadCompleted,
                familyTreeLoadFailed);
        }

        function familyTreeLoadCompleted(result) {
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
                    gender: value.gender
                });
                apiItems.push(apiItem);
            });
            $rootScope.items = apiItems;
            console.log('load complete, tree is:');
            console.log($rootScope.items);
        }

        function familyTreeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.search();
        $scope.loadTree();

    }
})(angular.module('phoenix'));