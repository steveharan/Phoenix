(function (app) {
    'use strict';

    app.controller('familiesCtrl', familiesCtrl);

    familiesCtrl.$inject = ['$scope', '$rootScope', '$modal', 'apiService', 'notificationService', '$location'];

    function familiesCtrl($scope, $rootScope, $modal, apiService, notificationService, $location) {
        $scope.pageClass = 'page-families';
        $scope.loadingFamilies = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Families = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.updateFamily = updateFamily;
        $scope.deleteFamily = deleteFamily;
        $scope.callPersons = callPersons;
        $scope.selectedFamily = {};
        $scope.showTableFormat = false;

        $scope.toggleView = function () {
            $scope.showTableFormat = $scope.showTableFormat === false ? true : false;
        };

        function callPersons(family) {
            $location.path("/persons/" + family.ID);
        }

        function search(page) {
            page = page || 0;

            $scope.loadingFamilies = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterFamilies
                }
            };

            apiService.get('/api/families/search/', config,
            familiesLoadCompleted,
            familiesLoadFailed);
        }

        function updateFamilyLoadFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }

        function deleteFamily(family) {
            family.deleted = true;
            openEditDialog(family);
        }

        function updateFamily(family) {
            if (family == null) {
                $scope.newFamily = true;
                family = {};
            }
            else {
                $scope.newFamily = false;
            }
            family.deleted = false;
            openEditDialog(family);
        }

        function openEditDialog(family) {
            $scope.EditedFamily = family;
            $modal.open({
                templateUrl: 'scripts/spa/families/familyEditModal.html',
                controller: 'familyEditCtrl',
                backdrop: 'static',
                scope: $scope,
                windowClass: 'app-modal-window'
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
                clearSearch();
            });
        }

        function familiesLoadCompleted(result) {
            $scope.Families = result.data.Items;
            console.log($scope.Families);

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingFamilies = false;

            if ($scope.filterFamilies && $scope.filterFamilies.length) {
                notificationService.displayInfo(result.data.Items.length + ' families found');
            }

        }

        function familiesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterFamilies = '';
            search();
        }

        $scope.search();

    }


})(angular.module('phoenix'));