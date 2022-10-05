angular.module('umbraco').controller('browseCtrl', function ($scope, $http) {
    this.getAllMembers = function () {
        $http({
            url: "/umbraco/backoffice/api/ApiAdmin/GetAllMembers",
            //url: "/umbraco/backoffice/api/apiadmin/members",
            method: "GET"//,
            //contentType: "application/json;charset=utf-8"
        }).then(function (response) {
            //alert(JSON.stringify(response.data));
            //var obj = JSON.parse(response.data);
            var obj = response.data;
            
            //angular.element('#tester').html(obj.data);
            if (response.status == 200) {
                $scope.members = obj.members;
                $scope.error = obj.error;
            }
            else {
                $scope.error = obj.error;
                $scope.members = '';
            }

        });
    };
    this.getAllMembers();
});