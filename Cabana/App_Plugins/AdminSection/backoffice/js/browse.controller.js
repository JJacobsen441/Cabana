angular.module('umbraco').controller('browseCtrl', function ($scope, $http) {
    this.getAllMembers = function () {
        $http({
            url: "/umbraco/backoffice/api/ApiAdmin/GetAllMembers",
            //url: "/umbraco/backoffice/api/apiadmin/members",
            method: "GET"//,
            //contentType: "application/json;charset=utf-8"
        }).then(function (response) {
            var obj = JSON.parse(response.data);
            //angular.element('#tester').html(obj.data);
            if (obj.error == '') {
                $scope.members = obj.members;
                $scope.error = '';
            }
            else {
                $scope.error = obj.error;
                $scope.members = '';
            }

        });
    };
    this.getAllMembers();
});