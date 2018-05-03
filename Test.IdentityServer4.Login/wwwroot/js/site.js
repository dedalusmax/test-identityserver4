// Write your JavaScript code.

function removeErrorMessage() {
    var messageElement = document.getElementById('error-message');
    if (messageElement) {
        messageElement.remove();
    }
}
function validate() {
    var username = document.getElementById('username');
    var password = document.getElementById('password');
    var loginButton = document.getElementById('loginButton');

    if (username.value !== '' && password.value !== '') {
        loginButton.removeAttribute("disabled");
    }
    else {
        loginButton.setAttribute("disabled", "disabled");
    }
}
