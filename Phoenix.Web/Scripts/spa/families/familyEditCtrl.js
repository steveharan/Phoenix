﻿(function (app) {
    'use strict';

    app.controller('familyEditCtrl', familyEditCtrl);

    familyEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function familyEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {
        $scope.addOrEdit = setLable();

        function setLable() {
            if ($scope.newFamily) {
                return 'Add New';
            }
            else {
                return 'Edit';
            }
        }

        $scope.cancelEdit = cancelEdit;
        $scope.updateFamily = updateFamily;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.myDate = new Date();

        function loadData() {
            apiService.get("/api/ethnicity/list", null,
                ethnicityLoadCompleted,
                ethnicityLoadFailed)
        }

        function ethnicityLoadCompleted(result) {
            console.log(result.data);
            $scope.ethnicities = result.data;
        }

        function ethnicityLoadFailed(result) {
            notificationService.displayError(response.data);
        }

        function moviesLoadCompleted(result) {
            $scope.latestMovies = result.data;
            $scope.loadingMovies = false;
        }


        function updateFamily() {
            console.log($scope.EditedFamily);
            if (!$scope.newFamily) {
                apiService.post('/api/families/update/', $scope.EditedFamily,
                updateFamilyCompleted,
                updateFamilyLoadFailed);
            }
            else {
                apiService.post('/api/families/create', $scope.EditedFamily,
                updateFamilyCompleted,
                updateFamilyLoadFailed);
            }
        }

        function updateFamilyCompleted(response) {
            notificationService.displaySuccess('The ' + $scope.EditedFamily.FamilyName + ' family has been updated');
            $scope.Editedfamily = {};
            $modalInstance.dismiss();
        }

        function updateFamilyLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

        loadData();
    }

})(angular.module('phoenix'));