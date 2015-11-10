(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService', '$mdDialog'];

    function customersCtrl($scope, $modal, apiService, notificationService, $mdDialog) {

        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Customers = [];

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

            $scope.loadingCustomers = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterCustomers
                }
            };

            apiService.get('/api/customers/search/', config,
            customersLoadCompleted,
            customersLoadFailed);
        }

        $scope.openDialog = function (ev) {
            $mdDialog.show({
                controller: DialogController,
                templateUrl: 'scripts/spa/customers/CreateSchoolyearDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true
            })
                .then(function (schoolyear) {
                    $scope.status = 'You changed the name to "' + schoolyear.name + '".';
                }, function () {
                    $scope.status = 'You cancelled the dialog.';
                });
        }

        function DialogController($scope, $mdDialog) {
            $scope.schoolyear = { name: 'bastienJS 2014-2015', startDate: new Date(), endDate: new Date() };
            $scope.hide = function () {
                $mdDialog.hide();
            };
            $scope.cancel = function () {
                $mdDialog.cancel();
            };
            $scope.save = function () {
                // Make ajax call here with .then
                // (
                $mdDialog.hide($scope.schoolyear);
                // ); so the dialog closes when ajax call is done!
            };
        }

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;
            $modal.open({
                templateUrl: 'scripts/spa/customers/editCustomerModal.html',
                controller: 'customerEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function customersLoadCompleted(result) {
            $scope.Customers = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingCustomers = false;

            if ($scope.filterCustomers && $scope.filterCustomers.length) {
                notificationService.displayInfo(result.data.Items.length + ' customers found');
            }

        }

        function customersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterCustomers = '';
            search();
        }

        $scope.search();
    }

})(angular.module('phoenix'));