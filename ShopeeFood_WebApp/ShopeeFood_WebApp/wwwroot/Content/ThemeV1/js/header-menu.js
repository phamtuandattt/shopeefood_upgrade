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



document.addEventListener("DOMContentLoaded", function () {
    const menu = document.getElementById("menu");
    const resultsDiv = document.getElementById("results");
    const links = menu.querySelectorAll(".nav-link-item");

    const loadingBox = document.querySelectorAll('.main-banner-right-home .list-restaurant .now-loading-restaurant');
    const itemResList = document.querySelectorAll('.main-banner-right-home .list-restaurant .item-restaurant');

        links.forEach(link => {
        link.addEventListener("click", async function (e) {
            e.preventDefault();

            // Remove "active" from all
            links.forEach(item => item.classList.remove("active"));
            this.classList.add("active");

            const id = this.dataset.id;

            // Show loading box, hide previous results
            loadingBox.forEach(item => item.style.display = 'block');
            itemResList.forEach(item => item.style.display = 'none');

            // 👇 API endpoint on your Controller
            const apiUrl = `/Home/GetMenuData?id=${id}`;

            try {
                const response = await fetch(`/Home/GetMenuData?id=${id}`);
                if (!response.ok) throw new Error("Failed to fetch");

                const data = await response.json();

                // Render data
                if (data.length === 0) {
                    resultsDiv.innerHTML = "<p>No items found.</p>";
                        
                } else {
                    resultsDiv.innerHTML = data.map(item => `
                            <div class="result-item">
                                <h4>${item.name}</h4>
                                <p>${item.description}</p>
                            </div>
                        `).join("");
                }

            } catch (err) {
                resultsDiv.innerHTML = `<p style="color:red;">Error loading data</p>`;
                console.error(err);
            } finally {
                // Hide loading box after fetch
                //loadingBox.forEach(item => item.style.display = 'none');
            }
        });
    });
});


