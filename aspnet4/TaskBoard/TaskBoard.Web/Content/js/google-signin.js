var gb = gb || {};

(function () {

    var googleUser = {};

    gb.initializeGoogleSigning = function () {

        var googleSignInButton = document.getElementById('google-sign-in-button');

        gapi.load('auth2', function () {

            auth2 = gapi.auth2.init({
                client_id: '446692708935-7vh1ofnvo36o48k29mauhdi43jbo717i.apps.googleusercontent.com',
                cookiepolicy: 'single_host_origin',
            });

            attachSignin(googleSignInButton);
        });

        if (gapi.auth2 && googleSignInButton) {
            attachSignin(googleSignInButton);
        }

        function attachSignin(element) {
            auth2.attachClickHandler(element, {},
                onGoogleSignIn, function (error) {
                    alert(JSON.stringify(error, undefined, 2));
                });
        }
    };
})();