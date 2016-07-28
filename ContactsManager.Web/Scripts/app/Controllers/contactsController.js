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
            console.log($scope.editableObject);
        }

        $scope.deleteObject = function (id) {
            console.log(id);
            $.ajax({
                url: '/api/contacts/' + id,
                type: 'DELETE',
                beforeSend: function () {

                },
                success: function (data) {
                    console.log('OK');
                },
                fail: function (data) {
                    console.log('FAIL');
                },
                complete: function () {
                    $('#editWindow').hide();
                }
            });
        }

        $scope.editObject = function (id) {
            console.log($scope.editableObject);
            console.log(JSON.stringify($scope.editableObject));

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

            $http({
                url: ajax_url,
                method: ajax_method,
                data: $scope.editableObject
            }).success(function (data) {
                console.log('OK');
            });
        }

        $scope.createObject = function () {

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
                    break;
                }
            }
        }

        hub.client.removeData = function (id) {
            $('.contact[data-id="' + id + '"]').remove();
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