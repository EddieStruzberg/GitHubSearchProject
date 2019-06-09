(function () {
    var app = angular.module('MyApp', ['ngRoute'])

    app.config(function ($routeProvider) {
        $routeProvider
           .when("/Home", {
               templateUrl: "home/Index.cshtml",
               controller: "HomeController"
           })
          .when("/SearchRepositories", {
              templateUrl: "home/SearchRepositories.cshtml",
              controller: "SearchRepositoriesController"
          })
         .when("/BookMarks", {
             templateUrl: "home/BookMarks.cshtml",
             controller: "BookMarksController"
         })
    });
    //--------------------------------------------------------------------------------
    app.controller("HomeController", function ($scope) {
        $scope.Headline = "Search for Repositories";
        //Make a new Repository search
        $scope.SetData = function () {
            sessionStorage.setItem("Search", $scope.Search);
        }
        AddEnterKeyEventToElement("search", "searchInput");
    });
    //--------------------------------------------------------------------------------
    app.controller("SearchRepositoriesController", function ($scope, $http) {
        $scope.Headline = "Repository Results Gallery"

        //Call to FindRepositories which makes an API call to git
        $http({
            method: "GET",
            url: '/Data/FindRepositories',
            params: { search: sessionStorage.getItem("Search") }
        }).then(function (response) {
            $scope.ReceivedData = response.data
        })

        AddEnterKeyEventToElement("search", "searchInput");

        //Make a new Repository search
        $scope.SearchRepositories = function () {
            $http({
                method: "GET",
                url: '/Data/FindRepositories',
                params: { search: $scope.Search }
            }).then(function (response) {
                $scope.ReceivedData = response.data
            })
        }

        //Set BookMark if it didnt already booked before
        $scope.SetBookMark = function (repository) {
            $scope.BookMarks = []

            //if there is previus booked repositories in this session load them
            var temp = sessionStorage.getItem('repositories');
            if (temp != null) {
                $scope.BookMarks = $.parseJSON(temp);
            }

            //check if the wanted repository already booked, if so-notify, else add the new bookmark
            if (containsObject(repository, $scope.BookMarks)) {
                alert('this Repository already BookMarked :) ')
            }
            else {
                $scope.BookMarks.push(repository);
            }
            //set the new Bookmark list 
            sessionStorage.setItem('repositories', JSON.stringify($scope.BookMarks));
        };
    });
    //--------------------------------------------------------------------------------
    app.controller("BookMarksController", function ($scope) {
        $scope.Headline = "BookMarked Repositories Gallery"
        //Get BookMarks from Session
        $scope.BookMarks = $.parseJSON(sessionStorage.getItem('repositories'));
    });
    //Global Functions----------------------------------------------------------------
    //Checks if an obj already in List
    function containsObject(obj, list) {
        var i;
        for (i = 0; i < list.length; i++) {
            if (list[i].id === obj.id) {
                return true;
            }
        }
        return false;
    }
    //Add ENTER key to hit the "search" button
    function AddEnterKeyEventToElement(buttonName, searchElementName) {
        var input = document.getElementById(searchElementName);
        input.addEventListener("keyup", function (event) {
            if (event.keyCode === 13) {
                event.preventDefault();
                document.getElementById(buttonName).click();
            }
        });
    }

})();