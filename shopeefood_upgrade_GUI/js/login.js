// LOGIN FORM VALIDATION AND FUNCTIONALITY

// Email validation regex
const emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

// Password requirements
const passwordRequirements = {
    minLength: 8,
    hasUpperCase: /[A-Z]/,
    hasLowerCase: /[a-z]/,
    hasNumbers: /\d/,
    hasSpecialChar: /[!@#$%^&*(),.?":{}|<>]/
};

// Form elements
const loginForm = document.getElementById('loginForm');
const emailInput = document.getElementById('email');
const passwordInput = document.getElementById('password');
const emailError = document.getElementById('emailError');
const passwordError = document.getElementById('passwordError');
const loginBtn = document.getElementById('loginBtn');
const passwordToggle = document.getElementById('passwordToggle');

// Demo credentials for testing
const demoCredentials = {
    email: 'demo@example.com',
    password: 'Demo123!'
};

// LOGIN FORM VALIDATION AND FUNCTIONALITY (Continued)

// Initialize form
document.addEventListener('DOMContentLoaded', function() {
    initializeForm();
});

function initializeForm() {
    // Add event listeners
    emailInput.addEventListener('input', validateEmail);
    // emailInput.addEventListener('blur', validateEmail);
    passwordInput.addEventListener('input', validatePassword);
    // passwordInput.addEventListener('blur', validatePassword);
    passwordToggle.addEventListener('click', togglePasswordVisibility);
    loginForm.addEventListener('submit', handleLogin);
    
    // Add real-time validation
    emailInput.addEventListener('keyup', debounce(validateEmail, 300));
    passwordInput.addEventListener('keyup', debounce(validatePassword, 300));
    
    // Add focus effects
    addFocusEffects();
    
    // Show demo credentials hint
    showDemoHint();
}

// Email validation function
function validateEmail() {
    const email = emailInput.value.trim();
    const emailGroup = emailInput.closest('.input-group-login-form');
    
    // Clear previous states
    clearValidationState(emailInput, emailError);
    
    if (email === '') {
        if (emailInput === document.activeElement) {
            return true; // Don't show error while typing
        }
        showError(emailInput, emailError, 'Email address is required');
        return false;
    }
    
    if (!emailRegex.test(email)) {
        showError(emailInput, emailError, 'Please enter a valid email address');
        return false;
    }
    
    if (email.length > 254) {
        showError(emailInput, emailError, 'Email address is too long');
        return false;
    }
    
    // Check for common email mistakes
    const commonMistakes = [
        { pattern: /@gmail\.co$/, suggestion: '@gmail.com' },
        { pattern: /@yahoo\.co$/, suggestion: '@yahoo.com' },
        { pattern: /@hotmail\.co$/, suggestion: '@hotmail.com' },
        { pattern: /@outlook\.co$/, suggestion: '@outlook.com' }
    ];
    
    for (let mistake of commonMistakes) {
        if (mistake.pattern.test(email)) {
            showError(emailInput, emailError, `Did you mean ${email.replace(mistake.pattern, mistake.suggestion)}?`);
            return false;
        }
    }
    
    showSuccess(emailInput);
    return true;
}

// Password validation function
function validatePassword() {
    const password = passwordInput.value;
    const passwordGroup = passwordInput.closest('.input-group-login-form');
    const toggelePwd = passwordInput.closest('.input-group-login-form').querySelector('.password-toggle');
    
    // Clear previous states
    clearValidationState(passwordInput, passwordError);
    
    if (password === '') {
        if (passwordInput === document.activeElement 
            || toggelePwd === document.activeElement
        ) {
            return true; // Don't show error while typing
        }
        showError(passwordInput, passwordError, 'Password is required');
        return false;
    }
    
    // Check minimum length
    if (password.length < passwordRequirements.minLength) {
        showError(passwordInput, passwordError, `Password must be at least ${passwordRequirements.minLength} characters long`);
        return false;
    }
    
    // Check password strength
    const strength = calculatePasswordStrength(password);
    
    if (strength.score < 3) {
        showError(passwordInput, passwordError, `Weak password. ${strength.feedback}`);
        return false;
    }
    
    showSuccess(passwordInput);
    return true;
}

// Calculate password strength
function calculatePasswordStrength(password) {
    let score = 0;
    let feedback = [];
    
    // Length check
    if (password.length >= 8) score++;
    else feedback.push('Use at least 8 characters');
    
    // Character variety checks
    if (passwordRequirements.hasUpperCase.test(password)) score++;
    else feedback.push('Add uppercase letters');
    
    if (passwordRequirements.hasLowerCase.test(password)) score++;
    else feedback.push('Add lowercase letters');
    
    if (passwordRequirements.hasNumbers.test(password)) score++;
    else feedback.push('Add numbers');
    
    if (passwordRequirements.hasSpecialChar.test(password)) score++;
    else feedback.push('Add special characters');
    
    // Additional checks
    if (password.length >= 12) score++;
    if (!/(.)\1{2,}/.test(password)) score++; // No repeated characters
    
    return {
        score: score,
        feedback: feedback.slice(0, 2).join(', ')
    };
}

// Show error state
function showError(input, errorElement, message) {
    input.classList.remove('valid');
    input.classList.add('invalid');
    errorElement.textContent = message;
    errorElement.classList.add('show');
    
    // Update label color
    const label = input.closest('.input-group-login-form').querySelector('.input-label');
    label.style.color = '#f56565';
}

// Show success state
function showSuccess(input) {
    input.classList.remove('invalid');
    input.classList.add('valid');
    
    const errorElement = input.closest('.input-group-login-form').querySelector('.error-message');
    errorElement.classList.remove('show');
    
    // Update label color
    const label = input.closest('.input-group-login-form').querySelector('.input-label');
    label.style.color = '#48bb78';
}

// Clear validation state
function clearValidationState(input, errorElement) {
    input.classList.remove('valid', 'invalid');
    errorElement.classList.remove('show');
    
    // Reset label color
    const label = input.closest('.input-group-login-form').querySelector('.input-label');
    label.style.color = '#4a5568';
}

// Toggle password visibility
function togglePasswordVisibility() {
    const eyeOpen = passwordToggle.querySelector('.eye-open');
    const eyeClosed = passwordToggle.querySelector('.eye-closed');
    
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        eyeOpen.style.display = 'none';
        eyeClosed.style.display = 'block';
        passwordToggle.setAttribute('aria-label', 'Hide password');
    } else {
        passwordInput.type = 'password';
        eyeOpen.style.display = 'block';
        eyeClosed.style.display = 'none';
        passwordToggle.setAttribute('aria-label', 'Show password');
    }
}

// Handle form submission
async function handleLogin(event) {
    event.preventDefault();
    
    // Validate all fields
    const isEmailValid = validateEmail();
    const isPasswordValid = validatePassword();
    
    if (!isEmailValid || !isPasswordValid) {
        showPopupMessage('error', 'Validation Error', 'Please fix the errors above and try again.');
        return;
    }
    
    // Show loading state
    setLoadingState(true);
    
    try {
        // Simulate API call
        const result = await simulateLogin();
        
        if (result.success) {
            showPopupMessage('success', 'Login Successful', 'Welcome back! Redirecting to dashboard...', {
                autoClose: true,
                autoCloseDelay: 2000,
                onClose: () => {
                    // Redirect to dashboard
                    window.location.href = '/dashboard';
                }
            });
        } else {
            showPopupMessage('error', 'Login Failed', result.message);
        }
        
    } catch (error) {
        showPopupMessage('error', 'Connection Error', 'Unable to connect to server. Please try again.');
    } finally {
        setLoadingState(false);
    }
}

// Simulate login API call
function simulateLogin() {
    return new Promise((resolve) => {
        const email = emailInput.value.trim();
        const password = passwordInput.value;
        
        setTimeout(() => {
            // Check demo credentials
            if (email === demoCredentials.email && password === demoCredentials.password) {
                resolve({ success: true });
            } else if (email === demoCredentials.email) {
                resolve({ success: false, message: 'Incorrect password. Please try again.' });
            } else {
                // Simulate different error scenarios
                const random = Math.random();
                if (random < 0.3) {
                    resolve({ success: false, message: 'Account not found. Please check your email address.' });
                } else if (random < 0.6) {
                    resolve({ success: false, message: 'Incorrect password. Please try again.' });
                } else if (random < 0.8) {
                    resolve({ success: false, message: 'Account is temporarily locked. Please try again later.' });
                } else {
                    resolve({ success: true });
                }
            }
        }, 2000); // Simulate network delay
    });
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
    const inputs = [emailInput, passwordInput];
    
    inputs.forEach(input => {
        input.addEventListener('focus', function() {
            this.closest('.input-wrapper').style.transform = 'translateY(-1px)';
            this.closest('.input-group-login-form').querySelector('.input-label').style.transform = 'translateY(-2px)';
        });
        
        input.addEventListener('blur', function() {
            this.closest('.input-wrapper').style.transform = 'translateY(0)';
            this.closest('.input-group-login-form').querySelector('.input-label').style.transform = 'translateY(0)';
        });
    });
}

// Show demo credentials hint
function showDemoHint() {
    setTimeout(() => {
        showPopupMessage('info', 'Demo Credentials', 
            `You can use these demo credentials to test the login:<br>
            <strong>Email:</strong> ${demoCredentials.email}<br>
            <strong>Password:</strong> ${demoCredentials.password}`, {
            autoClose: true,
            autoCloseDelay: 8000,
            size: 'large'
        });
    }, 1000);
}

// Debounce function for performance
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// // Social login functions
// function loginWithGoogle() {
//     showPopupMessage('info', 'Google Login', 'Redirecting to Google authentication...', {
//         autoClose: true,
//         autoCloseDelay: 2000
//     });
    
//     // Simulate Google OAuth
//     setTimeout(() => {
//         // In real implementation, redirect to Google OAuth
//         console.log('Redirecting to Google OAuth...');
//     }, 2000);
// }

// function loginWithFacebook() {
//     showPopupMessage('info', 'Facebook Login', 'Redirecting to Facebook authentication...', {
//         autoClose: true,
//         autoCloseDelay: 2000
//     });
    
//     // Simulate Facebook OAuth
//     setTimeout(() => {
//         // In real implementation, redirect to Facebook OAuth
//         console.log('Redirecting to Facebook OAuth...');
//     }, 2000);
// }

// // Show signup form
// function showSignupForm() {
//     showPopupMessage('info', 'Sign Up', 'Redirecting to registration page...', {
//         autoClose: true,
//         autoCloseDelay: 1500,
//         onClose: () => {
//             // Redirect to signup page
//             window.location.href = '/signup';
//         }
//     });
// }

// Auto-fill demo credentials (for testing)
function fillDemoCredentials() {
    emailInput.value = demoCredentials.email;
    passwordInput.value = demoCredentials.password;
    
    // Trigger validation
    validateEmail();
    validatePassword();
    
    showPopupMessage('success', 'Demo Filled', 'Demo credentials have been filled in the form.', {
        autoClose: true,
        autoCloseDelay: 2000
    });
}

// Keyboard shortcuts
document.addEventListener('keydown', function(event) {
    // Ctrl/Cmd + D to fill demo credentials
    if ((event.ctrlKey || event.metaKey) && event.key === 'd') {
        event.preventDefault();
        fillDemoCredentials();
    }
    
    // Enter key to submit form when focused on inputs
    if (event.key === 'Enter' && (event.target === emailInput || event.target === passwordInput)) {
        event.preventDefault();
        handleLogin(event);
    }
});

// // Form auto-save (optional)
// function saveFormData() {
//     const rememberMe = document.getElementById('rememberMe').checked;
    
//     if (rememberMe) {
//         localStorage.setItem('loginEmail', emailInput.value);
//     } else {
//         localStorage.removeItem('loginEmail');
//     }
// }

// // Load saved form data
// function loadFormData() {
//     const savedEmail = localStorage.getItem('loginEmail');
//     if (savedEmail) {
//         emailInput.value = savedEmail;
//         document.getElementById('rememberMe').checked = true;
//         validateEmail();
//     }
// }

// // Initialize saved data on load
// document.addEventListener('DOMContentLoaded', function() {
//     loadFormData();
// });

// // Save data when remember me is checked
// document.getElementById('rememberMe').addEventListener('change', function() {
//     if (this.checked) {
//         saveFormData();
//     } else {
//         localStorage.removeItem('loginEmail');
//     }
// });

// // Real-time password strength indicator (optional enhancement)
// function showPasswordStrength() {
//     const password = passwordInput.value;
//     if (password.length === 0) return;
    
//     const strength = calculatePasswordStrength(password);
//     const strengthColors = ['#f56565', '#ed8936', '#ecc94b', '#48bb78', '#38a169'];
//     const strengthTexts = ['Very Weak', 'Weak', 'Fair', 'Good', 'Strong'];
    
//     // You can add a strength indicator UI here
//     console.log(`Password strength: ${strengthTexts[Math.min(strength.score, 4)]} (${strength.score}/5)`);
// }

// // Enhanced error handling for network issues
// window.addEventListener('online', function() {
//     showPopupMessage('success', 'Connection Restored', 'Internet connection has been restored.', {
//         autoClose: true,
//         autoCloseDelay: 3000
//     });
// });

// window.addEventListener('offline', function() {
//     showPopupMessage('warning', 'Connection Lost', 'Please check your internet connection and try again.', {
//         autoClose: false
//     });
// });

// // Form analytics (optional)
// function trackFormInteraction(action, field) {
//     // In real implementation, send to analytics service
//     console.log(`Form interaction: ${action} on ${field}`);
// }

// // Add analytics tracking
// emailInput.addEventListener('focus', () => trackFormInteraction('focus', 'email'));
// passwordInput.addEventListener('focus', () => trackFormInteraction('focus', 'password'));
// loginForm.addEventListener('submit', () => trackFormInteraction('submit', 'form'));

// // Accessibility improvements
// function improveAccessibility() {
//     // Add ARIA labels
//     emailInput.setAttribute('aria-describedby', 'emailError');
//     passwordInput.setAttribute('aria-describedby', 'passwordError');
    
//     // Add live regions for screen readers
//     emailError.setAttribute('aria-live', 'polite');
//     passwordError.setAttribute('aria-live', 'polite');
    
//     // Add form validation announcements
//     loginForm.setAttribute('novalidate', 'true');
// }

// // Initialize accessibility features
// document.addEventListener('DOMContentLoaded', improveAccessibility);

// // Export functions for testing (if using modules)
// if (typeof module !== 'undefined' && module.exports) {
//     module.exports = {
//         validateEmail,
//         validatePassword,
//         calculatePasswordStrength,
//         simulateLogin
//     };
// }
