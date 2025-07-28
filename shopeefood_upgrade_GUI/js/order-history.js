// Sample order data
const sampleOrders = [
    {
        id: "ORD001",
        date: "2024-03-15",
        restaurant: "Pizza Palace",
        status: "delivered",
        total: 45.99,
        deliveryFee: 3.99,
        tax: 4.12,
        items: [
            {
                name: "Margherita Pizza",
                image: "https://via.placeholder.com/80x80/FF6B6B/FFFFFF?text=🍕",
                price: 18.99,
                quantity: 1
            },
            {
                name: "Caesar Salad",
                image: "https://via.placeholder.com/80x80/4ECDC4/FFFFFF?text=🥗",
                price: 12.99,
                quantity: 1
            },
            {
                name: "Garlic Bread",
                image: "https://via.placeholder.com/80x80/FFE66D/FFFFFF?text=🍞",
                price: 6.99,
                quantity: 2
            }
        ]
    },
    {
        id: "ORD002",
        date: "2024-03-12",
        restaurant: "Burger House",
        status: "delivered",
        total: 32.50,
        deliveryFee: 2.99,
        tax: 2.87,
        items: [
            {
                name: "Classic Burger",
                image: "https://via.placeholder.com/80x80/FF8E53/FFFFFF?text=🍔",
                price: 15.99,
                quantity: 1
            },
            {
                name: "French Fries",
                image: "https://via.placeholder.com/80x80/A8E6CF/FFFFFF?text=🍟",
                price: 5.99,
                quantity: 1
            },
            {
                name: "Coke",
                image: "https://via.placeholder.com/80x80/FF3838/FFFFFF?text=🥤",
                price: 2.99,
                quantity: 2
            }
        ]
    },
    {
        id: "ORD003",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    },
    {
        id: "ORD003",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    },
    {
        id: "ORD004",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    },
        {
        id: "ORD005",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    },
        {
        id: "ORD006",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    },
        {
        id: "ORD007",
        date: "2024-03-10",
        restaurant: "Sushi Express",
        status: "cancelled",
        total: 67.80,
        deliveryFee: 4.99,
        tax: 6.16,
        items: [
            {
                name: "Salmon Roll",
                image: "https://via.placeholder.com/80x80/FFB3BA/FFFFFF?text=🍣",
                price: 24.99,
                quantity: 2
            },
            {
                name: "Miso Soup",
                image: "https://via.placeholder.com/80x80/BAFFC9/FFFFFF?text=🍜",
                price: 8.99,
                quantity: 1
            }
        ]
    }
];

// Pagination variables
let currentPage = 1;
let itemsPerPage = 5;
let totalPages = 1;
let allOrders = [...sampleOrders];
let filteredOrders = [...sampleOrders];

// Initialize the page
// Update the DOMContentLoaded event listener
document.addEventListener('DOMContentLoaded', function() {
    renderOrders(filteredOrders);
    updateActiveFiltersCount(); // Initialize filter count
    
    
    // Form submission handler
    document.getElementById('addressFormElement')?.addEventListener('submit', function(e) {
        e.preventDefault();
        saveAddress();
    });
});



// Sample order data (keep existing sampleOrders array)
// ... (keep your existing sampleOrders array as is)

// Initialize the page
document.addEventListener('DOMContentLoaded', function() {
    updatePagination();
    renderCurrentPage();
    updateActiveFiltersCount();
});

// Updated render function with pagination
function renderOrders(orders) {
    filteredOrders = orders;
    currentPage = 1; // Reset to first page when filters change
    updatePagination();
    renderCurrentPage();
}

function renderCurrentPage() {
    const container = document.getElementById('orderCardsContainer');
    const orderCount = document.getElementById('orderCount');
    const pageInfo = document.getElementById('pageInfo');
    
    // Calculate pagination
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = itemsPerPage === 'all' ? filteredOrders.length : Math.min(startIndex + itemsPerPage, filteredOrders.length);
    const currentOrders = itemsPerPage === 'all' ? filteredOrders : filteredOrders.slice(startIndex, endIndex);
    
    // Update summary info
    orderCount.textContent = `${filteredOrders.length} order${filteredOrders.length !== 1 ? 's' : ''} found`;
    
    if (filteredOrders.length === 0) {
        pageInfo.textContent = 'No orders to display';
        container.innerHTML = '<div class="no-orders">No orders found matching your criteria.</div>';
        document.getElementById('paginationContainer').style.display = 'none';
        return;
    }
    
    // Show pagination container
    document.getElementById('paginationContainer').style.display = 'flex';
    
    // Update page info
    if (itemsPerPage === 'all') {
        pageInfo.textContent = `Showing all ${filteredOrders.length} orders`;
    } else {
        pageInfo.textContent = `Showing ${startIndex + 1}-${endIndex} of ${filteredOrders.length} orders`;
    }
    
    // Add loading effect
    container.classList.add('loading');
    
    setTimeout(() => {
        // Render orders
        container.innerHTML = currentOrders.map(order => {
            const subtotal = order.total - order.deliveryFee - order.tax;
            const itemCount = order.items.reduce((sum, item) => sum + item.quantity, 0);
            
            return `
                <div class="order-card" onclick="showOrderDetails('${order.id}')">
                    <div class="order-card-header">
                        <div class="order-basic-info">
                            <h3 class="restaurant-name">${order.restaurant}</h3>
                            <p class="order-date">${formatDate(order.date)}</p>
                        </div>
                        <div class="order-status-price">
                            <span class="order-status status-${order.status}">${capitalizeFirst(order.status)}</span>
                            <span class="order-total">$${order.total.toFixed(2)}</span>
                        </div>
                    </div>
                    <div class="order-card-body">
                        <div class="order-preview-items">
                            ${order.items.slice(0, 3).map(item => `
                                <img src="${item.image}" alt="${item.name}" class="preview-item-image">
                            `).join('')}
                            ${order.items.length > 3 ? `<span class="more-items">+${order.items.length - 3}</span>` : ''}
                        </div>
                        <div class="order-summary-info">
                            <span class="item-count">${itemCount} item${itemCount !== 1 ? 's' : ''}</span>
                            <span class="order-id">Order #${order.id}</span>
                        </div>
                    </div>
                </div>
            `;
        }).join('');
        
        container.classList.remove('loading');
    }, 150); // Small delay for smooth transition
}

function updatePagination() {
    if (itemsPerPage === 'all') {
        totalPages = 1;
        document.getElementById('paginationContainer').style.display = 'none';
        return;
    }
    
    totalPages = Math.ceil(filteredOrders.length / itemsPerPage);
    
    // Update pagination info
    const paginationInfo = document.getElementById('paginationInfo');
    paginationInfo.textContent = totalPages > 0 ? `Page ${currentPage} of ${totalPages}` : 'No pages';
    
    // Update button states
    updatePaginationButtons();
    
    // Generate page numbers
    generatePageNumbers();
}

function updatePaginationButtons() {
    const btnFirst = document.getElementById('btnFirst');
    const btnPrev = document.getElementById('btnPrev');
    const btnNext = document.getElementById('btnNext');
    const btnLast = document.getElementById('btnLast');
    
    // Disable/enable buttons based on current page
    btnFirst.disabled = currentPage === 1;
    btnPrev.disabled = currentPage === 1;
    btnNext.disabled = currentPage === totalPages || totalPages === 0;
    btnLast.disabled = currentPage === totalPages || totalPages === 0;
}

function generatePageNumbers() {
    const pageNumbers = document.getElementById('pageNumbers');
    pageNumbers.innerHTML = '';
    
    if (totalPages <= 1) return;
    
    const maxVisiblePages = window.innerWidth <= 480 ? 3 : 5;
    let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);
    
    // Adjust start page if we're near the end
    if (endPage - startPage < maxVisiblePages - 1) {
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }
    
    // Add first page and ellipsis if needed
    if (startPage > 1) {
        pageNumbers.appendChild(createPageButton(1));
        if (startPage > 2) {
            pageNumbers.appendChild(createEllipsis());
        }
    }
    
    // Add visible page numbers
    for (let i = startPage; i <= endPage; i++) {
        pageNumbers.appendChild(createPageButton(i));
    }
    
    // Add ellipsis and last page if needed
    if (endPage < totalPages) {
        if (endPage < totalPages - 1) {
            pageNumbers.appendChild(createEllipsis());
        }
        pageNumbers.appendChild(createPageButton(totalPages));
    }
}

function createPageButton(pageNum) {
    const button = document.createElement('div');
    button.className = `page-number ${pageNum === currentPage ? 'active' : ''}`;
    button.textContent = pageNum;
    button.onclick = () => goToPage(pageNum);
    return button;
}

function createEllipsis() {
    const ellipsis = document.createElement('div');
    ellipsis.className = 'page-ellipsis';
    ellipsis.textContent = '...';
    return ellipsis;
}

// Pagination navigation functions
function goToPage(page) {
    if (page === 'last') {
        page = totalPages;
    }
    
    if (page >= 1 && page <= totalPages && page !== currentPage) {
        currentPage = page;
        updatePagination();
        renderCurrentPage();
        
        // Scroll to top of orders list
        document.querySelector('.order-list-area').scrollIntoView({ 
            behavior: 'smooth', 
            block: 'start' 
        });
    }
}

function goToNextPage() {
    if (currentPage < totalPages) {
        goToPage(currentPage + 1);
    }
}

function goToPreviousPage() {
    if (currentPage > 1) {
        goToPage(currentPage - 1);
    }
}

function changeItemsPerPage() {
    const select = document.getElementById('itemsPerPage');
    const newItemsPerPage = select.value === 'all' ? 'all' : parseInt(select.value);
    
    if (newItemsPerPage !== itemsPerPage) {
        itemsPerPage = newItemsPerPage;
        currentPage = 1; // Reset to first page
        updatePagination();
        renderCurrentPage();
    }
}

// Keep all your existing functions (showOrderDetails, closeOrderDetails, etc.)
// ... (all the existing functions remain the same)

// Updated applyFilters function
function applyFilters() {
    const timeFilter = document.getElementById('timeFilter').value;
    const priceFilter = document.getElementById('priceFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;
    const sortFilter = document.getElementById('sortFilter').value;
    const searchFilter = document.getElementById('searchFilter').value.toLowerCase();

    let filtered = [...sampleOrders];

    // Apply filters (existing logic)
    if (timeFilter !== 'all') {
        const now = new Date();
        const filterDate = new Date();
        
        switch(timeFilter) {
            case 'week': filterDate.setDate(now.getDate() - 7); break;
            case 'month': filterDate.setDate(now.getDate() - 30); break;
            case '3months': filterDate.setMonth(now.getMonth() - 3); break;
            case 'year': filterDate.setFullYear(now.getFullYear() - 1); break;
        }
        
        filtered = filtered.filter(order => new Date(order.date) >= filterDate);
    }

    if (priceFilter !== 'all') {
        const [min, max] = priceFilter.split('-').map(p => p === '+' ? Infinity : parseFloat(p));
        filtered = filtered.filter(order => {
            return max ? (order.total >= min && order.total <= max) : order.total >= min;
        });
    }

    if (statusFilter !== 'all') {
        filtered = filtered.filter(order => order.status === statusFilter);
    }

    if (searchFilter) {
        filtered = filtered.filter(order => 
            order.restaurant.toLowerCase().includes(searchFilter) ||
            order.items.some(item => item.name.toLowerCase().includes(searchFilter))
        );
    }

    // Apply sorting
    filtered.sort((a, b) => {
        switch(sortFilter) {
            case 'date-desc': return new Date(b.date) - new Date(a.date);
            case 'date-asc': return new Date(a.date) - new Date(b.date);
            case 'price-desc': return b.total - a.total;
            case 'price-asc': return a.total - b.total;
            case 'items-desc': 
                const aItems = a.items.reduce((sum, item) => sum + item.quantity, 0);
                const bItems = b.items.reduce((sum, item) => sum + item.quantity, 0);
                return bItems - aItems;
            default: return 0;
        }
    });

    renderOrders(filtered);
    updateActiveFiltersCount();
}

// Updated clearFilters function
function clearFilters() {
    document.getElementById('timeFilter').value = 'all';
    document.getElementById('priceFilter').value = 'all';
    document.getElementById('statusFilter').value = 'all';
    document.getElementById('sortFilter').value = 'date-desc';
    document.getElementById('searchFilter').value = '';
    
    renderOrders([...sampleOrders]);
    updateActiveFiltersCount();
}

// Keep all other existing functions unchanged...
// (showOrderDetails, closeOrderDetails, reorderItems, returnOrder, downloadReceipt, 
//  formatDate, capitalizeFirst, toggleFilterArea, updateActiveFiltersCount, etc.)


// Add these variables at the top of your script section
let isFilterExpanded = true; // Start expanded
let activeFiltersCount = 0;

// Add these functions to your existing JavaScript

function toggleFilterArea() {
    const filterContent = document.getElementById('filterContent');
    const toggleArrow = document.getElementById('toggleArrow');
    const filterArea = document.querySelector('.filter-area');
    
    isFilterExpanded = !isFilterExpanded;
    
    if (isFilterExpanded) {
        filterContent.classList.remove('collapsed');
        toggleArrow.classList.remove('rotated');
        filterArea.classList.add('expanded');
    } else {
        filterContent.classList.add('collapsed');
        toggleArrow.classList.add('rotated');
        filterArea.classList.remove('expanded');
        showFilterSummary();
    }
}

function updateActiveFiltersCount() {
    const timeFilter = document.getElementById('timeFilter').value;
    const priceFilter = document.getElementById('priceFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;
    const sortFilter = document.getElementById('sortFilter').value;
    const searchFilter = document.getElementById('searchFilter').value.trim();
    
    let count = 0;
    const defaultValues = {
        timeFilter: 'all',
        priceFilter: 'all',
        statusFilter: 'all',
        sortFilter: 'date-desc'
    };
    
    // Count active filters
    if (timeFilter !== defaultValues.timeFilter) count++;
    if (priceFilter !== defaultValues.priceFilter) count++;
    if (statusFilter !== defaultValues.statusFilter) count++;
    if (sortFilter !== defaultValues.sortFilter) count++;
    if (searchFilter !== '') count++;
    
    activeFiltersCount = count;
    
    // Update UI elements
    const activeFiltersElement = document.getElementById('activeFiltersCount');
    const quickClearBtn = document.getElementById('quickClearBtn');
    
    if (count > 0) {
        activeFiltersElement.textContent = `${count} active`;
        activeFiltersElement.style.display = 'block';
        quickClearBtn.style.display = 'block';
        quickClearBtn.classList.add('show');
    } else {
        activeFiltersElement.style.display = 'none';
        quickClearBtn.style.display = 'none';
        quickClearBtn.classList.remove('show');
    }
    
    // Update filter input styles
    updateFilterStyles();
}

function updateFilterStyles() {
    const filters = [
        { element: document.getElementById('timeFilter'), default: 'all' },
        { element: document.getElementById('priceFilter'), default: 'all' },
        { element: document.getElementById('statusFilter'), default: 'all' },
        { element: document.getElementById('sortFilter'), default: 'date-desc' },
        { element: document.getElementById('searchFilter'), default: '' }
    ];
    
    filters.forEach(filter => {
        const isActive = filter.element.value !== filter.default;
        const filterGroup = filter.element.closest('.filter-group');
        
        if (isActive) {
            filter.element.classList.add('has-value');
            filterGroup.classList.add('active');
        } else {
            filter.element.classList.remove('has-value');
            filterGroup.classList.remove('active');
        }
    });
}

function showFilterSummary() {
    // This function could show a summary of active filters when collapsed
    // For now, we'll just update the active filters count
    updateActiveFiltersCount();
}

// Update the existing clearFilters function
function clearFilters() {
    document.getElementById('timeFilter').value = 'all';
    document.getElementById('priceFilter').value = 'all';
    document.getElementById('statusFilter').value = 'all';
    document.getElementById('sortFilter').value = 'date-desc';
    document.getElementById('searchFilter').value = '';
    
    filteredOrders = [...sampleOrders];
    renderOrders(filteredOrders);
    updateActiveFiltersCount();
}

// Update the existing applyFilters function to include the count update
function applyFilters() {
    const timeFilter = document.getElementById('timeFilter').value;
    const priceFilter = document.getElementById('priceFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;
    const sortFilter = document.getElementById('sortFilter').value;
    const searchFilter = document.getElementById('searchFilter').value.toLowerCase();

    let filtered = [...sampleOrders];

    // Apply filters (existing code remains the same)
    if (timeFilter !== 'all') {
        const now = new Date();
        const filterDate = new Date();
        
        switch(timeFilter) {
            case 'week': filterDate.setDate(now.getDate() - 7); break;
            case 'month': filterDate.setDate(now.getDate() - 30); break;
            case '3months': filterDate.setMonth(now.getMonth() - 3); break;
            case 'year': filterDate.setFullYear(now.getFullYear() - 1); break;
        }
        
        filtered = filtered.filter(order => new Date(order.date) >= filterDate);
    }

    if (priceFilter !== 'all') {
        const [min, max] = priceFilter.split('-').map(p => p === '+' ? Infinity : parseFloat(p));
        filtered = filtered.filter(order => {
            return max ? (order.total >= min && order.total <= max) : order.total >= min;
        });
    }

    if (statusFilter !== 'all') {
        filtered = filtered.filter(order => order.status === statusFilter);
    }

    if (searchFilter) {
        filtered = filtered.filter(order => 
            order.restaurant.toLowerCase().includes(searchFilter) ||
            order.items.some(item => item.name.toLowerCase().includes(searchFilter))
        );
    }

    // Apply sorting (existing code remains the same)
    filtered.sort((a, b) => {
        switch(sortFilter) {
            case 'date-desc': return new Date(b.date) - new Date(a.date);
            case 'date-asc': return new Date(a.date) - new Date(b.date);
            case 'price-desc': return b.total - a.total;
            case 'price-asc': return a.total - b.total;
            case 'items-desc': 
                const aItems = a.items.reduce((sum, item) => sum + item.quantity, 0);
                const bItems = b.items.reduce((sum, item) => sum + item.quantity, 0);
                return bItems - aItems;
            default: return 0;
        }
    });

    filteredOrders = filtered;
    renderOrders(filteredOrders);
    updateActiveFiltersCount();
}

function showOrderDetails(orderId) {
    const order = sampleOrders.find(o => o.id === orderId);
    if (!order) return;

    const modal = document.getElementById('orderDetailsModal');
    const subtotal = order.total - order.deliveryFee - order.tax;

    // Populate modal content
    document.getElementById('modalOrderTitle').textContent = `${order.restaurant} Order`;
    document.getElementById('modalOrderId').textContent = `Order #${order.id}`;
    document.getElementById('modalOrderDate').textContent = formatDate(order.date);
    document.getElementById('modalOrderStatus').textContent = capitalizeFirst(order.status);
    document.getElementById('modalOrderStatus').className = `order-status status-${order.status}`;

    // Populate order items
    const itemsList = document.getElementById('orderItemsList');
    itemsList.innerHTML = order.items.map(item => `
                <div class="order-item-row">
                    <div class="item-image-container">
                        <img src="${item.image}" alt="${item.name}" class="item-image">
                    </div>
                    <div class="item-details">
                        <h4 class="item-name">${item.name}</h4>
                        <p class="item-price">$${item.price.toFixed(2)}</p>
                    </div>
                    <div class="item-quantity">
                        <span class="quantity-label">Qty: ${item.quantity}</span>
                    </div>
                    <div class="item-total">
                        <span class="total-price">$${(item.price * item.quantity).toFixed(2)}</span>
                    </div>
                </div>
            `).join('');

    // Populate summary
    document.getElementById('modalSubtotal').textContent = `$${subtotal.toFixed(2)}`;
    document.getElementById('modalDeliveryFee').textContent = `$${order.deliveryFee.toFixed(2)}`;
    document.getElementById('modalTax').textContent = `$${order.tax.toFixed(2)}`;
    document.getElementById('modalTotal').textContent = `$${order.total.toFixed(2)}`;

    modal.style.display = 'flex';
}

function closeOrderDetails() {
    document.getElementById('orderDetailsModal').style.display = 'none';
}

function applyFilters() {
    const timeFilter = document.getElementById('timeFilter').value;
    const priceFilter = document.getElementById('priceFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;
    const sortFilter = document.getElementById('sortFilter').value;
    const searchFilter = document.getElementById('searchFilter').value.toLowerCase();

    let filtered = [...sampleOrders];

    // Apply filters
    if (timeFilter !== 'all') {
        const now = new Date();
        const filterDate = new Date();

        switch (timeFilter) {
            case 'week': filterDate.setDate(now.getDate() - 7); break;
            case 'month': filterDate.setDate(now.getDate() - 30); break;
            case '3months': filterDate.setMonth(now.getMonth() - 3); break;
            case 'year': filterDate.setFullYear(now.getFullYear() - 1); break;
        }

        filtered = filtered.filter(order => new Date(order.date) >= filterDate);
    }

    if (priceFilter !== 'all') {
        const [min, max] = priceFilter.split('-').map(p => p === '+' ? Infinity : parseFloat(p));
        filtered = filtered.filter(order => {
            return max ? (order.total >= min && order.total <= max) : order.total >= min;
        });
    }

    if (statusFilter !== 'all') {
        filtered = filtered.filter(order => order.status === statusFilter);
    }

    if (searchFilter) {
        filtered = filtered.filter(order =>
            order.restaurant.toLowerCase().includes(searchFilter) ||
            order.items.some(item => item.name.toLowerCase().includes(searchFilter))
        );
    }

    // Apply sorting
    filtered.sort((a, b) => {
        switch (sortFilter) {
            case 'date-desc': return new Date(b.date) - new Date(a.date);
            case 'date-asc': return new Date(a.date) - new Date(b.date);
            case 'price-desc': return b.total - a.total;
            case 'price-asc': return a.total - b.total;
            case 'items-desc':
                const aItems = a.items.reduce((sum, item) => sum + item.quantity, 0);
                const bItems = b.items.reduce((sum, item) => sum + item.quantity, 0);
                return bItems - aItems;
            default: return 0;
        }
    });

    filteredOrders = filtered;
    renderOrders(filteredOrders);
}

function clearFilters() {
    document.getElementById('timeFilter').value = 'all';
    document.getElementById('priceFilter').value = 'all';
    document.getElementById('statusFilter').value = 'all';
    document.getElementById('sortFilter').value = 'date-desc';
    document.getElementById('searchFilter').value = '';

    filteredOrders = [...sampleOrders];
    renderOrders(filteredOrders);
}

function reorderItems() {
    alert('Reorder functionality would redirect to cart with these items');
}

function returnOrder() {
    if (confirm('Are you sure you want to return this order?')) {
        alert('Return request submitted successfully');
        closeOrderDetails();
    }
}

function downloadReceipt() {
    alert('Receipt download would start here');
}

// Utility functions
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });
}

function capitalizeFirst(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
}