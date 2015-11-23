(function (app) {
    'use strict';

    app.controller('familyEditCtrl', familyEditCtrl);

    familyEditCtrl.$inject = ['$scope', '$uibModal', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function familyEditCtrl($scope, $uibModal, $uibModalInstance, $timeout, apiService, notificationService) {
        $scope.addOrEdit = setLable();
        function setLable() {
            if ($scope.EditedFamily.deleted) {
                return 'PLEASE CONFIRM DELETE FOR THE';
            }
            else {
                if ($scope.newFamily) {
                    $scope.EditedFamily = {};
                    return 'Add New Family';
                }
                else {
                    return 'Edit';
                }
            }
        }
        $scope.cancelEdit = cancelEdit;
        $scope.updateFamily = updateFamily;
        $scope.GetDiagnosisSubType = GetDiagnosisSubType;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};
        $scope.ethnicities = [];
        $scope.openPersonDialog = openPersonDialog;

        $scope.myDate = new Date();

        function openPersonDialog(family) {
            $scope.EditedFamily = family;
            $uibModal.open({
                templateUrl: 'scripts/spa/families/familyPersonsModal.html',
                controller: 'familyPersonsCtrl',
                backdrop: 'static',
                scope: $scope,
                windowClass: 'app-modal-window'
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
                clearSearch();
            });
        }

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
                    diagnosisId: $scope.EditedFamily.DiagnosisID
                }
            };

            apiService.get("/api/data/diagnosisSubType", config,
                diagnosesSubTypeLoadCompleted,
                diagnosesSubTypeLoadFailed)
        }

        function updateFamily() {
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
            console.log('updateFamilyCompleted Scope');
            console.log($scope);
            if ($scope.EditedFamily.deleted) {
                notificationService.displaySuccess('The ' + $scope.EditedFamily.FamilyName + ' family has been deleted');
            }
            else {
                notificationService.displaySuccess('The ' + $scope.EditedFamily.FamilyName + ' family has been updated');
            }
            $scope.EditedFamily = {};
            $uibModalInstance.dismiss();
        }

        function updateFamilyLoadFailed(response) {
            console.log(response);
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

        loadData();
    }

})(angular.module('phoenix'));