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
        $scope.contactsODataObject = angular.copy(contactsService.odataObject);
        $scope.sortType = {
            type: 'Id',
            reversed: false
        }
        $scope.prevPageLink = '';


        $scope.sort = function (type) {
            if ($scope.sortType.type == type) {
                $scope.sortType.reversed = !$scope.sortType.reversed;
            }
            else {
                $scope.sortType.type = type;
                $scope.sortType.reversed = false;
            }
            $scope.contactsODataObject = angular.copy(contactsService.odataObject);
            $scope.contactsODataObject.NextPageLink = $scope.contactsODataObject.NextPageLink + '&$orderby=' + $scope.sortType.type + ($scope.sortType.reversed ? ' desc' : '') + '&q=' + $scope.queryString;
            $scope.getObjects();
        }

        $scope.getObjects = function () {
            if ((!$('.ajaxLoadingContacts').is(':visible')) && ($scope.contactsODataObject.NextPageLink !== null)) {
                $('.ajaxLoadingContacts').show();
                $http.get($scope.contactsODataObject.NextPageLink).error(function (data) {
                    console.log(data);
                }).success(function (data) {
                    $scope.contactsODataObject.Count = data.Count;
                    $scope.contactsODataObject.NextPageLink = data.NextPageLink;
                    $scope.contactsODataObject.Items.push.apply($scope.contactsODataObject.Items, data.Items);
                    $('.ajaxLoadingContacts').hide();
                });
            }
        }

        $scope.highlight = function (text, search) {
            if (!search) {
                return $sce.trustAsHtml(text);
            }
            return $sce.trustAsHtml(text.replace(new RegExp(search, 'gi'), '<span class="highlightedText">$&</span>'));
        };

        $scope.getObject = function (id) {
            if (id !== null){
                $scope.editableObject = angular.copy($.grep($scope.contactsODataObject.Items, function (e) { return e.id == id; })[0]);
            }
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
            $scope.contactsODataObject = angular.copy(contactsService.odataObject);
            $scope.contactsODataObject.NextPageLink = $scope.contactsODataObject.NextPageLink + '&$orderby=' + $scope.sortType.type + ($scope.sortType.reversed ? ' desc' : '') + '&q=' + $scope.searchString;
            $scope.getObjects();
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