(function () {
    'use strict';

    angular.module('phoenix', ['common.core', 'common.ui', 'ngAnimate', 'schemaForm', 'BasicPrimitives', 'ui.bootstrap'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/customers", {
                templateUrl: "scripts/spa/customers/customers.html",
                controller: "customersCtrl"
            })
            .when("/customers/register", {
                templateUrl: "scripts/spa/customers/register.html",
                controller: "customersRegCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/persons/:id", {
                templateUrl: "scripts/spa/persons/persons.html",
                controller: "personsCtrl"
            })
            .when("/personRelationships/:id", {
                templateUrl: "scripts/spa/personRelationships/personRelationships.html",
                controller: "personRelationshipsCtrl"
            })
            .when("/families", {
                templateUrl: "scripts/spa/families/families.html",
                controller: "familiesCtrl"
            })
            .when("/familyTree/:id", {
                templateUrl: "scripts/spa/familyTree/bpFamilyTree.html",
                controller: "bpFamilyTreeCtrl"
            })
            .when("/tests/:id", {
                templateUrl: "scripts/spa/tests/tests.html",
                controller: "testsCtrl"
            })
            .otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    $(".nav a").on("click", function () {
        $(".nav").find(".active").removeClass("active");
        $(this).parent().addClass("active");
    });

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();