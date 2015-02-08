angular.module('contactsModule').controller('contactsController', function ($scope, $http) {

    $scope.loadRecords = function () {
        $http.get("/api/Contact").success(function (data, status, headers, config) {
            $scope.Contacts = data;
            $scope.ContactsQuantity = $scope.Contacts.length;

            $scope.Id = "";
            $scope.Firstname = "";
            $scope.Secondname = "";
            $scope.Email = "";
            $scope.Phone = "";
        });        
    }

    $scope.save = function () {
        if ($scope.Id === undefined) {
            var contact = {
                Firstname: $scope.Firstname,
                Secondname: $scope.Secondname,
                Email: $scope.Email,
                Phone: $scope.Phone
            };

            var request = $http.post("/api/Contact/", contact).success(function (data, status, headers, config) {
                $scope.loadRecords();
            });
        }
        else {
            var contact = {
                Id: $scope.Id,
                Firstname: $scope.Firstname,
                Secondname: $scope.Secondname,
                Email: $scope.Email,
                Phone: $scope.Phone
            };

            var request = $http.post("/api/Contact/" + contact.Id, contact).success(function (data, status, headers, config) {
                $scope.loadRecords();
            });
        }

        
    };

    $scope.get = function (Contact) {
        $http.get("/api/Contact/" + Contact.Id).success(function (data, status, headers, config) {
            $scope.Id = data.Id;
            $scope.Firstname = data.Firstname;
            $scope.Secondname = data.Secondname;
            $scope.Email = data.Email;
            $scope.Phone = data.Phone;
        });
    }

    $scope.delete = function () {
        $http.delete("/api/Contact/" + $scope.Id).success(function (data, status, headers, config) {
            $scope.loadRecords();
        });        
    }

    $scope.cancel = function () {
        $scope.Id = "";
        $scope.Firstname = "";
        $scope.Secondname = "";
        $scope.Email = "";
        $scope.Phone = "";
    }

    $scope.loadRecords();
});