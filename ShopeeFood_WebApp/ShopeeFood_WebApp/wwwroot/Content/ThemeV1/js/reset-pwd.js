// RESET PASSWORD FORM VALIDATION AND FUNCTIONALITY

// Password requirements configuration
const passwordRequirements = {
    minLength: 8,
    maxLength: 128,
    hasUpperCase: /[A-Z]/,
    hasLowerCase: /[a-z]/,
    hasNumbers: /\d/,
    hasSpecialChar: /[!@#$%^&*(),.?":{}|<>]/,
    noRepeatedChars: /^(?!.*(.)\1{2,})/,
    noSequential: /^(?!.*(?:abc|bcd|cde|def|efg|fgh|ghi|hij|ijk|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz|123|234|345|456|567|678|789|890))/i
};

// Common weak passwords to avoid
const weakPasswords = [
    'password', '123456', '123456789', 'qwerty', 'abc123', 'password123',
    'admin', 'letmein', 'welcome', 'monkey', '1234567890', 'password1'
];

// Form elements
const resetForm = document.getElementById('resetPasswordForm');
const newPasswordInput = document.getElementById('newPassword');
const confirmPasswordInput = document.getElementById('confirmPassword');
const newPasswordError = document.getElementById('newPasswordError');
const confirmPasswordError = document.getElementById('confirmPasswordError');
const resetBtn = document.getElementById('resetBtn');
const newPasswordToggle = document.getElementById('newPasswordToggle');
const confirmPasswordToggle = document.getElementById('confirmPasswordToggle');
const passwordStrength = document.getElementById('passwordStrength');
const strengthFill = document.getElementById('strengthFill');
const strengthText = document.getElementById('strengthText');
const strengthRequirements = document.getElementById('strengthRequirements');
const passwordMatch = document.getElementById('passwordMatch');

// State management
let passwordValidationState = {
    isNewPasswordValid: false,
    isConfirmPasswordValid: false,
    passwordsMatch: false,
    strengthScore: 0
};

// Initialize form
document.addEventListener('DOMContentLoaded', function () {
    initializeResetForm();
});

function initializeResetForm() {
    // Add event listeners
    newPasswordInput.addEventListener('input', handleNewPasswordInput);
    //newPasswordInput.addEventListener('blur', validateNewPassword);
    confirmPasswordInput.addEventListener('input', handleConfirmPasswordInput);
    //confirmPasswordInput.addEventListener('blur', validateConfirmPassword);

    // Password toggle functionality
    newPasswordToggle.addEventListener('click', () => togglePasswordVisibility(newPasswordInput, newPasswordToggle));
    confirmPasswordToggle.addEventListener('click', () => togglePasswordVisibility(confirmPasswordInput, confirmPasswordToggle));

    // Form submission
    resetForm.addEventListener('submit', handlePasswordReset);

    // Add real-time validation with debouncing
    newPasswordInput.addEventListener('keyup', debounce(validateNewPassword, 300));
    confirmPasswordInput.addEventListener('keyup', debounce(validatePasswordMatch, 300));

    // Add focus effects
    addFocusEffects();

    // Initialize accessibility features
    initializeAccessibility();

    // Show initial instructions
    // showInitialInstructions();
}

// Handle new password input
function handleNewPasswordInput() {
    const password = newPasswordInput.value;

    // Show strength indicator when user starts typing
    if (password.length > 0) {
        //passwordStrength.classList.add('show');
        updatePasswordStrength(password);
        updatePasswordRequirements(password);
    } else {
        //passwordStrength.classList.remove('show');
        clearValidationState(newPasswordInput, newPasswordError);
    }

    // Validate confirm password if it has content
    if (confirmPasswordInput.value.length > 0) {
        validatePasswordMatch();
    }
}

// Handle confirm password input
function handleConfirmPasswordInput() {
    const confirmPassword = confirmPasswordInput.value;

    if (confirmPassword.length > 0) {
        validatePasswordMatch();
    } else {
        clearValidationState(confirmPasswordInput, confirmPasswordError);
        passwordMatch.classList.remove('show');
    }
}

// Validate new password
function validateNewPassword() {
    const password = newPasswordInput.value;

    // Clear previous states
    clearValidationState(newPasswordInput, newPasswordError);

    if (password === '') {
        if (newPasswordInput === document.activeElement) {
            return false;
        }
        showError(newPasswordInput, newPasswordError, 'New password is required');
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check minimum length
    if (password.length < passwordRequirements.minLength) {
        showError(newPasswordInput, newPasswordError, `Password must be at least ${passwordRequirements.minLength} characters long`);
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check maximum length
    if (password.length > passwordRequirements.maxLength) {
        showError(newPasswordInput, newPasswordError, `Password must not exceed ${passwordRequirements.maxLength} characters`);
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check for weak passwords
    if (weakPasswords.includes(password.toLowerCase())) {
        showError(newPasswordInput, newPasswordError, 'This password is too common. Please choose a stronger password');
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check password strength
    const strength = calculatePasswordStrength(password);

    if (strength.score < 4) {
        showError(newPasswordInput, newPasswordError, `Password is too weak. ${strength.feedback}`);
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check for sequential characters
    if (!passwordRequirements.noSequential.test(password)) {
        showError(newPasswordInput, newPasswordError, 'Avoid sequential characters (abc, 123, etc.)');
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    // Check for repeated characters
    if (!passwordRequirements.noRepeatedChars.test(password)) {
        showError(newPasswordInput, newPasswordError, 'Avoid repeating the same character multiple times');
        passwordValidationState.isNewPasswordValid = false;
        return false;
    }

    showSuccess(newPasswordInput);
    passwordValidationState.isNewPasswordValid = true;
    return true;
}

// Validate confirm password and check match
function validateConfirmPassword() {
    const confirmPassword = confirmPasswordInput.value;

    // Clear previous states
    clearValidationState(confirmPasswordInput, confirmPasswordError);
    passwordMatch.classList.remove('show');

    if (confirmPassword === '') {
        if (confirmPasswordInput === document.activeElement) {
            return false;
        }
        showError(confirmPasswordInput, confirmPasswordError, 'Please confirm your password');
        passwordValidationState.isConfirmPasswordValid = false;
        return false;
    }

    return validatePasswordMatch();
}

// Validate password match
function validatePasswordMatch() {
    const newPassword = newPasswordInput.value;
    const confirmPassword = confirmPasswordInput.value;

    if (confirmPassword === '') {
        passwordValidationState.passwordsMatch = false;
        return false;
    }

    if (newPassword !== confirmPassword) {
        showError(confirmPasswordInput, confirmPasswordError, 'Passwords do not match');
        confirmPasswordInput.classList.remove('matching');
        passwordMatch.classList.remove('show');
        passwordValidationState.passwordsMatch = false;
        return false;
    }

    // Passwords match
    clearValidationState(confirmPasswordInput, confirmPasswordError);
    confirmPasswordInput.classList.add('matching');
    passwordMatch.classList.add('show');
    passwordValidationState.passwordsMatch = true;
    passwordValidationState.isConfirmPasswordValid = true;

    return true;
}

// Calculate password strength
function calculatePasswordStrength(password) {
    let score = 0;
    let feedback = [];

    // Length scoring
    if (password.length >= 8) score += 1;
    if (password.length >= 12) score += 1;
    if (password.length >= 16) score += 1;

    // Character variety scoring
    if (passwordRequirements.hasUpperCase.test(password)) score += 1;
    else feedback.push('Add uppercase letters');

    if (passwordRequirements.hasLowerCase.test(password)) score += 1;
    else feedback.push('Add lowercase letters');

    if (passwordRequirements.hasNumbers.test(password)) score += 1;
    else feedback.push('Add numbers');

    if (passwordRequirements.hasSpecialChar.test(password)) score += 1;
    else feedback.push('Add special characters');

    // Bonus points
    if (password.length >= 20) score += 1;
    if (/[^\w\s]/.test(password)) score += 1; // Non-alphanumeric characters
    if (!/(.)\1{1,}/.test(password)) score += 1; // No repeated characters

    return {
        score: Math.min(score, 10),
        feedback: feedback.slice(0, 2).join(', ')
    };
}

// Update password strength indicator
function updatePasswordStrength(password) {
    const strength = calculatePasswordStrength(password);
    const percentage = (strength.score / 10) * 100;

    strengthFill.style.width = `${percentage}%`;

    // Update strength text and color
    const strengthLevels = [
        { min: 0, max: 2, text: 'Very Weak', color: '#f56565' },
        { min: 3, max: 4, text: 'Weak', color: '#ed8936' },
        { min: 5, max: 6, text: 'Fair', color: '#ecc94b' },
        { min: 7, max: 8, text: 'Good', color: '#48bb78' },
        { min: 9, max: 10, text: 'Excellent', color: '#38a169' }
    ];

    const currentLevel = strengthLevels.find(level =>
        strength.score >= level.min && strength.score <= level.max
    );

    strengthText.textContent = `Password Strength: ${currentLevel.text}`;
    strengthText.style.color = currentLevel.color;
    strengthFill.style.background = currentLevel.color;

    passwordValidationState.strengthScore = strength.score;
}

// Update password requirements checklist
function updatePasswordRequirements(password) {
    const requirements = [
        { name: 'length', test: password.length >= passwordRequirements.minLength },
        { name: 'uppercase', test: passwordRequirements.hasUpperCase.test(password) },
        { name: 'lowercase', test: passwordRequirements.hasLowerCase.test(password) },
        { name: 'number', test: passwordRequirements.hasNumbers.test(password) },
        { name: 'special', test: passwordRequirements.hasSpecialChar.test(password) }
    ];

    requirements.forEach(req => {
        const element = strengthRequirements.querySelector(`[data-requirement="${req.name}"]`);
        if (element) {
            if (req.test) {
                element.classList.add('met');
            } else {
                element.classList.remove('met');
            }
        }
    });
}

// Toggle password visibility
function togglePasswordVisibility(input, toggle) {
    const eyeOpen = toggle.querySelector('.eye-open');
    const eyeClosed = toggle.querySelector('.eye-closed');

    if (input.type === 'password') {
        input.type = 'text';
        eyeOpen.style.display = 'none';
        eyeClosed.style.display = 'block';
        toggle.setAttribute('aria-label', 'Hide password');
    } else {
        input.type = 'password';
        eyeOpen.style.display = 'block';
        eyeClosed.style.display = 'none';
        toggle.setAttribute('aria-label', 'Show password');
    }
}

// Show error state
function showError(input, errorElement, message) {
    input.classList.remove('valid', 'matching');
    input.classList.add('invalid');
    errorElement.textContent = message;
    errorElement.classList.add('show');

    // Update label color
    const label = input.closest('.input-group-reset-pwd').querySelector('.input-label');
    label.style.color = '#f56565';

    // Add shake animation
    input.style.animation = 'shake 0.5s ease-in-out';
    setTimeout(() => {
        input.style.animation = '';
    }, 500);
}

// Show success state
function showSuccess(input) {
    input.classList.remove('invalid');
    input.classList.add('valid');

    const errorElement = input.closest('.input-group-reset-pwd').querySelector('.error-message');
    errorElement.classList.remove('show');

    // Update label color
    const label = input.closest('.input-group-reset-pwd').querySelector('.input-label');
    label.style.color = '#48bb78';
}

// Clear validation state
function clearValidationState(input, errorElement) {
    input.classList.remove('valid', 'invalid', 'matching');
    errorElement.classList.remove('show');

    // Reset label color
    const label = input.closest('.input-group-reset-pwd').querySelector('.input-label');
    label.style.color = '#4a5568';
}

// Handle form submission
async function handlePasswordReset(event) {
    event.preventDefault();

    // Validate all fields
    const isNewPasswordValid = validateNewPassword();
    const isConfirmPasswordValid = validateConfirmPassword();

    if (!isNewPasswordValid || !isConfirmPasswordValid || !passwordValidationState.passwordsMatch) {
        return;
    }

    // Check minimum strength requirement
    if (passwordValidationState.strengthScore < 4) {
        showPopupMessage('warning', 'Weak Password', 'Please choose a stronger password for better security.');
        return;
    }

    // Show loading state
    setLoadingState(true);

    try {
        // Simulate API call
        const result = await simulatePasswordReset();

        if (result.success) {
            // Show success animation
            showSuccessAnimation();

            // Show success message
            showPopupMessage('success', 'Password Reset Successful',
                'Your password has been successfully updated. You will be redirected to the login page.', {
                autoClose: true,
                autoCloseDelay: 3000,
                onClose: () => {
                    // Redirect to login page
                    window.location.href = '/login';
                }
            });

            // Clear form
            resetForm.reset();
            clearAllValidationStates();

        } else {
            showPopupMessage('error', 'Reset Failed', result.message);
        }

    } catch (error) {
        showPopupMessage('error', 'Connection Error', 'Unable to connect to server. Please try again.');
    } finally {
        setLoadingState(false);
    }
}

// Simulate password reset API call
function simulatePasswordReset() {
    return new Promise((resolve) => {
        setTimeout(() => {
            // Simulate different scenarios
            const random = Math.random();

            if (random < 0.1) {
                resolve({ success: false, message: 'Session expired. Please request a new password reset link.' });
            } else if (random < 0.2) {
                resolve({ success: false, message: 'Password reset token is invalid or expired.' });
            } else if (random < 0.3) {
                resolve({ success: false, message: 'Cannot use a previously used password. Please choose a different one.' });
            } else {
                resolve({ success: true });
            }
        }, 2000); // Simulate network delay
    });
}

// Set loading state
function setLoadingState(isLoading) {
    const btnText = resetBtn.querySelector('.btn-text');
    const btnSpinner = resetBtn.querySelector('.btn-spinner');

    if (isLoading) {
        resetBtn.disabled = true;
        btnText.textContent = 'Resetting Password...';
        btnSpinner.style.display = 'block';
        resetBtn.style.cursor = 'not-allowed';
    } else {
        resetBtn.disabled = false;
        btnText.textContent = 'Reset Password';
        btnSpinner.style.display = 'none';
        resetBtn.style.cursor = 'pointer';
    }
}

// Show success animation
function showSuccessAnimation() {
    const animation = document.createElement('div');
    animation.className = 'success-animation';
    animation.innerHTML = '✓';

    document.body.appendChild(animation);
    animation.classList.add('show');

    setTimeout(() => {
        animation.remove();
    }, 1500);
}

// Add focus effects
function addFocusEffects() {
    const inputs = [newPasswordInput, confirmPasswordInput];

    inputs.forEach(input => {
        input.addEventListener('focus', function () {
            this.closest('.input-wrapper').style.transform = 'translateY(-1px)';
            this.closest('.input-group-reset-pwd').querySelector('.input-label').style.transform = 'translateY(-2px)';
        });

        input.addEventListener('blur', function () {
            this.closest('.input-wrapper').style.transform = 'translateY(0)';
            this.closest('.input-group-reset-pwd').querySelector('.input-label').style.transform = 'translateY(0)';
        });
    });
}

// Initialize accessibility features
function initializeAccessibility() {
    // Add ARIA labels
    newPasswordInput.setAttribute('aria-describedby', 'newPasswordError passwordStrength');
    confirmPasswordInput.setAttribute('aria-describedby', 'confirmPasswordError passwordMatch');

    // Add live regions for screen readers
    newPasswordError.setAttribute('aria-live', 'polite');
    confirmPasswordError.setAttribute('aria-live', 'polite');
    strengthText.setAttribute('aria-live', 'polite');

    // Add form validation announcements
    resetForm.setAttribute('novalidate', 'true');

    // Add password toggle labels
    newPasswordToggle.setAttribute('aria-label', 'Show password');
    confirmPasswordToggle.setAttribute('aria-label', 'Show password');
}


// Clear all validation states
function clearAllValidationStates() {
    clearValidationState(newPasswordInput, newPasswordError);
    clearValidationState(confirmPasswordInput, confirmPasswordError);
    //passwordStrength.classList.remove('show');
    passwordMatch.classList.remove('show');

    // Reset state
    passwordValidationState = {
        isNewPasswordValid: false,
        isConfirmPasswordValid: false,
        passwordsMatch: false,
        strengthScore: 0
    };
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

// Go back to login
function goBackToLogin() {
    window.location.href = '/login';
}

// Keyboard shortcuts
document.addEventListener('keydown', function (event) {
    // Escape key to go back
    if (event.key === 'Escape') {
        goBackToLogin();
    }

    // Enter key to submit form when focused on inputs
    if (event.key === 'Enter' && (event.target === newPasswordInput || event.target === confirmPasswordInput)) {
        event.preventDefault();
        handlePasswordReset(event);
    }

    // Ctrl/Cmd + Shift + P to generate strong password (for testing)
    if ((event.ctrlKey || event.metaKey) && event.shiftKey && event.key === 'P') {
        event.preventDefault();
        generateStrongPassword();
    }
});

// Generate strong password (for testing)
function generateStrongPassword() {
    const chars = {
        lowercase: 'abcdefghijklmnopqrstuvwxyz',
        uppercase: 'ABCDEFGHIJKLMNOPQRSTUVWXYZ',
        numbers: '0123456789',
        special: '!@#$%^&*()_+-=[]{}|;:,.<>?'
    };

    let password = '';

    // Ensure at least one character from each category
    password += chars.lowercase[Math.floor(Math.random() * chars.lowercase.length)];
    password += chars.uppercase[Math.floor(Math.random() * chars.uppercase.length)];
    password += chars.numbers[Math.floor(Math.random() * chars.numbers.length)];
    password += chars.special[Math.floor(Math.random() * chars.special.length)];

    // Fill the rest randomly
    const allChars = chars.lowercase + chars.uppercase + chars.numbers + chars.special;
    for (let i = password.length; i < 12; i++) {
        password += allChars[Math.floor(Math.random() * allChars.length)];
    }

    // Shuffle the password
    password = password.split('').sort(() => Math.random() - 0.5).join('');

    // Fill both inputs
    newPasswordInput.value = password;
    confirmPasswordInput.value = password;

    // Trigger validation
    handleNewPasswordInput();
    handleConfirmPasswordInput();

    showPopupMessage('success', 'Strong Password Generated',
        'A strong password has been generated and filled in both fields. Feel free to modify it if needed.', {
        autoClose: true,
        autoCloseDelay: 3000
    });
}

// Password strength tips
function showPasswordTips() {
    const tips = [
        'Use a mix of uppercase and lowercase letters',
        'Include numbers and special characters',
        'Make it at least 12 characters long',
        'Avoid dictionary words and personal information',
        'Consider using a passphrase with spaces',
        'Use a password manager for unique passwords'
    ];

    const randomTip = tips[Math.floor(Math.random() * tips.length)];

    showPopupMessage('info', 'Password Tip', `💡 ${randomTip}`, {
        autoClose: true,
        autoCloseDelay: 4000
    });
}

// Show password tips periodically
setTimeout(() => {
    if (newPasswordInput.value.length === 0) {
        showPasswordTips();
    }
}, 10000);

// Form analytics (optional)
function trackFormInteraction(action, field) {
    // In real implementation, send to analytics service
    console.log(`Reset form interaction: ${action} on ${field}`);
}


// Form analytics (continued)
resetForm.addEventListener('submit', () => trackFormInteraction('submit', 'resetForm'));

// Password visibility tracking
newPasswordToggle.addEventListener('click', () => trackFormInteraction('toggleVisibility', 'newPassword'));
confirmPasswordToggle.addEventListener('click', () => trackFormInteraction('toggleVisibility', 'confirmPassword'));

// Enhanced error handling for network issues
window.addEventListener('online', function () {
    showPopupMessage('success', 'Connection Restored', 'Internet connection has been restored.', {
        autoClose: true,
        autoCloseDelay: 3000
    });
});

window.addEventListener('offline', function () {
    showPopupMessage('warning', 'Connection Lost', 'Please check your internet connection and try again.', {
        autoClose: false
    });
});

// Real-time password comparison
function comparePasswords() {
    const newPassword = newPasswordInput.value;
    const confirmPassword = confirmPasswordInput.value;

    if (newPassword.length === 0 || confirmPassword.length === 0) {
        return;
    }

    // Visual feedback for partial matches
    let matchPercentage = 0;
    const minLength = Math.min(newPassword.length, confirmPassword.length);

    for (let i = 0; i < minLength; i++) {
        if (newPassword[i] === confirmPassword[i]) {
            matchPercentage++;
        }
    }

    matchPercentage = (matchPercentage / Math.max(newPassword.length, confirmPassword.length)) * 100;

    // Update UI based on match percentage
    if (matchPercentage === 100 && newPassword === confirmPassword) {
        confirmPasswordInput.style.borderColor = '#38a169';
    } else if (matchPercentage > 50) {
        confirmPasswordInput.style.borderColor = '#ecc94b';
    } else {
        confirmPasswordInput.style.borderColor = '#f56565';
    }
}

// Add real-time comparison
confirmPasswordInput.addEventListener('input', comparePasswords);

// Caps Lock detection
function detectCapsLock(event) {
    const capsLockOn = event.getModifierState && event.getModifierState('CapsLock');
    const capsWarning = document.getElementById('capsLockWarning');

    if (capsLockOn) {
        if (!capsWarning) {
            const warning = document.createElement('div');
            warning.id = 'capsLockWarning';
            warning.style.cssText = `
                position: fixed;
                top: 20px;
                right: 20px;
                background: #fed7d7;
                color: #c53030;
                padding: 12px 16px;
                border-radius: 8px;
                border: 1px solid #feb2b2;
                font-size: 0.9rem;
                z-index: 1000;
                animation: slideIn 0.3s ease;
            `;
            warning.innerHTML = '⚠️ Caps Lock is ON';
            document.body.appendChild(warning);
        }
    } else if (capsWarning) {
        capsWarning.remove();
    }
}

// Add caps lock detection
newPasswordInput.addEventListener('keydown', detectCapsLock);
confirmPasswordInput.addEventListener('keydown', detectCapsLock);

// Initialize form state on load
document.addEventListener('DOMContentLoaded', function () {
    //loadFormState();
});

// Cleanup on page unload
window.addEventListener('beforeunload', function () {
    // Clear sensitive data
    newPasswordInput.value = '';
    confirmPasswordInput.value = '';

    // Save form state (without passwords)
    //saveFormState();
});

// Final initialization check
console.log('Reset Password Form fully initialized');
