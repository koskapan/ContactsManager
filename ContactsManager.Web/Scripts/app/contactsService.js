var contactsApp = angular.module('contactsApp');
contactsApp.factory('contactsService', function () {
    return {
        'odataObject': {
            'Count': 0,
            'Items': [],
            'NextPageLink': '/api/contacts/all?$inlinecount=allpages'
        }};
});