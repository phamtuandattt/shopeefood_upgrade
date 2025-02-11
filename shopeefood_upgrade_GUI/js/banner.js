document.addEventListener("DOMContentLoaded", function () {
    /* ----- SHOW SEARCH LIST -----*/
    var searchInput = document.querySelector("#txtSearchHome");
    var resultDiv = document.getElementById("searching-banner"); // Create result div
    var loaderDiv = document.getElementById("loader-searching-banner");

    var typingTimer;
    var typingDelay = 2000; // 2 seconds delay

    // Listen for user input with delay
    searchInput.addEventListener("input", function () {
        clearTimeout(typingTimer); // Clear previous timer
        resultDiv.style.display = "none";
        loaderDiv.style.display = "grid"; // Hide div while typing
        
        if (searchInput.value !== "") {
            typingTimer = setTimeout(function () {
                resultDiv.style.display = "block"; // Show after delay
                resultDiv.style.position = "absolute";
                loaderDiv.style.display = "block";
            }, typingDelay);
        } else {
            loaderDiv.style.display = "block";
        }
    });


    /* ----- SCROLL DIV FOLLOW DIV -----*/
    window.onscroll = function () { scrolling() };

    function scrolling() {
        var fixedDiv = document.getElementById("banner-left");
        var scrollableDiv = document.getElementById("banner-right").offsetHeight;
    
        if (document.documentElement.scrollTop >= scrollableDiv - 770) {
            fixedDiv.style.position = "absolute";
            fixedDiv.style.top = scrollableDiv - 700 + "px";
        }
        else {
            fixedDiv.style.position = "fixed";
            fixedDiv.style.top = "70px";
        }
    }
});


