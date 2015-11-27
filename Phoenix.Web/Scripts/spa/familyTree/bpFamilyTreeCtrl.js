﻿(function(app) {
    'use strict';

    app.controller('bpFamilyTreeCtrl', bpFamilyTreeCtrl);

    bpFamilyTreeCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', 'apiService', 'notificationService', '$location'];

    function bpFamilyTreeCtrl($scope, $rootScope, $uibModal, $routeParams, apiService, notificationService, $location) {

        $scope.index = 10;
        $scope.Message = "";
        $scope.addrelation = addrelation;
 
        function addrelation(personId) {
            // load the person
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
                clearSearch();
            }, function () {
                clearSearch();
            });
        }


        var options = {};
 
        var items = [
            new primitives.famdiagram.ItemConfig({
                id: 0,
                title: "Scott Aasrud",
                description: "Root",
                phone: "1 (416) 001-4567",
                email: "scott.aasrud@mail.com",
                image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
             new primitives.famdiagram.ItemConfig({
                 id: 10,
                 title: "Scott Aasrud 2",
                 description: "Root",
                 phone: "1 (416) 001-4567",
                 email: "scott.aasrud@mail.com",
                 image: "demo/images/photos/a.png",
                 itemTitleColor: primitives.common.Colors.RoyalBlue
             }),
            new primitives.famdiagram.ItemConfig({
                id: 1,
                parents: [0, 10],
                title: "Ted Lucas",
                description: "Left",
                phone: "1 (416) 002-4567",
                email: "ted.lucas@mail.com",
                image: "demo/images/photos/b.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 2,
                parents: [0, 10],
                title: "Joao Stuger",
                description: "Right",
                phone: "1 (416) 003-4567",
                email: "joao.stuger@mail.com",
                image: "demo/images/photos/c.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 3,
                parents: [2],
                title: "Hidden Node",
                phone: "1 (416) 004-4567",
                email: "hidden.node@mail.com",
                description: "Dotted Node",
                image: "demo/images/photos/e.png",
                itemTitleColor: primitives.common.Colors.PaleVioletRed
            })
        ];
 
        options.items = items;
        options.cursorItem = 0;
        options.highlightItem = 0;
        options.hasSelectorCheckbox = primitives.common.Enabled.True;
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
 
        $scope.addItem = function (index, parent) {
            var id = $scope.index++;
            $scope.myOptions.items.splice(index, 0, new primitives.famdiagram.ItemConfig({
                id: id,
                parent: parent,
                title: "New title " + id,
                description: "New description " + id,
                image: "demo/images/photos/b.png"
            }));
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
                + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 2px; width: 50px; height: 60px;">'
                    + '<img name="photo" src="{{itemConfig.image}}" style="height: 60px; width:50px;" />'
                + '</div>'
                + '<div name="phone" class="bp-item" style="top: 26px; left: 56px; width: 162px; height: 18px; font-size: 12px;">{{itemConfig.phone}}</div>'
                + '<div class="bp-item" style="top: 44px; left: 56px; width: 162px; height: 18px; font-size: 12px;">'
                + '<a name="email" href="mailto::{{itemConfig.email}}" target="_top">{{itemConfig.email}}</a></div>'
                + '<div name="description" class="bp-item" style="top: 62px; left: 56px; width: 162px; height: 36px; font-size: 10px;">{{itemConfig.description}}</div>'
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
                    /* destroy items scopes */
                    for (var index = 0; index < scopes.length; index++) {
                        itemScopes[index].$destroy();
                    }

                    /* destory jQuery UI widget instance */
                    chart.remove();
                });
            };

            return {
                scope: {
                    options: '=options',
                    onCursorChanged: '&onCursorChanged',
                    onHighlightChanged: '&onHighlightChanged',
                },
                link: link
            };
        });
    });




})(angular.module('phoenix'));
