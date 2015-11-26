(function (app) {
    'use strict';

    app.controller('familyTreeCtrl', familyTreeCtrl);

    familyTreeCtrl.$inject = ['$scope', '$rootScope', '$uibModal', '$routeParams', 'apiService', 'notificationService'];

    function familyTreeCtrl($scope, $rootScope, $uibModal, $routeParams, apiService, notificationService) {
        $scope.search = search;
        $scope.loadingPersons = true;

        function search(page) {
            $scope.loadingPersons = true;
            apiService.get('/api/personRelationships/search/' + $routeParams.id, null,
            personsRealtionshipsLoadCompleted,
            personsRealtionshipsLoadFailed);
        }

        function personsRealtionshipsLoadCompleted(result) {
            console.log('tree load complete');
            console.log(result.data.Items);
            $scope.jsonResult = angular.toJson(result.data.Items).toLowerCase();
            console.log($scope.jsonResult);
            $scope.loadingPersons = false;
            if ($scope.filterPersons && $scope.filterPersons.length) {
                notificationService.displayInfo(result.data.Items.length + ' people found');
            }
        }

        function personsRealtionshipsLoadFailed(response) {
            notificationService.displayError(response.data);
        }


        $scope.search();

        var options = new primitives.famdiagram.Config();

        var items = [
                { id: 1, parents: [5, 6], title: "Steve Haran", label: "Steve", description: "Desc" },
                { id: 2, parents: [7, 8], title: "Maite Tome Esteban", label: "Maite", description: "Desc" },
                { id: 3, parents: [1, 2], title: "Pablo Haran Tome", label: "Pablo", description: "Desc" },
                { id: 4, parents: [1, 2], title: "Elena Haran Tome", label: "Elena", description: "Desc" },
                { id: 5, title: "Tony Haran", label: "Tone", description: "Desc" },
                { id: 6, title: "Patricia Haran", label: "Tone", description: "Desc" },
                { id: 7, title: "Camilo", label: "Camilo", description: "Desc" },
                { id: 8, title: "Mj", label: "Mj", description: "Desc" },
                { id: 9, parents: [5, 6], title: "Wendy Allen", label: "Wendy", description: "Desc" },
                { id: 10, title: "Tim Allen", label: "Wendy", description: "Desc" },
                { id: 11, parents: [9, 10], title: "Jack Allen", label: "Jack", description: "Desc" },
                { id: 12, parents: [9, 10], title: "Sophie Allen", label: "Sophie", description: "Desc" },
                { id: 13, parents: [7, 8], title: "Mari Jose", label: "Mari Jose", description: "Desc" },
                { id: 14, spouses: [13], title: "Mari Jose Future Husband", label: "Mari Jose Husband", description: "Desc" }

        ];

    //    var items = [
    //        { "id": 1, "parents": "[5]", "title": "steve haran", "label": "haran", "description": "father" },
    //        { "id": 2, "parents": "[1, 3]", "title": "pablo haran", "label": "haran", "description": "mother" },
    //        { "id": 3, "parents": "[]", "title": "maite tome esteban", "label": "tome esteban", "description": null },
    //        { "id": 4, "parents": "[1, 3]", "title": "elena haran tome", "label": "haran tome", "description": "mother" },
    //        { "id": 5, "parents": "[]", "title": "anthony haran", "label": "haran", "description": null },
    //        { "id": 6, spouses: [5], "title": "patricia haran", "label": "haran", "description": null }
    //];

        options.items = items;
        options.cursorItem = 2;
        options.linesWidth = 1;
        options.linesColor = "black";
        options.hasSelectorCheckbox = primitives.common.Enabled.True;
        options.normalLevelShift = 20;
        options.dotLevelShift = 20;
        options.lineLevelShift = 20;
        options.normalItemsInterval = 10;
        options.dotItemsInterval = 10;
        options.lineItemsInterval = 10;
        options.arrowsDirection = primitives.common.GroupByType.parents;

        $scope.myOptions = options;
        console.log('myoptions:');
        console.log($scope.myOptions);


        jQuery("#basicdiagram").famDiagram(options);
    }
})(angular.module('phoenix'));

angular.module('BasicPrimitives', [], function ($compileProvider) {
    $compileProvider.directive('bpOrgDiagram', function ($compile) {
        function link(scope, element, attrs) {
            var itemScopes = [];

            var config = new primitives.orgdiagram.Config();
            angular.extend(config, scope.options);

            config.onItemRender = onTemplateRender;
            config.onCursorChanged = onCursorChanged;
            config.onHighlightChanged = onHighlightChanged;

            var chart = jQuery(element).orgDiagram(config);

            scope.$watch('options.highlightItem', function (newValue, oldValue) {
                var highlightItem = chart.orgDiagram("option", "highlightItem");
                if (highlightItem != newValue) {
                    chart.orgDiagram("option", { highlightItem: newValue });
                    chart.orgDiagram("update", primitives.orgdiagram.UpdateMode.PositonHighlight);
                }
            });

            scope.$watch('options.cursorItem', function (newValue, oldValue) {
                var cursorItem = chart.orgDiagram("option", "cursorItem");
                if (cursorItem != newValue) {
                    chart.orgDiagram("option", { cursorItem: newValue });
                    chart.orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
                }
            });

            scope.$watchCollection('options.items', function (items) {
                chart.orgDiagram("option", { items: items });
                chart.orgDiagram("update", primitives.orgdiagram.UpdateMode.Refresh);
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