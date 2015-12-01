(function(app) {
    'use strict';

    app.controller('bpFamilyTreeCtrl', bpFamilyTreeCtrl);

    bpFamilyTreeCtrl.$inject = ['$scope', '$rootScope', '$uibModal'];

    function bpFamilyTreeCtrl($scope, $rootScope, $uibModal) {

        $scope.index = 10;
        $scope.Message = "";
        $scope.addChild = addChild;
        $scope.addParent = addParent;
        $scope.deletePerson = deletePerson;

        function deletePerson(personId) {
            alert('delete person ' + personId);
        }

        function addChild(personId) {
            alert('addChild');

            $scope.addingChild = true;
            $scope.addingParent= false;

            // pass the personid that we are adding a person to
            $scope.addRelationToPersonId = personId;
            var person = "";
            $scope.EditedPerson = person;
            $scope.newPerson = true;
            $uibModal.open({
                templateUrl: 'scripts/spa/persons/personEditModal.html',
                controller: 'personEditCtrl',
                backdrop: 'static',
                scope: $scope,
                windowClass: 'app-modal-window'
            }).result.then(function ($scope) {
            }, function () {
                if ($rootScope.NewlyCreatedPerson != null) {
                    var id = $rootScope.NewlyCreatedPerson.PersonID;
                    var index = $scope.myOptions.items.length + 1;
                    var objParent = [];
                    var objSpouse = [];
                    if ($rootScope.NewlyCreatedPerson.RelationshipTypeId !== 3) {
                        var jsonParent = '[' + personId + ']';
                        objParent = angular.fromJson(jsonParent);
                    } else {
                        var jsonSpouse = '[' + personId + ']';
                        objSpouse = angular.fromJson(jsonSpouse);
                    }

                    $scope.myOptions.items.splice(index, 0, new primitives.famdiagram.ItemConfig({
                        id: id,
                        parents: objParent,
                        spouses: objSpouse,
                        title: $rootScope.NewlyCreatedPerson.FirstName + ' ' + $rootScope.NewlyCreatedPerson.SurName,
                        description: $rootScope.NewlyCreatedPerson.Notes
                    }));
                }
            });
        }

        function addParent(personId) {
            alert('addparent');
            $scope.addingParent = true;
            $scope.addingChild = false;
            // pass the personid that we are adding a person to
            $scope.addRelationToPersonId = personId;
            var person = "";
            $scope.EditedPerson = person;
            $scope.newPerson = true;
            $uibModal.open({
                templateUrl: 'scripts/spa/persons/personEditModal.html',
                controller: 'personEditCtrl',
                backdrop: 'static',
                scope: $scope,
                windowClass: 'app-modal-window'
            }).result.then(function ($scope) {
            }, function () {
                if ($rootScope.NewlyCreatedPerson != null) {
                    var id = $rootScope.NewlyCreatedPerson.PersonID;
                    var index = $scope.myOptions.items.length + 1;
                    var objParent = [];
                    var objSpouse = [];
                    if ($rootScope.NewlyCreatedPerson.RelationshipTypeId != 3) {
                        var jsonParent = '[' + personId + ']';
                        objParent = angular.fromJson(jsonParent);
                    } else {
                        var jsonSpouse = '[' + personId + ']';
                        objSpouse = angular.fromJson(jsonSpouse);
                    }

                    $scope.myOptions.items.splice(index, 0, new primitives.famdiagram.ItemConfig({
                        id: id,
                        parents: objParent,
                        spouses: objSpouse,
                        title: $rootScope.NewlyCreatedPerson.FirstName + ' ' + $rootScope.NewlyCreatedPerson.SurName,
                        description: $rootScope.NewlyCreatedPerson.Notes
                    }));
                }
            });
        }

        var options = {};

        options.items = $rootScope.items;
        options.cursorItem = 0;
        options.highlightItem = 0;
        options.hasSelectorCheckbox = primitives.common.Enabled.False;
        options.templates = [getContactTemplate()];
        options.defaultTemplateName = "contactTemplate";
 
        $scope.myOptions = options;
 
        $scope.setCursorItem = function (item) {
            $scope.myOptions.cursorItem = item;
        };
 
        $scope.setHighlightItem = function (item) {
            $scope.myOptions.highlightItem = item;
        };
 
        $scope.deleteItem = function (index) {
            $scope.myOptions.items.splice(index, 1);
        }
 
        $scope.onMyCursorChanged = function () {
            $scope.Message = "onMyCursorChanged";
        }
 
        $scope.onMyHighlightChanged = function () {
            $scope.Message = "onMyHighlightChanged";
        }
 
        function getContactTemplate() {
            var result = new primitives.famdiagram.TemplateConfig();
            result.name = "contactTemplate";
 
            result.itemSize = new primitives.common.Size(220, 120);
            result.minimizedItemSize = new primitives.common.Size(5, 5);
            result.minimizedItemCornerRadius = 5;
            result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);
 
 
            var itemTemplate = jQuery(
              '<div class="bp-item bp-corner-all bt-item-frame">'
                + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="background:{{itemTitleColor}};top: 2px; left: 2px; width: 216px; height: 20px;">'
                    + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 208px; height: 18px;">{{itemConfig.title}}</div>'
                + '</div>'
                + '<div name="deceased" class="bp-item" style="top: 26px; left: 6px; width: 162px; height: 18px; font-size: 12px;">Deceased: {{itemConfig.deceased}}</div>'
                + '<div name="gender" class="bp-item" style="top: 44px; left: 6px; width: 162px; height: 18px; font-size: 12px;">Gender: {{itemConfig.gender}}</div>'
                + '<div name="dob" class="bp-item" style="top: 62px; left: 6px; width: 162px; height: 18px; font-size: 12px;">DOB: {{itemConfig.dob | date:"mediumDate"}}</div>'
                + '<div name="reg" class="bp-item" style="top: 80px; left: 6px; width: 162px; height: 18px; font-size: 12px;">Registered: {{itemConfig.registered | date:"mediumDate"}}</div>'
            + '</div>'
            ).css({
                width: result.itemSize.width + "px",
                height: result.itemSize.height + "px"
            }).addClass("bp-item bp-corner-all bt-item-frame");
            result.itemTemplate = itemTemplate.wrap('<div>').parent().html();
 
            return result;
        }
    }

    angular.module('BasicPrimitives', [], function ($compileProvider) {
        $compileProvider.directive('bpFamDiagram', function ($compile) {
            function link(scope, element, attrs) {
                var itemScopes = [];

                var config = new primitives.famdiagram.Config();
                angular.extend(config, scope.options);

                config.onItemRender = onTemplateRender;
                config.onCursorChanged = onCursorChanged;
                config.onHighlightChanged = onHighlightChanged;

                var chart = jQuery(element).famDiagram(config);

                scope.$watch('options.highlightItem', function (newValue, oldValue) {
                    var highlightItem = chart.famDiagram("option", "highlightItem");
                    if (highlightItem != newValue) {
                        chart.famDiagram("option", { highlightItem: newValue });
                        chart.famDiagram("update", primitives.famdiagram.UpdateMode.PositonHighlight);
                    }
                });

                scope.$watch('options.cursorItem', function (newValue, oldValue) {
                    var cursorItem = chart.famDiagram("option", "cursorItem");
                    if (cursorItem != newValue) {
                        chart.famDiagram("option", { cursorItem: newValue });
//                        chart.famDiagram("update", primitives.famdiagram.UpdateMode.Refresh);
                    }
                });

                scope.$watchCollection('options.items', function (items) {
                    chart.famDiagram("option", { items: items });
//                    chart.famDiagram("update", primitives.famdiagram.UpdateMode.Refresh);
                });

                function onTemplateRender(event, data) {
                    var itemConfig = data.context;

                    switch (data.renderingMode) {
                        case primitives.common.RenderingMode.Create:
                            /* Initialize widgets here */
                            var itemScope = scope.$new();
                            itemScope.itemConfig = itemConfig;
                            $compile(data.element.contents())(itemScope);
                            if (!scope.$parent.$$phase) {
                                itemScope.$apply();
                            }
                            itemScopes.push(itemScope);
                            break;
                        case primitives.common.RenderingMode.Update:
                            /* Update widgets here */
                            var itemScope = data.element.contents().scope();
                            itemScope.itemConfig = itemConfig;
                            break;
                    }
                }

                function onButtonClick(e, data) {
                    scope.onButtonClick();
                    scope.$apply();
                }

                function onCursorChanged(e, data) {
                    scope.options.cursorItem = data.context ? data.context.id : null;
                    scope.onCursorChanged();
                    scope.$apply();
                }

                function onHighlightChanged(e, data) {
                    scope.options.highlightItem = data.context ? data.context.id : null;
                    scope.onHighlightChanged();
                    scope.$apply();
                }

                element.on('$destroy', function () {
                    ///* destroy items scopes */
                    ////console.log('length is ' + itemScopes.length);
                    //for (var index = 0; index < itemScopes.length; index++) {
                    //    itemScopes[index].$destroy();
                    //}

                    ///* destory jQuery UI widget instance */
                    //chart.remove();
                });
            };

            return {
                scope: {
                    options: '=options',
                    onCursorChanged: '&onCursorChanged',
                    onHighlightChanged: '&onHighlightChanged'
                },
                link: link
            };
        });
    });




})(angular.module('phoenix'));
