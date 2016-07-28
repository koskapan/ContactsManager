(function () {
    'use strict';
    var contactsApp = angular.module('contactsApp');
    contactsApp.controller('contactsController',
    function ($scope, contactsService, $http) {

        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() === $(document).height()) {
                $scope.getObjects();
            }
        });
        $scope.query = '';
        $scope.contactsODataObject = contactsService.odataObject;
        $scope.prevPageLink = '';

        $scope.getObjects = function () {
            $http.get($scope.contactsODataObject.NextPageLink).success(function (data) {
                $scope.contactsODataObject.Count = data.Count;
                $scope.contactsODataObject.NextPageLink = data.NextPageLink;
                $scope.contactsODataObject.Items.push.apply($scope.contactsODataObject.Items, data.Items);
            });        
        }

        $scope.getObject = function (id) {
            if (id !== null)
                $scope.editableObject = angular.copy($.grep($scope.contactsODataObject.Items, function (e) { return e.id == id; })[0]);
            else
                $scope.editableObject = {  }
        }

        $scope.deleteObject = function (id) {
            $.ajax({
                url: '/api/contacts/' + id,
                type: 'DELETE',
                beforeSend: function () {

                },
                success: function (data) {
                },
                fail: function (data) {
                },
                complete: function () {
                }
            });
        }

        $scope.editObject = function (id) {

            var ajax_url = '';
            var ajax_method = '';

            if (id !== undefined) {
                ajax_url = '/api/contacts/' + id;
                ajax_method = 'PUT';
            }
            else {
                ajax_url = '/api/contacts/';
                ajax_method = 'POST';
            }
            if ($scope.myForm.$valid) {
                $http({
                    url: ajax_url,
                    method: ajax_method,
                    data: $scope.editableObject
                }).success(function (data) {
                });
            }
        }

        $scope.search = function () {
        }
        
        var hub = $.connection.contactsHub;

        hub.client.addData = function (object) {
            var loadedContactsLength = $('.contact').length;
            if ($scope.contactsODataObject.Count == loadedContactsLength)
            {
                $scope.getObjects();
            }
        }

        hub.client.editData = function (id, object) {
            for (var i =0; $scope.contactsODataObject.Items.length; i++)
            {
                if ($scope.contactsODataObject.Items[i].id == id)
                {
                    $scope.contactsODataObject.Items[i] = object;
                    $scope.$apply();
                    break;
                }
            }
        }

        hub.client.removeData = function (id) {
            for (var i = 0; $scope.contactsODataObject.Items.length; i++) {
                if ($scope.contactsODataObject.Items[i].id == id) {
                    $scope.contactsODataObject.Items.splice(i, 1);
                    $scope.$apply();
                    break;
                }
            }
        }

        $.connection.hub.start()
           .done(function () {
               console.log('SignalR success');
           })
           .fail(function () {
               console.log('SignalR fail');
           });
        
        $scope.getObjects();
    });
})();