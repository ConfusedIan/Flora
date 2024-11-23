const emailField = document.getElementById("email");
const passwordField = document.getElementById("password");
const confirmPasswordField = document.getElementById("confirm-password");
const submitButton = document.getElementById("submitButton");

function validateForm() {
    const email = emailField.value.trim();
    const password = passwordField.value.trim();
    const confirmPassword = confirmPasswordField.value.trim();

    let isValid = true; // Track overall validity

    // Email Validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        isValid = false;
    }

    // Password Validation: At least one letter, one number, and 8+ characters
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    if (!passwordRegex.test(password)) {
        alert("Password must be at least 8 characters long and include both letters and numbers.");
        isValid = false;
    }

    // Check if Passwords Match
    if (password !== confirmPassword) {
        alert("Passwords do not match.");
        isValid = false;
    }

    // Enable or disable the submit button based on validity
    submitButton.disabled = !isValid;
}

// Attach event listeners for real-time validation
emailField.addEventListener("input", validateForm);
passwordField.addEventListener("input", validateForm);
confirmPasswordField.addEventListener("input", validateForm);

//-----------------------------------------------------------------------------------------------------------------------------------------

const form = document.querySelector(".feedback-form");
const nameField = document.getElementById("name");
const emailField = document.getElementById("email");
const messageField = document.getElementById("message");

form.addEventListener("submit", (event) => {
    const name = nameField.value.trim();
    const email = emailField.value.trim();
    const message = messageField.value.trim();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!name || !email || !message) {
        alert("Please fill out all fields.");
        event.preventDefault(); // Prevent form submission
        return;
    }

    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        event.preventDefault();
        return;
    }


});

//--------------------------------------------------------------------------

function dynamicGreeting() {
    var greetingElement = document.getElementById('greeting');
    var hour = new Date().getHours();
    var message;

    if (hour >= 5 && hour < 12) {
        message = 'And Good Morning!';
    } else if (hour >= 12 && hour < 18) {
        message = 'And Good Afternoon!';
    } else {
        message = 'And Good Evening!';
    }

    greetingElement.innerHTML = message;
}