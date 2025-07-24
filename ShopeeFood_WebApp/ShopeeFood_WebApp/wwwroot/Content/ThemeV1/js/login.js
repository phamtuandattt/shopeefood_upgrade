

document.addEventListener("DOMContentLoaded", function () {
    const btnLoginHeader = document.querySelector(".header-container .login .btn-login-header");

    btnLoginHeader.addEventListener("click", function (e) {
        e.preventDefault();
        window.location.href = '/login';
    });
});