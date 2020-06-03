/**
 * Set up click listener on page load.
 */
OnLoad(function () 
{
    // On left click...
    document.getElementById("submit").addEventListener("click", (ev) => {
        ev.preventDefault();
        SubmitMessage();
    });
});

/**
 * API model for messages.
 */
var MessageModel = function (firstName, lastName, email, message, reason)
{
    this.FirstName = firstName,
    this.LastName = lastName,
    this.Email = email,
    this.Message = message,
    this.Reason = reason
};

/**
 * Click event for submit button. Parse contact info and send via ajax.
 */
function SubmitMessage()
{
    // Get reason for user message.
    var submitReason = GetSubmitReason();

    // Get first name.
    var firstName = document.getElementById("first-name").value;

    // Get last name.
    var lastName = document.getElementById("last-name").value;

    // Get email
    var email = document.getElementById("email").value;

    if(CheckStringNullOrEmpty(email))
    {
        alert("Please enter an Email Address.");
        return;
    }

    var message = document.getElementById("message").value;

    if(CheckStringNullOrEmpty(message))
    {
        alert("Please enter a message.");
        return;
    }

    // Create the upload paramaters
    var messageModel = new MessageModel(firstName, lastName, email, message, submitReason);

    AjaxPost("/contact/message", messageModel);
}

/**
 * Find the value of the submit reason checkbox.
 */
function GetSubmitReason()
{
    if(document.getElementById("id-general").checked)
        return "General Inquiry";

    if (document.getElementById("id-licensing").checked)
        return "Licensing";

    if (document.getElementById("id-developer").checked)
        return "Developer";

    if (document.getElementById("id-sales").checked)
        return "Sales";

    return "Unspecified";
}

/**
 * Check if string is null or empty.
 * @param {String} value - string to check.
 */
function CheckStringNullOrEmpty(value)
{
    if(value == "" || value == " " || value == null)
        return true;

    return false;
}

/**
 * Makes an Ajax call to the specified URL and calls onSuccess if succeeds
 * @param {string} url - The URL to call
 * @param {object} model - The data model to send with this call
 */
function AjaxPost(url, model)
{
    // If we are still sending...
    if (document.getElementById("submit").value == "Sending...")
        // Ignore
        return;

    // Disable the submit button.
    DisableSubmitButton();

    // Inform user message is being sent.
    PromptSending();

    // Create the request.
    var xmlHttpRequest = new XMLHttpRequest();

    // Open the request.
    xmlHttpRequest.open("POST", url, true);

    // Set request headers.
    xmlHttpRequest.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    // On state change, process...
    xmlHttpRequest.onreadystatechange = function ()
    {
        // If it's ready...
        if (xmlHttpRequest.readyState == XMLHttpRequest.DONE)
        {
            // Inform user if request succeeded or not.
            if (xmlHttpRequest.status != 200)
            {
                PromptError();
            }
            else
            {
                if(xmlHttpRequest.response == "true")
                    PromptSuccess();
                else
                    PromptError();
            }

            // Re enable the submit button.
            EnableSubmitButton();

            // Clear entrys.
            ClearTextEntrys();
        }
    };

    try
    {
        xmlHttpRequest.send(JSON.stringify(model));
    }
    catch(ex)
    {
        console.log(ex);
    }
}

/**
 * Disable submit button and enable spinner.
 */
function DisableSubmitButton()
{
    // Get submit button.
    var submitButton = document.getElementById("submit");

    // Hide button.
    submitButton.classList.add("hidden");

    // Get spinner.
    var spinner = document.getElementById("spinner");

    // Reveal spinner.
    spinner.classList.remove("hidden");
}

/**
 * Enable submit button and disable spinner.
 */
function EnableSubmitButton()
{
    // Get spinner.
    var spinner = document.getElementById("spinner");

    // Hide spinner.
    spinner.classList.add("hidden");

    // Get submit button.
    var submitButton = document.getElementById("submit");

    // Reveal button.
    submitButton.classList.remove("hidden");
}

/**
 * Prompt user their message is being sent.
 */
function PromptSending()
{
    // Get the result message.
    var responseMessage = document.getElementById("request-result-message");

    // Remove the opposite class if it exists.
    responseMessage.classList.remove("contact-result-positive", "contact-result-negative", "hidden");

    // Update the response message value.
    responseMessage.innerHTML = "Sending message..."
}

/**
 * Prompt user an error has occured sending the message.
 */
function PromptError()
{
    // Get the result message.
    var responseMessage = document.getElementById("request-result-message");

    // Remove the opposite class if it exists.
    responseMessage.classList.remove("contact-result-positive", "hidden");

    // Change color of message.
    responseMessage.classList.add("contact-result-negative");

    // Update the response message value.
    responseMessage.innerHTML = "Something went wrong sending the message. Please try again later."
}

/**
 * Prompt user message has sent successfully.
 */
function PromptSuccess()
{
    // Get the result message.
    var responseMessage = document.getElementById("request-result-message");

    // Remove the opposite class if it exists.
    responseMessage.classList.remove("contact-result-negative", "hidden");

    // Change color of message.
    responseMessage.classList.add("contact-result-positive");

    // Update the response message value.
    responseMessage.innerHTML = "Message sent succesfully. Someone will contact you soon."
}


function ClearTextEntrys()
{
    document.getElementById("first-name").value = "";
    document.getElementById("last-name").value = "";
    document.getElementById("email").value = "";
    document.getElementById("id-general").click();
    document.getElementById("message").value = "";
}
