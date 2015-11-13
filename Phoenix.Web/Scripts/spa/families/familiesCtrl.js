﻿(function (app) {
    'use strict';

    app.controller('familiesCtrl', familiesCtrl);

    familiesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function familiesCtrl($scope, $modal, apiService, notificationService) {

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

        $scope.showTableFormat = false;

        $scope.toggleView = function () {
            $scope.showTableFormat = $scope.showTableFormat === false ? true : false;
        };

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

        function openEditDialog(family) {
            console.log('Editing...');
            console.log(family);
            if (family == null) {
                $scope.newFamily = true;
            }
            else {
                $scope.newFamily = false;
            }
            $scope.EditedFamily = family;
            $modal.open({
                templateUrl: 'scripts/spa/families/familyEditModal.html',
                controller: 'familyEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function familiesLoadCompleted(result) {
            $scope.Families = result.data.Items;

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