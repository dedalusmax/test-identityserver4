﻿/// <reference path="client-oauth2.js" />

var ClientOAuth2 = require('client-oauth2')

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = new ClientOAuth2({
    authority: "http://localhost:5000",
    client_id: "js_clientcredentials",
    clientSecret: '"secret".Sha256()',
    accessTokenUri: "http://localhost:5000",
    authorizationUri: 'https://github.com/login/oauth/authorize',
    redirect_uri: "http://localhost:5007/callback.html",
    response_type: "id_token token",
    scope: "openid profile email api:system",
    post_logout_redirect_uri: "http://localhost:5007/index.html",
});

var mgr = new Oidc.UserManager(config);

config.getToken()
    .then(function (user) {
        if (user) {
            log("User logged in", user.profile);
        }
        else {
            log("User not logged in");
        }
    })

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {

        
        var url = "http://localhost:5001/api/v1/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}