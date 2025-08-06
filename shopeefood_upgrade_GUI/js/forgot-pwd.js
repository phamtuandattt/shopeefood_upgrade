
// Form elements
const loginForm = document.getElementById('forgotPwdForm');
const emailInput = document.getElementById('email');
const emailError = document.getElementById('emailError');

// Initialize form
document.addEventListener('DOMContentLoaded', function() {
    initializeForm();
});

function initializeForm() {
    loginForm.addEventListener('submit', handleForgotPwd);

    // Add focus effects
    addFocusEffects();
}

// Handle form submission
async function handleForgotPwd(event) {
    event.preventDefault();
    
    const email = emailInput.value.trim();
    if (email === '') {
        emailInput.focus();
        return false;
    }

    
    // Show loading state
    setLoadingState(true);
    
    console.log("OK");
}

// Set loading state
function setLoadingState(isLoading) {
    const btnText = loginBtn.querySelector('.btn-text');
    const btnSpinner = loginBtn.querySelector('.btn-spinner');
    
    if (isLoading) {
        loginBtn.disabled = true;
        btnText.style.display = 'none';
        btnSpinner.style.display = 'block';
        loginBtn.style.cursor = 'not-allowed';
    } else {
        loginBtn.disabled = false;
        btnText.style.display = 'block';
        btnSpinner.style.display = 'none';
        loginBtn.style.cursor = 'pointer';
    }
}

// Add focus effects
function addFocusEffects() {
    const inputs = [emailInput];
    
    inputs.forEach(input => {
        input.addEventListener('focus', function() {
            this.closest('.input-wrapper').style.transform = 'translateY(-1px)';
        });
        
        input.addEventListener('blur', function() {
            this.closest('.input-wrapper').style.transform = 'translateY(0)';
        });
    });
}
