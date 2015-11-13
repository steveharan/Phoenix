﻿(function (app) {
    'use strict';

    app.controller('customersRegCtrl', customersRegCtrl);

    customersRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService', 'notificationService'];

    function customersRegCtrl($scope, $location, $rootScope, apiService, notificationService) {

        $scope.newCustomer = {};
        $scope.Register = Register;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.submission = {
        };

        function Register() {
           apiService.post('/api/customers/register', $scope.newCustomer,
           registerCustomerSucceded,
           registerCustomerFailed);
        }

        function registerCustomerSucceded(response) {
            console.log(response);
            var customerRegistered = response.data;
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newCustomer.LastName + ' has been successfully registed');
            $scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
            notificationService.displaySuccess('Customer added successfully');
            $location.path('/customers');
            //$scope.newCustomer = {};
        }

        function registerCustomerFailed(response) {
            console.log(response);
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };
    }

})(angular.module('phoenix'));