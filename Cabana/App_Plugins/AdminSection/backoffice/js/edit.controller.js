angular.module('umbraco').controller('editCtrl', function ($scope, $http) {
    $scope.name = {val:null};
    this.getMemberMovies = function (u_name) {
        //alert(u_name);
        if (u_name == null || u_name == '') {
            $scope.error = "invalid user";
            $scope.movies = '';
            $scope.res_name = '';
        }
        else {
            $http({
                url: "/Umbraco/backoffice/Api/ApiAdmin/GetMembersMovies",//member/" + mem_name + "/movies",
                //url: "/umbraco/backoffice/api/apiadmin/member/" + u_name + "/movies",
                //url: "/umbraco/backoffice/api/apiadmin/membermovies/" + u_name,
                method: "GET",
                params: {
                    name: u_name
                }
            }).then(function (response) {
                var obj = response.data;
                if (obj.error == '') {
                    $scope.movies = obj.movies;
                    $scope.res_name = obj.name;
                    $scope.error = '';
                }
                else {
                    $scope.error = obj.error;
                    $scope.movies = '';
                    $scope.res_name = '';
                }

            });/**/
        }
    };
    //alert('ok');
});