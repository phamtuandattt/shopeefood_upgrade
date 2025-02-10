document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("searchModal");
    var btn = document.getElementById("openModal");
    var closeBtn = document.querySelector(".close");
    var searchInput = document.querySelector(".search-input");

    // Open modal
    btn.onclick = function () {
        modal.style.display = "flex";  // Make sure it's visible before animation
        setTimeout(() => {
            modal.classList.add("show");
        }, 10); // Small delay to allow CSS transition
    };

    // Close modal
    closeBtn.onclick = function () {
        modal.classList.remove("show");
        setTimeout(() => {
            modal.style.display = "none";
        }, 500); // Wait for fade-out animation
    };

    window.onclick = function (event) {
        if (event.target === modal) {
            modal.classList.remove("show");
            setTimeout(() => {
                modal.style.display = "none";
            }, 500);
        }
    };


    /* ----- SHOW SEARCH LIST -----*/
    var searchInput = document.querySelector(".search-input");
    var loaderDiv = document.getElementById("loader");
    var resultDiv = document.getElementById("searching"); // Create result div

    var typingTimer;
    var typingDelay = 2000; // 2 seconds delay

    // Listen for user input with delay
    searchInput.addEventListener("input", function () {
        clearTimeout(typingTimer); // Clear previous timer
        resultDiv.style.display = "none";
        loaderDiv.style.display = "grid"; // Hide div while typing

        typingTimer = setTimeout(function () {
            loaderDiv.style.display = "none";
            resultDiv.style.display = "block"; // Show after delay
        }, typingDelay);
    });
});


