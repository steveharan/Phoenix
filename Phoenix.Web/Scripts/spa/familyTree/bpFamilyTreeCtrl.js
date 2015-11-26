(function (app) {
    'use strict';

    app.controller('bpFamilyTreeCtrl', bpFamilyTreeCtrl);

    bpFamilyTreeCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', 'apiService', 'notificationService', '$location'];

    function bpFamilyTreeCtrl($scope, $rootScope, $uibModal, $routeParams, apiService, notificationService, $location) {
        $scope.loading = true;
        $scope.search = search;
        $scope.index = 10;
        $scope.Message = "";

        $scope.options = {};
        $scope.apiItems = [];

        $scope.search();

        function search(page) {
            apiService.get('/api/personRelationships/getfamilytree/' + $routeParams.id, null,
            personsRealtionshipsLoadCompleted,
            personsRealtionshipsLoadFailed);
        }


        var apiItems = [];
        var apiItem = "";
        var obj = "";

        function personsRealtionshipsLoadCompleted(result) {
            angular.forEach(result.data.Items, function (value, key) {
                obj = angular.fromJson(value.parents);

                apiItem = new primitives.famdiagram.ItemConfig({
                    id: value.id,
                    title: value.title,
                    description: value.description,
                    //phone: "1 (416) 001-4567",
                    parents: obj,
                    //email: "scott.aasrud@mail.com",
                    //image: "demo/images/photos/a.png",
                    itemTitleColor: primitives.common.Colors.RoyalBlue
                });
                apiItems.push(apiItem);
            });

            //Put the resulting object into scope
            $scope.apiItems = apiItems;

            console.log('$scope.apiItems 1');
            console.log($scope.apiItems);
            $scope.options.items = $scope.apiItems;
            $scope.options.cursorItem = 0;
            $scope.options.highlightItem = 0;
            $scope.options.hasSelectorCheckbox = primitives.common.Enabled.True;
            $scope.options.templates = [getContactTemplate()];
            $scope.options.defaultTemplateName = "contactTemplate";

            console.log('$scope');
            console.log($scope);
            $scope.myOptions = $scope.options;

            $scope.loading = false;
            console.log('$scope.loading in completed function');
            console.log($scope.loading);
            if ($scope.filterPersons && $scope.filterPersons.length) {
                notificationService.displayInfo(result.data.Items.length + ' people found');
            }
        }

        function personsRealtionshipsLoadFailed(response) {
            notificationService.displayError(response.data);
        }


        var items = [
            new primitives.famdiagram.ItemConfig({
                id: 0,
                title: "Steve Haran",
                description: "Main man",
                phone: "1 (416) 001-4567",
                parents: [21, 23],
                email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 3,
                title: "Maite Tome Esteban",
                description: "Main woman",
                phone: "1 (416) 001-4567",
                spouses: [0],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 13,
                title: "Pablo Haran Tome",
                description: "Main boy",
                phone: "1 (416) 001-4567",
                parents: [0, 3],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 17,
                title: "Elena Haran Tome",
                description: "Main girl",
                phone: "1 (416) 001-4567",
                parents: [0, 3],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 21,
                title: "Anthony Haran",
                description: "Steve's Dad",
                phone: "1 (416) 001-4567",
                parents: [],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 31,
                title: "Patricia Haran",
                description: "Steve's Mum",
                phone: "1 (416) 001-4567",
                spouses: [21],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 33,
                title: "Wendy Allan",
                description: "Sister",
                phone: "1 (416) 001-4567",
                parents: [31, 21],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            }),
            new primitives.famdiagram.ItemConfig({
                id: 34,
                title: "Tim Allan",
                description: "Sister",
                phone: "1 (416) 001-4567",
                spouses: [33],
                //email: "scott.aasrud@mail.com",
                //image: "demo/images/photos/a.png",
                itemTitleColor: primitives.common.Colors.RoyalBlue
            })
        ];

        console.log('$scope.loading = ');
        console.log($scope.loading);
        console.log('$scope.apiItems 2');
        console.log($scope.apiItems);
        $scope.options.items = $scope.apiItems;
        $scope.options.cursorItem = 0;
        $scope.options.highlightItem = 0;
        $scope.options.hasSelectorCheckbox = primitives.common.Enabled.True;
        $scope.options.templates = [getContactTemplate()];
        $scope.options.defaultTemplateName = "contactTemplate";

        console.log('$scope');
        console.log($scope);
        $scope.myOptions = $scope.options;

        //$scope.search();

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
                parents: [parent],
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
                + '<div class="bp-item" style="top: 44px; left: 56px; width: 162px; height: 18px; font-size: 12px;"><a name="email" href="mailto::{{itemConfig.email}}" target="_top">{{itemConfig.email}}</a></div>'
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
})(angular.module('phoenix'));

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
                    //chart.famDiagram("update", primitives.famdiagram.UpdateMode.PositonHighlight);
                }
            });

            scope.$watch('options.cursorItem', function (newValue, oldValue) {
                var cursorItem = chart.famDiagram("option", "cursorItem");
                if (cursorItem != newValue) {
                    chart.famDiagram("option", { cursorItem: newValue });
                    //chart.famDiagram("update", primitives.famdiagram.UpdateMode.Refresh);
                }
            });

            scope.$watchCollection('options.items', function (items) {
                chart.famDiagram("option", { items: items });
                //chart.famDiagram("update", primitives.famdiagram.UpdateMode.Refresh);
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
