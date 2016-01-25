(function (app) {
    'use strict';
    app.controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function personEditCtrl($scope, $rootScope, $uibModal, $routeParams, $uibModalInstance, $timeout, apiService, notificationService) {
        $scope.addOrEdit = setLable();
        function setLable() {
            console.log($scope);
            if ($scope.EditedPerson.deleted) {
                return 'PLEASE CONFIRM DELETE FOR ';
            }
            else {
                if ($scope.newPerson) {
                    $scope.EditedPerson = {};
                    $scope.EditedPerson.FamilyID = $routeParams.id;
                    return 'Add New Family Member';
                }
                else {
                    return 'Edit';
                }
            }
        }
        $scope.cancelEdit = cancelEdit;
        $scope.updatePerson = updatePerson;
        $scope.GetDiagnosisSubType = GetDiagnosisSubType;

        if ($scope.EditedPerson.Gender == 'M')
        {
            $scope.GenderName = 'Male';
        }
        else
        {
            $scope.GenderName = 'Female';
        }

        $scope.data = {
            availableOptions: [
              { id: '0', name: '-- Select --' },
              { id: 'M', name: 'Male' },
              { id: 'F', name: 'Female' },
            ],
            selectedOption: { id: $scope.EditedPerson.Gender, name: $scope.GenderName }
        };

        $scope.relations = {
            availableOptions: [
              { id: '0', name: '-- Select --' },
              { id: '1', name: 'Father' },
              { id: '2', name: 'Mother' },
              { id: '3', name: 'Spouse' }
            ],
            selectedOption: { id: '0', name: '-- Select --' }
        };

        $scope.openDatePicker = openDatePicker;
        $scope.openDatePicker2 = openDatePicker2;
        $scope.openDatePicker3 = openDatePicker3;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.datepicker = {};
        $scope.datepicker2 = {};
        $scope.datepicker3 = {};
        $scope.ethnicities = [];

        // To store the selected parents of this person that were typed in using auto complete.
        $scope.selectedFatherId = -1;
        $scope.selectedMotherId = -1;
        $scope.selectedMother = selectedMother;
        $scope.selectedFather = selectedFather;

        $scope.myDate = new Date();

        function selectedMother($item) {
            if ($item) {
                $scope.selectedMotherId = $item.originalObject.ID;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedMotherId = -1;
                $scope.isEnabled = false;
            }
        }

        function selectedFather($item) {
            if ($item) {
                $scope.selectedFatherId = $item.originalObject.ID;
                $scope.isEnabled = true;
            }
            else {
                $scope.selectedFatherId = -1;
                $scope.isEnabled = false;
            }
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
                ethnicitiesLoadFailed);
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

        function loadRelatedPerson(personId) {
            apiService.get("/api/persons/details/" + personId, null,
                relatedPersonLoadCompleted,
                relatedPersonLoadFailed);
        }

        function relatedPersonLoadCompleted(response) {
            $scope.RelatedPerson = response.data;
        }

        function relatedPersonLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function loadData() {
            if ($scope.addRelationToPersonId != null) {
                loadRelatedPerson($scope.addRelationToPersonId);
            }
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
                diagnosesSubTypeLoadFailed);
        }

        function updatePerson() {
            if (!$scope.newPerson) {
                $scope.EditedPerson.Gender = $scope.data.selectedOption.id,
                apiService.post('/api/persons/update', $scope.EditedPerson,
                updatePersonCompleted,
                updatePersonLoadFailed);
            }
            else {
                var now = new Date();
                $scope.EditedPerson.FamilyID = $routeParams.id;
                $scope.EditedPerson.DateDeceased = now;
                $scope.EditedPerson.Gender = $scope.data.selectedOption.id;
                if ($scope.addRelationToPersonId != null) {
                    if ($scope.addingChild) {
                        apiService.post('/api/persons/create/' + $scope.addRelationToPersonId + '/' + $scope.relations.selectedOption.id, $scope.EditedPerson,
                            updatePersonCompleted,
                            updatePersonLoadFailed);
                    } else {
                        apiService.post('/api/persons/createparent/' + $scope.addRelationToPersonId + '/' + $scope.relations.selectedOption.id, $scope.EditedPerson,
                            updatePersonCompleted,
                            updatePersonLoadFailed);
                    }
                } else {
                    apiService.post('/api/persons/create/', $scope.EditedPerson,
                        updatePersonCompleted,
                        updatePersonLoadFailed);

                };
            }
        }

        function updatePersonCompleted(response) {
            if ($scope.EditedPerson.deleted) {
                notificationService.displaySuccess($scope.EditedPerson.FirstName + ' deleted');
            }
            else {
                notificationService.displaySuccess($scope.EditedPerson.FirstName + ' updated');
            }
            //$scope.EditedPerson = {};
            $rootScope.NewlyCreatedPerson = $scope.EditedPerson;
            $rootScope.NewlyCreatedPerson.PersonID = response.data.ID;
            $rootScope.NewlyCreatedPerson.RelationshipTypeId = $scope.relations.selectedOption.id;
            $uibModalInstance.dismiss();
        }

        function updatePersonLoadFailed(response) {
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
            $scope.datepicker2.opened = false;

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[uib-datepicker-popup-wrap]').css('z-index', '10000');
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
                $('ul[uib-datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

        function openDatePicker3($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.datepicker.opened = false;

            $timeout(function () {
                $scope.datepicker3.opened = true;
            });

            $timeout(function () {
                $('ul[uib-datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

        loadData();
    }

})(angular.module('phoenix'));