let addresses = [
    {
        id: 1,
        label: "Home",
        fullName: "John Doe",
        streetAddress: "123 Main Street",
        city: "New York",
        state: "NY",
        zipCode: "10001",
        country: "US",
        phoneNumber: "+1 (555) 123-4567"
    },
    {
        id: 2,
        label: "Work",
        fullName: "John Doe",
        streetAddress: "456 Business Ave",
        city: "New York",
        state: "NY",
        zipCode: "10002",
        country: "US",
        phoneNumber: "+1 (555) 987-6543"
    },
    {
        id: 3,
        label: "Work",
        fullName: "John Doe",
        streetAddress: "456 Business Ave",
        city: "New York",
        state: "NY",
        zipCode: "10002",
        country: "US",
        phoneNumber: "+1 (555) 987-6543"
    }
];

let editingAddressId = null;

// Initialize the page
document.addEventListener('DOMContentLoaded', function () {
    renderAddresses();

    // Form submission handler
    document.getElementById('addressFormElement').addEventListener('submit', function (e) {
        e.preventDefault();
        saveAddress();
    });
});

function renderAddresses() {
    const addressList = document.getElementById('addressList');
    addressList.innerHTML = '';

    addresses.forEach(address => {
        const addressCard = document.createElement('div');
        addressCard.className = 'address-card';
        addressCard.innerHTML = `
                    <div class="address-header">
                        <h4>${address.label}</h4>
                        <div class="address-actions">
                            <button class="btn-icon" onclick="editAddress(${address.id})" title="Edit">
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
                                    <path d="M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04c.39-.39.39-1.02 0-1.41l-2.34-2.34c-.39-.39-1.02-.39-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z"/>
                                </svg>
                            </button>
                            <button class="btn-icon btn-delete" onclick="deleteAddress(${address.id})" title="Delete">
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
                                    <path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"/>
                                </svg>
                            </button>
                        </div>
                    </div>
                    <div class="address-details">
                        <p><strong>${address.fullName}</strong></p>
                        <p>${address.streetAddress}</p>
                        <p>${address.city}, ${address.state} ${address.zipCode}</p>
                        <p>${address.country}</p>
                        ${address.phoneNumber ? `<p>Phone: ${address.phoneNumber}</p>` : ''}
                    </div>
                `;
        addressList.appendChild(addressCard);
    });
}

function showAddAddressForm() {
    editingAddressId = null;
    document.getElementById('formTitle').textContent = 'Add New Address';
    document.getElementById('addressForm').style.display = 'block';
    document.getElementById('addressFormElement').reset();
}

function editAddress(id) {
    const address = addresses.find(addr => addr.id === id);
    if (address) {
        editingAddressId = id;
        document.getElementById('formTitle').textContent = 'Edit Address';
        document.getElementById('addressForm').style.display = 'block';

        // Populate form with existing data
        document.getElementById('addressLabel').value = address.label;
        document.getElementById('fullName').value = address.fullName;
        document.getElementById('streetAddress').value = address.streetAddress;
        document.getElementById('city').value = address.city;
        document.getElementById('state').value = address.state;
        document.getElementById('zipCode').value = address.zipCode;
        document.getElementById('country').value = address.country;
        document.getElementById('phoneNumber').value = address.phoneNumber || '';
    }
}

function deleteAddress(id) {
    if (confirm('Are you sure you want to delete this address?')) {
        addresses = addresses.filter(addr => addr.id !== id);
        renderAddresses();
    }
}

function saveAddress() {
    const formData = {
        label: document.getElementById('addressLabel').value,
        fullName: document.getElementById('fullName').value,
        streetAddress: document.getElementById('streetAddress').value,
        city: document.getElementById('city').value,
        state: document.getElementById('state').value,
        zipCode: document.getElementById('zipCode').value,
        country: document.getElementById('country').value,
        phoneNumber: document.getElementById('phoneNumber').value
    };

    if (editingAddressId) {
        // Update existing address
        const index = addresses.findIndex(addr => addr.id === editingAddressId);
        if (index !== -1) {
            addresses[index] = { ...addresses[index], ...formData };
        }
    } else {
        // Add new address
        const newAddress = {
            id: Date.now(), // Simple ID generation
            ...formData
        };
        addresses.push(newAddress);
    }

    renderAddresses();
    cancelAddressForm();
}

function cancelAddressForm() {
    document.getElementById('addressForm').style.display = 'none';
    document.getElementById('addressFormElement').reset();
    editingAddressId = null;
}

// POPUP MESSAGE SYSTEM JAVASCRIPT
// Main function to show popup messages
function showPopupMessage(type, title, message, options = {}) {
    const container = document.getElementById('popup-container');
    
    // Default options
    const defaultOptions = {
        showCloseButton: true,
        autoClose: false,
        autoCloseDelay: 3000,
        showFooter: false,
        buttons: [],
        size: 'normal',
        customIcon: null,
        onClose: null,
        onConfirm: null,
        closeAnimation: 'default' // 'default', 'slide-up', 'slide-down', 'fade-scale', 'rotate-out'
    };
    
    const config = { ...defaultOptions, ...options };
    
    // Create popup HTML
    const popupHTML = `
        <div class="popup-message ${config.size} ${type}">
            ${config.showCloseButton ? '<button class="popup-close" onclick="closePopup()">×</button>' : ''}
            
            <div class="popup-header">
                <div class="popup-icon-container ${type}">
                    ${config.customIcon || getPopupIcon(type)}
                </div>
                <div class="popup-content">
                    <h3 class="popup-title">${title}</h3>
                    <p class="popup-message-text">${message}</p>
                </div>
            </div>
            
            ${config.showFooter ? createPopupFooter(config.buttons) : ''}
            ${type === 'loading' ? '<div class="popup-progress"><div class="popup-progress-bar"></div></div>' : ''}
            ${config.autoClose ? '<div class="popup-auto-dismiss"></div>' : ''}
        </div>
    `;
    
    // Show popup with smooth animation
    container.innerHTML = popupHTML;
    container.classList.add('show');
    
    // Trigger show animation
    setTimeout(() => {
        const popup = container.querySelector('.popup-message');
        if (popup) {
            popup.classList.add('show');
        }
    }, 10);
    
    // Handle auto close
    if (config.autoClose) {
        setTimeout(() => {
            closePopup(config.closeAnimation);
            if (config.onClose) config.onClose();
        }, config.autoCloseDelay);
    }
    
    // Handle loading progress
    if (type === 'loading') {
        animateProgress();
    }
    
    // Store config for button handlers
    window.currentPopupConfig = config;
    
    // Prevent body scroll
    document.body.style.overflow = 'hidden';
}

// Enhanced close popup function with smooth animations
function closePopup(animationType = 'default') {
    const container = document.getElementById('popup-container');
    const popup = container.querySelector('.popup-message');
    
    if (!popup) return;
    
    // Add closing class with animation type
    container.classList.add('closing');
    popup.classList.add('closing');
    
    if (animationType !== 'default') {
        popup.classList.add(animationType);
    }
    
    // Wait for animation to complete
    setTimeout(() => {
        container.classList.remove('show', 'closing');
        container.innerHTML = '';
        document.body.style.overflow = '';
        
        // Call onClose callback if exists
        if (window.currentPopupConfig && window.currentPopupConfig.onClose) {
            window.currentPopupConfig.onClose();
        }
        
        // Clean up config
        window.currentPopupConfig = null;
    }, 400); // Match the CSS transition duration
}

// Close with specific animation
function closePopupWithAnimation(animationType) {
    closePopup(animationType);
}

// Get appropriate icon for popup type
function getPopupIcon(type) {
    const icons = {
        success: `
            <svg class="popup-icon" viewBox="0 0 24 24" fill="currentColor">
                <path d="M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z"/>
            </svg>
        `,
        error: `
            <svg class="popup-icon" viewBox="0 0 24 24" fill="currentColor">
                <path d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"/>
            </svg>
        `,
        warning: `
            <svg class="popup-icon" viewBox="0 0 24 24" fill="currentColor">
                <path d="M1 21h22L12 2 1 21zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z"/>
            </svg>
        `,
        info: `
            <svg class="popup-icon" viewBox="0 0 24 24" fill="currentColor">
                <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z"/>
            </svg>
        `,
        loading: `
            <div class="popup-spinner"></div>
        `
    };
    
    return icons[type] || icons.info;
}

// Create footer with buttons
function createPopupFooter(buttons) {
    if (!buttons.length) return '';
    
    const buttonHTML = buttons.map(btn => `
        <button class="popup-btn ${btn.type || 'secondary'}" onclick="${btn.action || 'closePopup()'}">
            ${btn.text}
        </button>
    `).join('');
    
    return `<div class="popup-footer">${buttonHTML}</div>`;
}

// Animate progress bar for loading
function animateProgress() {
    const progressBar = document.querySelector('.popup-progress-bar');
    if (progressBar) {
        let width = 0;
        const interval = setInterval(() => {
            width += Math.random() * 15;
            if (width >= 100) {
                width = 100;
                clearInterval(interval);
            }
            progressBar.style.width = width + '%';
        }, 200);
    }
}

// Enhanced demo functions with different close animations
function showCustomPopup() {
    showPopupMessage('info', 'Custom Popup', 'This popup will close with a slide-up animation.', {
        showFooter: true,
        buttons: [
            { text: 'Cancel', type: 'secondary', action: 'closePopup("slide-up")' },
            { text: 'Confirm', type: 'primary', action: 'handleConfirm()' }
        ],
        size: 'large',
        closeAnimation: 'slide-up'
    });
}

function showConfirmationPopup() {
    showPopupMessage('warning', 'Confirm Action', 'This popup will close with a rotate animation.', {
        showFooter: true,
        buttons: [
            { text: 'Cancel', type: 'secondary', action: 'closePopup("rotate-out")' },
            { text: 'Delete', type: 'danger', action: 'handleDelete()' }
        ],
        customIcon: `
            <svg class="popup-icon" viewBox="0 0 24 24" fill="currentColor">
                <path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"/>
            </svg>
        `,
        closeAnimation: 'rotate-out'
    });
}

function showProgressPopup() {
    showPopupMessage('loading', 'Processing', 'Please wait while we process your request...', {
        showCloseButton: false,
        autoClose: true,
        autoCloseDelay: 4000,
        closeAnimation: 'fade-scale',
        onClose: () => {
            showPopupMessage('success', 'Complete', 'Your request has been processed successfully!', {
                autoClose: true,
                autoCloseDelay: 3000,
                closeAnimation: 'slide-down'
            });
        }
    });
}

function handleConfirm() {
    closePopup('fade-scale');
    setTimeout(() => {
        showPopupMessage('success', 'Confirmed', 'Your action has been confirmed successfully!', {
            autoClose: true,
            autoCloseDelay: 3000,
            closeAnimation: 'slide-up'
        });
    }, 400);
}

function handleDelete() {
    closePopup('rotate-out');
    setTimeout(() => {
        showPopupMessage('success', 'Deleted', 'The item has been successfully deleted.', {
            autoClose: true,
            autoCloseDelay: 3000,
            closeAnimation: 'fade-scale'
        });
    }, 400);
}

// Enhanced utility functions with smooth animations
function showSuccessPopup(title, message, autoClose = true) {
    showPopupMessage('success', title, message, { 
        autoClose, 
        autoCloseDelay: 3000,
        closeAnimation: 'slide-up'
    });
}

function showErrorPopup(title, message, autoClose = false) {
    showPopupMessage('error', title, message, { 
        autoClose,
        closeAnimation: 'slide-down'
    });
}

function showInfoPopup(title, message, autoClose = true) {
    showPopupMessage('info', title, message, { 
        autoClose, 
        autoCloseDelay: 4000,
        closeAnimation: 'fade-scale'
    });
}

function showWarningPopup(title, message, autoClose = false) {
    showPopupMessage('warning', title, message, { 
        autoClose,
        closeAnimation: 'rotate-out'
    });
}

function showLoadingPopup(title, message) {
    showPopupMessage('loading', title, message, { 
        showCloseButton: false,
        autoClose: false,
        closeAnimation: 'fade-scale'
    });
}

// Close popup when clicking outside (with smooth animation)
document.addEventListener('click', function(event) {
    const container = document.getElementById('popup-container');
    if (event.target === container && container.classList.contains('show')) {
        closePopup('fade-scale');
    }
});

// Close popup with Escape key (with smooth animation)
document.addEventListener('keydown', function(event) {
    if (event.key === 'Escape') {
        const container = document.getElementById('popup-container');
        if (container.classList.contains('show')) {
            closePopup('slide-up');
        }
    }
});

// Prevent multiple rapid clicks during animation
let isAnimating = false;

function closePopupSafe(animationType = 'default') {
    if (isAnimating) return;
    
    isAnimating = true;
    closePopup(animationType);
    
    setTimeout(() => {
        isAnimating = false;
    }, 500);
}

// Enhanced integration function
function deleteOrderWithPopup(orderId) {
    showPopupMessage('warning', 'Confirm Delete', 'Are you sure you want to delete this order? This action cannot be undone.', {
        showFooter: true,
        buttons: [
            { text: 'Cancel', type: 'secondary', action: 'closePopupSafe("slide-down")' },
            { text: 'Delete', type: 'danger', action: `confirmDeleteOrder('${orderId}')` }
        ],
        closeAnimation: 'rotate-out'
    });
}

function confirmDeleteOrder(orderId) {
    closePopupSafe('rotate-out');
    
    setTimeout(() => {
        // Show loading
        showLoadingPopup('Deleting Order', 'Please wait while we delete your order...');
        
        // Simulate API call
        setTimeout(() => {
            closePopupSafe('fade-scale');
            setTimeout(() => {
                // Simulate success or failure
                if (Math.random() > 0.3) {
                    showSuccessPopup('Delete Success', 'Your order has been successfully deleted.');
                } else {
                    showErrorPopup('Delete Failed', 'Unable to delete the order. Please try again later.');
                }
            }, 400);
        }, 2000);
    }, 400);
}
