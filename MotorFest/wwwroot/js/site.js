// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
//document.addEventListener("DOMContentLoaded", function () {
//    setTimeout(function () {
//        document.getElementById("loading-screen").classList.add("fade-out");
//    }, 2000);
//});
document.addEventListener("DOMContentLoaded", function () {
    var loadingScreen = document.getElementById("loading-screen");
    var fadeContainer = document.querySelector(".fade-container");

    if (loadingScreen) {
        setTimeout(function () {
            loadingScreen.classList.add("fade-out");
        }, 1500);
    }

    if (fadeContainer) {
        fadeContainer.style.opacity = "1";
    }
});