﻿(function (app) {
    'use strict';

    app.controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$modal', '$routeParams', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function personEditCtrl($scope, $modal, $routeParams, $modalInstance, $timeout, apiService, notificationService) {
        $scope.addOrEdit = setLable();
        function setLable() {
            if ($scope.EditedPerson.deleted) {
                return 'PLEASE CONFIRM DELETE FOR ';
            }
            else {
                if ($scope.newPerson) {
                    $scope.EditedPerson = {};
                    return 'Add New';
                }
                else {
                    return 'Edit';
                }
            }
        }
        $scope.cancelEdit = cancelEdit;
        $scope.updatePerson = updatePerson;
        $scope.GetDiagnosisSubType = GetDiagnosisSubType;

        $scope.openDatePicker = openDatePicker;
        $scope.openDatePicker2 = openDatePicker2;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};
        $scope.datepicker2 = {};
        $scope.ethnicities = [];

        $scope.myDate = new Date();

        function ethnicitiesLoadCompleted(response) {
            console.log(response.data);
            $scope.ethniticies = response.data;
        }

        function ethnicitiesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function loadEthnicities() {
            apiService.get("/api/data/ethnicity", null,
                ethnicitiesLoadCompleted,
                ethnicitiesLoadFailed)
        }

        function diagnosesLoadCompleted(response) {
            console.log(response.data);
            $scope.diagnoses = response.data;
        }

        function diagnosesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function diagnosesSubTypeLoadCompleted(response) {
            console.log(response.data);
            $scope.diagnosesSubType = response.data;
        }

        function diagnosesSubTypeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function loadDiagnoses() {
            apiService.get("/api/data/diagnosis", null,
                diagnosesLoadCompleted,
                diagnosesLoadFailed);

            GetDiagnosisSubType();
        }

        function loadData() {
            loadEthnicities();
            loadDiagnoses();
        }

        function GetDiagnosisSubType() {

            var config = {
                params: {
                    diagnosisId: $scope.EditedPerson.DiagnosisID
                }
            };

            apiService.get("/api/data/diagnosisSubType", config,
                diagnosesSubTypeLoadCompleted,
                diagnosesSubTypeLoadFailed)
        }

        function updatePerson() {
            if (!$scope.newPerson) {
                apiService.post('/api/persons/update/', $scope.EditedPerson,
                updatePersonCompleted,
                updatePersonLoadFailed);
            }
            else {
                $scope.EditedPerson.FamilyID = $routeParams.id;
                console.log('create person - editedperson is');
                console.log($scope.EditedPerson);
                console.log('create person - editedperson above');
                apiService.post('/api/persons/create', $scope.EditedPerson,
                updatePersonCompleted,
                updatePersonLoadFailed);
            }
        }

        function updatePersonCompleted(response) {
            console.log('updatePersonCompleted Scope');
            console.log($scope);
            if ($scope.EditedPerson.deleted) {
                notificationService.displaySuccess($scope.EditedPerson.FirstName + ' deleted');
            }
            else {
                notificationService.displaySuccess($scope.EditedPerson.FirstName + ' updated');
            }
            $scope.EditedPerson = {};
            $modalInstance.dismiss();
        }

        function updatePersonLoadFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker2.opened = false;

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);
        };

        function openDatePicker2($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = false;

            $timeout(function () {
                $scope.datepicker2.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

        loadData();
    }

})(angular.module('phoenix'));