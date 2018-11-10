var personController = function ($scope, $http) {
    $scope.title = "List of People";
    //$scope.selectedId = 0;

    

    $scope.savePerson = function (data) {
        console.log(data);
        $http.post('http://localhost:54980/api/People', data).then(function (response) {
            console.log(response);
        });
    }

    $scope.updatePerson = function (data) {
        console.log(data);
        $http.put('http://localhost:54980/api/People/' + $scope.selectedId, data).then(function (response) {
            console.log(response);
        });
    }

    $scope.updatebtnClicked = function (data) {
        $scope.selectedId = $("#Id").val();
        $scope.updatePerson(data);
    }

    

    $scope.getPeople = function () {
        $http.get('http://localhost:54980/api/People').then(function (response) {
            $scope.peopleList = response.data;
            console.log(response);
        });
    }

    $scope.getById = function (xid) {
        debugger;
        $http.get('http://localhost:54980/api/People/' + xid).then(function (response) {
            console.log(response);
            $scope.personDTO = response.data;
        });
    }


    $scope.deletePerson = function (personId) {
        $http.delete('http://localhost:54980/api/People/' + personId).success(function (data) {
            $scope.peopleList = data;
            $scope.refresh();
        });
    }
}

personAppModule.controller('personController', personController)