var gb = gb || {};

(function () {

    var googleUser = {};

    gb.initializeGoogleSigning = function () {
        gapi.load('auth2', function () {
            // Retrieve the singleton for the GoogleAuth library and set up the client.
            auth2 = gapi.auth2.init({
                client_id: '446692708935-7vh1ofnvo36o48k29mauhdi43jbo717i.apps.googleusercontent.com',
                cookiepolicy: 'single_host_origin',
                // Request scopes in addition to 'profile' and 'email'
                //scope: 'additional_scope'
            });
            attachSignin(document.getElementById('google-sign-in-button'));
        });

        if (gapi.auth2) {
            attachSignin(document.getElementById('google-sign-in-button'));
        }

        function attachSignin(element) {
            auth2.attachClickHandler(element, {},
                onSignIn, function (error) {
                    alert(JSON.stringify(error, undefined, 2));
                });
        }
    };
})();