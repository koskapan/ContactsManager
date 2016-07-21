
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
        $scope.editableObject = $.grep($scope.contactsODataObject.Items, function (e) { return e.id == id; })[0];
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
        $.ajax({
            url: '/api/contacts/' + id,
            type: 'PUT',
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