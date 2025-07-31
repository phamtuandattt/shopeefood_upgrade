//let addresses = [
//    {
//        id: 1,
//        label: "Home",
//        fullName: "John Doe",
//        streetAddress: "123 Main Street",
//        city: "New York",
//        state: "NY",
//        zipCode: "10001",
//        country: "US",
//        phoneNumber: "+1 (555) 123-4567"
//    },
//    {
//        id: 2,
//        label: "Work",
//        fullName: "John Doe",
//        streetAddress: "456 Business Ave",
//        city: "New York",
//        state: "NY",
//        zipCode: "10002",
//        country: "US",
//        phoneNumber: "+1 (555) 987-6543"
//    },
//    {
//        id: 3,
//        label: "Work",
//        fullName: "John Doe",
//        streetAddress: "456 Business Ave",
//        city: "New York",
//        state: "NY",
//        zipCode: "10002",
//        country: "US",
//        phoneNumber: "+1 (555) 987-6543"
//    }
//];

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
                        <h4>${address.addressType}</h4>
                        <div class="address-actions">
                            <button class="btn-icon" onclick="editAddress(${address.addressId})" title="Edit">
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
                                    <path d="M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04c.39-.39.39-1.02 0-1.41l-2.34-2.34c-.39-.39-1.02-.39-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z"/>
                                </svg>
                            </button>
                            <button class="btn-icon btn-delete" onclick="deleteAddress(${address.addressId})" title="Delete">
                                <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
                                    <path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z"/>
                                </svg>
                            </button>
                        </div>
                    </div>
                    <div class="address-details">
                        <p><strong>${address.addressName}</strong></p>
                        <p>${address.street}</p>
                        ${address.addressPhoneNumber ? `<p>Phone: ${address.addressPhoneNumber}</p>` : ''}
                    </div>
                `;
        addressList.appendChild(addressCard);
    });
}
//<p><strong>${address.AddressName}</strong></p>
//<p>${address.Street}</p>
//<p>${address.city}, ${address.state} ${address.zipCode}</p>
//<p>${address.country}</p>
//${ address.AddressPhoneNumber ? `<p>Phone: ${address.AddressPhoneNumber}</p>` : '' }

function showAddAddressForm() {
    editingAddressId = null;
    document.getElementById('formTitle').textContent = 'Add New Address';
    document.getElementById('addressForm').style.display = 'block';
    document.getElementById('addressFormElement').reset();
}

function editAddress(id) {
    const address = addresses.find(addr => addr.AddressId === id);
    if (address) {
        editingAddressId = id;
        document.getElementById('formTitle').textContent = 'Edit Address';
        document.getElementById('addressForm').style.display = 'block';

        // Populate form with existing data
        document.getElementById('addressLabel').value = address.AddressType;
        document.getElementById('fullName').value = address.AddressName;
        document.getElementById('streetAddress').value = address.Street;
        //document.getElementById('city').value = address.city;
        //document.getElementById('state').value = address.state;
        //document.getElementById('zipCode').value = address.zipCode;
        //document.getElementById('country').value = address.country;
        document.getElementById('phoneNumber').value = address.AddressPhoneNumber || '';
    }
}

function deleteAddress(id) {
    if (confirm('Are you sure you want to delete this address?')) {
        addresses = addresses.filter(addr => addr.AddressId !== id);
        renderAddresses();
    }
}

function saveAddress() {
    const formData = {
        AddressType: document.getElementById('addressLabel').value,
        AddressName: document.getElementById('fullName').value,
        Street: document.getElementById('streetAddress').value,
        //city: document.getElementById('city').value,
        //state: document.getElementById('state').value,
        //zipCode: document.getElementById('zipCode').value,
        //country: document.getElementById('country').value,
        AddressPhoneNumber: document.getElementById('phoneNumber').value
    };

    if (editingAddressId) {
        // Update existing address
        const index = addresses.findIndex(addr => addr.id === editingAddressId);
        if (index !== -1) {
            addresses[index] = { ...addresses[index], ...formData };
        }
    }
    else {
        const apiUrl = '/add-customer-address';

        $.ajax({
            url: apiUrl,
            type: "POST",
            data: formData,
            beforeSend: function () {
                document.querySelector('.loader-redirect-overlay').style.display = 'flex';
            },
            complete: function () {
                document.querySelector('.loader-redirect-overlay').style.display = 'none';
            },
            success: function (response) {
                if (response != null) {
                    // Add new address
                    //const newAddress = {
                    //    id: Date.now(), // Simple ID generation
                    //    ...formData
                    //};
                    console.log(response);
                    addresses.push(response);
                    renderAddresses();
                }
                console.log(response);
            }
        });
    }

    cancelAddressForm();
}

function cancelAddressForm() {
    document.getElementById('addressForm').style.display = 'none';
    document.getElementById('addressFormElement').reset();
    editingAddressId = null;
}