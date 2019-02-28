var app = angular.module("HomeJS", ['chart.js', 'ngMaterial', 'ngMessages']);
app.controller('HomeController', function ($http, $scope) {

    $http.get('/Home/Get_Citys').then(function (d) {
        $scope.citylist = d.data;

        $scope.selectedCity = $scope.citylist[0];

        $scope.changeCity = function (city) {
            $scope.selectedCity = city;
        }
    }, function () {
        alert('failed');
    });

    

    $http.get('/Home/Get_Units').then(function (u) {
        $scope.unitlist = u.data;

        $scope.selectedunit = $scope.unitlist[0];

        $scope.changedunit = function (unit) {
            $scope.selectedunit = unit;
        }
    }, function () {
        alert('failed');
    });
    $scope.api_result = function () {
        $http.get('/Home/Get_Api_Result?city_id=' + $scope.city_id + '&unit_id=' + $scope.unit_id + '&selected_date=' + this.myDate.toDateString()).then(function (response) {
            $scope.api_response = response.data;

            var date_time_info = [];
            var max_temp_info = [];
            var temp_info = [];
            var min_temp_info = [];
            var i = 0;
            for (i = 0; i < $scope.api_response.data.length; i++) {
                date_time_info.push($scope.api_response.data[i].datetime);
                temp_info.push($scope.api_response.data[i].temp);
                min_temp_info.push($scope.api_response.data[i].min_temp);
                max_temp_info.push($scope.api_response.data[i].max_temp);
            }

            $scope.labels = date_time_info;
            $scope.series = ['Max Temp', 'Temp', 'Min Temp'];

            $scope.data = [max_temp_info, temp_info, min_temp_info];

        }, function () {
            alert('failed');
        });
    };

    this.myDate = new Date().toDateString();
    this.isOpen = false;

    $scope.today = new Date();
    
});
