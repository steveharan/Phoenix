(function (app) {
    'use strict';

    app.controller('customerEditCtrl', customerEditCtrl);

    customerEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function customerEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.addOrEdit = setLable();

        function setLable() {
            if ($scope.newCustomer) {
                return 'Add New Customer';
            }
            else {
                return 'Edit';
            }
        }

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.myDate = new Date();

        function updateCustomer() {
            console.log($scope.EditedCustomer);
            if (!$scope.newCustomer) {
                apiService.post('/api/customers/update/', $scope.EditedCustomer,
                updateCustomerCompleted,
                updateCustomerLoadFailed);
            }
            else {
                apiService.post('/api/customers/register', $scope.EditedCustomer,
                updateCustomerCompleted,
                updateCustomerLoadFailed);
            }
        }

        function updateCustomerCompleted(response) {
            notificationService.displaySuccess($scope.EditedCustomer.FirstName + ' ' + $scope.EditedCustomer.LastName + ' has been updated');
            $scope.EditedCustomer = {};
            $uibModalInstance.dismiss();
        }

        function updateCustomerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[uib-datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

    }

})(angular.module('phoenix'));