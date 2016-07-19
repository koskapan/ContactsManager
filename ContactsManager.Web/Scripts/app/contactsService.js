var contactsApp = angular.module('contactsApp');
contactsApp.factory('contactsService', function () {
    return {
        'odataObject': {
            'Count': 0,
            'Items': [],
            'NextPageLink': '/api/v1/contacts/all?$inlinecount=allpages'
        }};
});