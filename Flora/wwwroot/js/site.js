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

//--------------------------------------------------------------------------

var correctAnswer;

function ContactValidation() {
    generateCaptcha();

    var forms = document.querySelectorAll("form");

    forms.forEach(function (form) {
        form.addEventListener("submit", function (event) {
            var userAnswer = form.querySelector("#captchaInput").value.trim();
            var name = form.querySelector("input[name='name']").value.trim();
            var email = form.querySelector("input[name='email']").value.trim();
            var message = form.querySelector("textarea[name='message']").value.trim();

            var validationMessages = [];

            if (!name) validationMessages.push("Name cannot be empty.");
            if (!email) validationMessages.push("Email cannot be empty.");
            if (!message) validationMessages.push("Message cannot be empty.");

            if (!userAnswer) {
                validationMessages.push("CAPTCHA cannot be empty.");
            } else if (parseInt(userAnswer, 10) !== correctAnswer) {
                validationMessages.push("Incorrect CAPTCHA answer. Please try again.");
                generateCaptcha();
            }

            if (validationMessages.length > 0) {
                event.preventDefault();
                alert(validationMessages.join("\n"));
            }
        });
    });
}
//-------------------------------------------------------------------------------------------------------------
function generateCaptcha() {
    var n1 = ranNum(), n2 = ranNum();

    correctAnswer = n1 + n2;

    var question = "What is " + n1 + " + " + n2 + "?";
    document.getElementById("captchaQ").textContent = question;
}

function ranNum() {
    return Math.floor(Math.random() * 10) + 1;
}

// Initialize the validation on page load
document.addEventListener("DOMContentLoaded", ContactValidation);

//-----------------------------------------------------------------------------------------

function showNotification(message) {
    const notification = document.getElementById("notification");
    if (notification && message) {
        notification.innerText = message;
        notification.classList.add("show-notification"); // Add a class to make it visible
        setTimeout(function () {
            notification.classList.remove("show-notification"); // Hide after 3 seconds
        }, 3000);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    // Get the TempData message from the hidden div
    const tempDataElement = document.getElementById("tempDataMessage");
    if (tempDataElement) {
        const message = tempDataElement.getAttribute("data-message");
        if (message) {
            showNotification(message);
        }
    }
});

