// Simulate data loading (e.g., 2 seconds)


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


window.addEventListener('DOMContentLoaded', function () {
    const loadingBox = document.querySelectorAll('.main-banner-right-home .list-restaurant .now-loading-restaurant');
    const itemResList = document.querySelectorAll('.main-banner-right-home .list-restaurant .item-restaurant');

    const loadingBoxDisList = document.querySelector('.main-banner-right-home .list-restaurant-district .now-loading-restaurant');
    const itemDisList = this.document.querySelectorAll('.main-banner-right-home .list-restaurant-district .item-restaurant-district');

    // Show loading box
    // loadingBox.style.display = 'block';
    loadingBox.forEach(item => item.style.display = 'block');
    loadingBoxDisList.style.display = 'block';

    // Hide all .item-res initially (in case CSS is not applied)

    itemResList.forEach(item => item.style.display = 'none');
    itemDisList.forEach(item => item.style.display = 'none');

    // Simulate loading
    setTimeout(() => {
        // Hide loading box
        // loadingBox.style.display = 'none';
        loadingBox.forEach(item => item.style.display = 'none');
        loadingBoxDisList.style.display = 'none';

        // Show all item-res elements
        itemResList.forEach(item => item.style.display = 'block');
        itemDisList.forEach(item => item.style.display = 'block');
    }, 2000);
});

document.addEventListener("DOMContentLoaded", function () {
    const seeAllBtn = document.getElementById("seeAllBtn");

    seeAllBtn.addEventListener("click", function (e) {
        e.preventDefault();
        const activeItem = document.querySelector('#menu .nav-link-item.active');

        if (activeItem) {
            const dataId = activeItem.getAttribute('data-id');

            const queryParams = $.param({
                cityId: 1,
                fieldId: dataId,
                pageSize: 6,
                pageNumber: 1
            });

            // Redirect
            window.location.href = '/shops/category?' + queryParams;
        }
    });
});