document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("searchModal");
    var btn = document.getElementById("openModal");
    var closeBtn = document.querySelector(".close");

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


// Handle get shop of city by business field
document.addEventListener("DOMContentLoaded", function () {
    const menu = document.getElementById("menu");
    const resultsDiv = document.getElementById("results");
    const links = menu.querySelectorAll(".nav-link-item");

    // loading box
    const loadingBox = document.querySelectorAll('.main-banner-right-home .list-restaurant .now-loading-restaurant');
    const itemResList = document.querySelectorAll('.main-banner-right-home .list-restaurant .item-restaurant');

    links.forEach(link => {
        link.addEventListener("click", async function (e) {
            e.preventDefault();

            // Remove "active" from all
            links.forEach(item => item.classList.remove("active"));
            this.classList.add("active");

            const id = this.dataset.id;

            const apiUrl = '/Home/GetMenuData?';

            $.ajax({
                url: apiUrl,
                type: "GET",
                data: id,
                beforeSend: function (bs) {
                    //$('#page-loader').show();
                    // Show loading box, hide previous results
                    loadingBox.forEach(item => item.style.display = 'block');
                    itemResList.forEach(item => item.style.display = 'none');
                },
                complete: function () {
                    // Hide loading box after fetch
                    loadingBox.forEach(item => item.style.display = 'none');
                },
                success: function (response) {
                    if (response != null && response.success) {

                        // apend content format html
                        // create function get data and return data attack HTML
                        

                    } else {

                    }
                },
                error: function (response) {

                }
            })

        });
    });
});


