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

            const apiUrl = '/home/shops';

            const data = new Object();
            data.cityId = 1; // Get select city
            data.fieldId = this.dataset.id;;
            data.pageSize = 6;
            data.pageNumber = 1;

            $.ajax({
                url: apiUrl,
                type: "POST",
                data: data,
                beforeSend: function (bs) {
                    //$('#page-loader').show();
                    // Show loading box, hide previous results
                    loadingBox.forEach(item => item.style.display = 'block');
                    itemResList.forEach(item => item.style.display = 'none');
                },
                complete: function () {
                    // Hide loading box after fetch
                    loadingBox.forEach(item => item.style.display = 'none');
                    itemResList.forEach(item => item.style.display = 'block');
                },
                success: function (response) {
                    if (response != null) {

                        // apend content format html
                        // create function get data and return data attack HTML
                        const restaurants = response.dataReturn; // assuming data is a list of objects
                        renderRestaurants(restaurants);
                        

                    } else {

                    }
                },
                error: function (response) {

                }
            })

        });
    });
});

function renderRestaurants(restaurants) {
    const container = $('#list-restaurant-container');
    container.empty(); // clear previous content
    console.log(restaurants);
    restaurants.forEach(item => {
        const html = `
        <div class="item-restaurant" id="${item.shopID}">
            <a target="_blank" class="item-content" href="#">
                <div class="img-restaurant">
                    <div class="tag-preferred">
                        <i class="icon-like" aria-hidden="true"></i>Yêu thích
                    </div>
                    <img src="${item.shopImage}" class="">
                </div>
                <div class="info-restaurant">
                    <div class="info-basic-res">
                        <h4 class="name-res" title="${item.shopName}">
                            ${item.shopName}
                        </h4>
                        <div class="address-res" title="${item.shopAddress}">
                            ${item.shopAddress}
                        </div>
                    </div>
                    <p class="content-promotion"><i class="icon-tag"></i> Giảm món</p>
                    <div class="opentime-status">
                        <span class="stt online" title="${item.shopUptime}"
                              style="color: rgb(35, 152, 57); background-color: rgb(35, 152, 57);"></span>
                    </div>
                </div>
            </a>
        </div>
        `;
        container.append(html);
    });
}

