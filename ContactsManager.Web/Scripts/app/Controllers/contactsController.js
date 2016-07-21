﻿
contactsApp.controller('contactsController',
function ($scope, contactsService) {

    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() === $(document).height()) {
           $scope.getObjects();
        }
    });
    $scope.query = '';
    $scope.contactsODataObject = contactsService.odataObject;
    $scope.prevPageLink = '';
    $scope.getObjects = function () {
        if ($scope.contactsODataObject.NextPageLink !== $scope.prevPageLink) {
            $.ajax({
                url: $scope.contactsODataObject.NextPageLink,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    $('.ajaxLoadingContacts').show();
                },
                success: function (data) {
                    //$scope.prevPageLink = $scope.contactsODataObject.NextPageLink;
                    $scope.contactsODataObject.Items = $scope.contactsODataObject.Items.concat(data.Items);
                    $scope.contactsODataObject.NextPageLink = data.NextPageLink;
                    $scope.contactsODataObject.Count = data.Count;
                    $scope.$apply();
                    console.log('OK');
                },
                fail: function (data) {
                    console.log('FAIL');
                    console.log(data);
                },
                complete: function () {
                    $('.ajaxLoadingContacts').hide();
                }
            });
        }
    }

    $scope.getObject = function (id) {
        $scope.editableObject = $scope.contactsODataObject.Items[id-1];
    }

    $scope.createObject = function() {
        $scope.editableObject = {
            'id' : -1
        };
    }

    $scope.editObject = function () {
        $.ajax({
            url: $scope.editableObject.id === -1 ? '/api/v1/contacts' : '/api/v1/contacts/' + $scope.editableObject.id,
            type: $scope.editableObject.id === -1 ? 'POST' : 'PUT',
            data: $scope.editableObject,
            contentType: 'application/json;charset=utf-8',
            beforeSend: function() {
                
            },
            success: function (data) {
                console.log('OK');
            },
            fail: function (data) {
                console.log('FAIL');
            },
            complete: function() {
                $('#editWindow').hide();
            }
        });
    }

    $scope.getObjects();
});