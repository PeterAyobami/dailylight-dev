using Dailylight.Server.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static Dna.FrameworkDI;

namespace Dailylight.Web.Server
{
    /// <summary>
    /// Manages the Web API calls
    /// </summary>
    [AuthorizeToken]
    public class ApiController : Controller
    {
        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        public ApiController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion


        #region Login / Register / Verify

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>Returns the result of the register request</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(ApiRoutes.Register)]
        public async Task<RegisterResultApiModel> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Please provide all required details to register for an account";

            // The error response for a failed login
            var errorResponse = new RegisterResultApiModel
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we have no credentials...
            if (registerCredentials == null)
                // Return failed response
                return errorResponse;

            // Make sure we have a user name
            if (string.IsNullOrWhiteSpace(registerCredentials.Username))
                // Return error message to user
                return errorResponse;

            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email
            };

            // Try and create a user
            var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

                // Send email verification
                //await SendUserEmailVerificationAsync(user);

                // Return valid response containing all users details
                return new RegisterResultApiModel
                {
                    FirstName = userIdentity.FirstName,
                    LastName = userIdentity.LastName,
                    Email = userIdentity.Email,
                    Username = userIdentity.UserName,
                    Token = userIdentity.GenerateJwtToken()
                };
            }
            // Otherwise if it failed...
            else
                // Return the failed response
                return new RegisterResultApiModel
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors.AggregateErrors()
                };
        }

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <returns>Returns the result of the login request</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(ApiRoutes.Login)]
        public async Task<UserProfileDetailsApiModel> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new UserProfileDetailsApiModel
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a user name
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message to user
                return errorResponse;

            // Validate if the user credentials are correct...

            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await mUserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await mUserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginCredentials.Password);

            // If the password was wrong
            if (!isValidPassword)
                // Return error message to user
                return errorResponse;

            // If we get here, we are valid and the user passed the correct login details

            // Return token to user
            return new UserProfileDetailsApiModel
            {
                // Pass back the user details and the token
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Token = user.GenerateJwtToken()
            };
        }

        #endregion


        #region Update User Profile

        /// <summary>
        /// The method to update the users profile details
        /// </summary>
        /// <param name="model">The user profile details to update</param>
        /// <returns>
        /// Returns successful response if the update was successful, 
        /// otherwise returns the error reasons for the failure
        /// </returns>
        [HttpPost]
        [Route(ApiRoutes.UpdateUserProfile)]
        public async Task<ApiResponse> UpdateUserProfileAsync([FromBody]UpdateUserProfileApiModel model)
        {
            // Get the user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                // Return error
                return new ApiResponse
                {
                    ErrorMessage = "User not found"
                };

            // If we have a first name...
            if (model.FirstName != null)
                // Update the profile details
                user.FirstName = model.FirstName;

            // If we have a last name...
            if (model.LastName != null)
                // Update the profile details
                user.LastName = model.LastName;

            // If we have an email...
            if (model.Email != null)
                // Update the profile details
                user.Email = model.Email;

            // If we have a username...
            if (model.Username != null)
                // Update the profile details
                user.UserName = model.Username;

            // Attempt to commit changes to the data store
            var result = await mUserManager.UpdateAsync(user);

            // If update was successful...
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise if it failed...
            else
                // Return the failed response
                return new ApiResponse
                {
                    ErrorMessage = result.Errors.AggregateErrors()
                };
        }

        #endregion


        #region Devotions / Reviews / Feedback

        /// <summary>
        /// The method to get the available devotion editions
        /// </summary>
        /// <returns>Returns the available devotion editions</returns>
        [Route(ApiRoutes.Editions)]
        public async Task<IActionResult> GetEditionsAsync()
        {
            // Get all editions
            var editions = await mContext
                .DevotionEditions
                .ToListAsync();

            var result = new List<EditionsResultApiModel>();

            // For each edition...
            foreach (var item in editions)
            {
                // Add to the api result
                var edition = new EditionsResultApiModel
                {
                    Id = item.Id,
                    Edition = item.Edition,
                    EditionYear = item.EditionYear,
                    EditionPrice = item.EditionPrice
                };

                result.Add(edition);
            }

            // Return the result
            return new JsonResult(result);
        }

        /// <summary>
        /// The method to verify if a user has 
        /// purchased a particular devotion edition
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.VerifyEdition)]
        public async Task<VerifyEditionResponseApiModel> VerifyEditionPurchase([FromBody]EditionRequestApiModel edition)
        {
            // Get the user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // Check if requested edition exist
            var hasPurchasedEdition = await mContext
                .PurchasedEditions
                .AnyAsync(x => x.UserId == user.Id & x.EditionId == edition.EditionId);

            // Return the result
            return new VerifyEditionResponseApiModel
            {
                HasEdition = hasPurchasedEdition
            };
        }

        /// <summary>
        /// The method to get devotions from the database
        /// </summary>
        /// <returns>Returns the json result of devotions with specified edition</returns>
        [Route(ApiRoutes.Devotions)]
        public async Task<IActionResult> GetDevotionsAsync([FromBody]DevotionsRequestApiModel devotionsRequest)
        {
            // Get the user...
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // Verify the user has purchased the requested devotion
            var userPurchasedDevotions = await mContext
                .PurchasedEditions
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Edition)
                .ToListAsync();

            // Filter for the requested edition
            var hasRequestedEdition = userPurchasedDevotions
                .Any(x => x.Edition.EditionYear == devotionsRequest.DevotionYear);

            // If the user does not have the specified devotion...
            if (!hasRequestedEdition)
                return new JsonResult(new DevotionResultApiModel
                {
                    ErrorMessage = "You have not purchased this edition"
                });

            // If we get here, user has purchased specified devotion edition
            // Get the specified devotion edition

            var data = await mContext
                .Devotions
                .Where(x => x.DevotionYear == devotionsRequest.DevotionYear)
                .ToListAsync();

            var result = new List<DevotionResultApiModel>();

            foreach (var item in data)
            {
                var devotion = new DevotionResultApiModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    AnchorScripture = item.AnchorScripture,
                    TextQuote = item.TextQuote,
                    FirstTextParagraph = item.FirstTextParagraph,
                    SecondTextParagraph = item.SecondTextParagraph,
                    ThirdTextParagraph = item.ThirdTextParagraph,
                    FurtherReading = item.FurtherReading,
                    MemoryVerse = item.MemoryVerse,
                    PrayerPoint = item.PrayerPoint,
                    PropheticDeclaration = item.PropheticDeclaration,
                    ImageUrl = item.ImageUrl,
                    Date = item.Date
                };

                result.Add(devotion);
            }

            // Return the devotion
            return new JsonResult(result);
        }

        /// <summary>
        /// The endpoint for adding user feedback
        /// </summary>
        /// <param name="userFeedback">The user feedback thats passed in</param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Feedback)]
        public async Task<ApiResponse> AddUserFeedbackAsync([FromBody]UserFeedbackApiModel userFeedback)
        {
            // The api response
            var response = new ApiResponse();

            // Validate the request body
            response.ErrorMessage = string
                .IsNullOrEmpty(userFeedback.FeedbackTitle) && string
                .IsNullOrEmpty(userFeedback.FeedbackCategory) && string
                .IsNullOrEmpty(userFeedback.FeedbackMessage) ?
                "Please provide all the necessary information" : null;

            // If we have an error message...
            if (response.ErrorMessage != null)
                // Return the error message response
                return response;

            // Get the feedback category
            var feedbackCategory = await mContext.FeedbackCategories
                .Where(x => x.FeedbackCategory == userFeedback.FeedbackCategory)
                .FirstAsync();

            // Get the user id
            var user = await mUserManager
                .GetUserAsync(HttpContext.User);

            // Set the feedback data
            var feedback = new UserFeedbackDataModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                FeedbackCategoryId = feedbackCategory.Id,
                FeedbackTitle = userFeedback.FeedbackTitle,
                FeedbackMessage = userFeedback.FeedbackMessage
            };

            // Store the user feedback
            await mContext.UserFeedbacks.AddAsync(feedback);

            // Coommit changes to the database
            await mContext.SaveChangesAsync();

            return response;
        }


        /// <summary>
        /// The endpoint to add a review about a particular devotion
        /// </summary>
        /// <param name="devotionId">The id of the devotion</param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Review)]
        public async Task<ApiResponse> AddDevotionReviewAsync([FromBody]DevotionReviewsApiModel devotionReviews)
        {
            // Set the response... Validate the requset body
            var response = new ApiResponse
            {
                ErrorMessage = string
                .IsNullOrEmpty(devotionReviews.ReviewMessage) || string
                .IsNullOrWhiteSpace(devotionReviews.ReviewMessage) ?
                "Please specify your review for this devotion" : null
            };

            // If we have an error message already...
            if (response.ErrorMessage != null)
                // Return the error response
                return response;

            // Get the user trying to make this review
            var user = await mUserManager
                .GetUserAsync(HttpContext.User);

            // Set the review data
            var review = new DevotionReviewsDataModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                DevotionId = devotionReviews.DevotionId,
                ReviewText = devotionReviews.ReviewMessage
            };

            // Store the devotion reviews
            await mContext.DevotionReviews.AddAsync(review);

            // Commit changes to the database
            await mContext.SaveChangesAsync();

            // Return the response
            return response;
        }

        #endregion


        #region Payment Process / Payment Verification
        
        [HttpPost]
        [Route(ApiRoutes.ProcessPayment)]
        public async Task<PaymentResultApiModel> ProcessPaymentAsync([FromBody]PaymentRequestApiModel paymentRequest)
        {
            // Get the user
            var user = await mUserManager
                .GetUserAsync(HttpContext.User);

            // Initialize a paystack transaction

            var payStack = new PayStackApi(Configuration["PayStack:SecretKey"]);

            // Set the request parameters
            var request = new TransactionInitializeRequest
            {
                Reference = paymentRequest.Reference,
                AmountInKobo = paymentRequest.Amount * 100,
                Email = user.Email,
                CallbackUrl = Configuration["AbsoluteUrl"] + ApiRoutes.VerifyPayment,
                Currency = "NGN"
            };

            try
            {
                // Initialize the payment transaction
                var trxResult = payStack
                    .Transactions
                    .Initialize(request);

                // If the transaction was successful...
                if (trxResult.Status)
                {
                    // Get the authorization url
                    var authorizationUrl = trxResult
                        .Data
                        .AuthorizationUrl;

                    // Register a payment transaction
                    await mContext
                        .PaymentTransactions
                        .AddAsync(new PaymentTransactionsDataModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            Reference = paymentRequest.Reference,
                            UserId = user.Id,
                            PaymentVerified = false,
                            EditionId = paymentRequest.EditionId
                        });

                    // Commit changes to the database
                    await mContext.SaveChangesAsync();

                    // Redirect to the authorised url
                    return new PaymentResultApiModel
                    {
                        AuthorisationUrl = authorizationUrl
                    };
                }
                // Otherwise, if it was unsuccessful...
                else
                {
                    // Return error message
                    return new PaymentResultApiModel
                    {
                        ErrorMessage = trxResult.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new PaymentResultApiModel
                {
                    ErrorMessage = ex.Message
                };
            }
        }


        /// <summary>
        /// The endpoint for payments verification
        /// </summary>
        /// <param name="trxref">The transaction reference id</param>
        /// <param name="reference">The transaction reference id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(ApiRoutes.VerifyPayment)]
        public async Task<IActionResult> VerifyPaymentAsync([FromQuery]string trxref, [FromQuery]string reference)
        {
            // Query the transaction reference
            if (string.IsNullOrEmpty(trxref))
                return Redirect("/");

            // Verify the reference id
            var clientTransaction = await mContext
                .PaymentTransactions
                .FirstOrDefaultAsync(x => x.Reference == trxref);

            // If a transaction does not exist...
            if (clientTransaction == null)
                // Return unrecognized transaction
                return View("TransactionResult", new TransactionResultDataModel
                {
                    Status = TransactionStatus.NotRecognized
                });

            // Otherwise, verify the payment status...

            // Get the transaction
            var payStack = new PayStackApi(Configuration["PayStack:SecretKey"]);
            // Get the payment status
            var paymentStatus = payStack.Transactions.Verify(trxref);

            // If the transaction was unsuccessful...
            if (!paymentStatus.Status)
                // Return error response
                return View("TransactionResult", new TransactionResultDataModel
                {
                    Status = TransactionStatus.Failed,
                    TransactionMessage = paymentStatus.Message
                });

            // Otherwise...

            // Update the payment verification status
            clientTransaction.PaymentVerified = true;
            mContext.PaymentTransactions.Update(clientTransaction);

            // Add to purchased edition
            await mContext.PurchasedEditions.AddAsync(new PurchasedEditionsDataModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = clientTransaction.UserId,
                EditionId = clientTransaction.EditionId
            });

            // Commit changes to database
            await mContext.SaveChangesAsync();

            // Return successful payment transaction
            return View("TransactionResult", new TransactionResultDataModel
            {
                Status = TransactionStatus.Successful,
                TransactionMessage = paymentStatus.Message
            });
        }

        #endregion


        #region Password Reset

        /// <summary>
        /// The endpoint for password reset requests
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route(ApiRoutes.PasswordResetRequest)]
        public async Task<PasswordResetResponseApiModel> ResetPasswordRequestAsync([FromBody]ResetPasswordUserCredentialsViewModel userCredentials)
        {
            // Get the user
            var user = await mUserManager.FindByEmailAsync(userCredentials.Email);

            // If user was not found...
            if (user == null)
                // Ignore sending reset password token
                return new PasswordResetResponseApiModel();

            // Otherwise...

            // Try sending the password reset token
            try
            {
                await SendPasswordResetEmailAsync(user);

                // Return the response
                return new PasswordResetResponseApiModel
                {
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                // Return the response with exception message
                return new PasswordResetResponseApiModel
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// The endpoint for password reset token confirmation
        /// </summary>
        [AllowAnonymous]
        [Route(ApiRoutes.PasswordResetConfirmation)]
        public async Task<IActionResult> ResetPasswordTokenConfirmationAsync(string userId, string resetToken)
        {
            // Get the user
            var user = await mUserManager.FindByIdAsync(userId);

            // If the user is null
            if (user == null)
                // TODO: Nice UI
                return Content("User not found");

            // If we have the user...

            // Load the reset password view
            return View(new PasswordResetViewModel 
            {
                UserId = userId,
                PasswordResetToken = resetToken
            });
        }

        /// <summary>
        /// The endpoint for password reset token confirmation
        /// </summary>
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromForm]PasswordResetViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Error");

            // Get the user
            var user = await mUserManager.FindByIdAsync(viewModel.UserId);

            // If the user is null
            if (user == null)
                // TODO: Nice UI
                return Content("User not found");

            // If we have the user...

            // Reset the users password
            var result = await mUserManager.ResetPasswordAsync(
                user, viewModel.PasswordResetToken, viewModel.NewPassword);

            // If succeeded...
            if (result.Succeeded)
                // TODO: Nice UI
                return Content("Password Reset was Successful :)");

            // TODO: Nice UI
            return Content("Invalid Password Reset Token :(");
        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Sends the given user a password reset token email
        /// </summary>
        /// <param name="user">The specified user</param>
        private async Task SendPasswordResetEmailAsync(ApplicationUser user)
        {
            // Get the user details
            var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

            // Generate an email verification code
            var passwordResetToken = await mUserManager.GeneratePasswordResetTokenAsync(user);

            // TODO: Replace with APIRoutes that will contain the static routes to use
            var resetUrl = $"http://{Request.Host.Value}/{ApiRoutes.PasswordResetConfirmation}/?userId={HttpUtility.UrlEncode(userIdentity.Id)}&resetToken={HttpUtility.UrlEncode(passwordResetToken)}";

            // Email the user the verification code
            await DailylightEmailSender.SendPasswordResetLinkEmailAsync(user.UserName, userIdentity.Email, resetUrl);
        }

        #endregion

        //[AllowAnonymous]
        //[Route("/passwordreset")]
        //public IActionResult PasswordReset()
        //{
        //    return View(new PasswordResetViewModel
        //    {
        //        UserId = "sjsks9212akq991jka91910nakakq91",
        //        PasswordResetToken = "siaaiaiq19119101029291kak"
        //    });
        //}
    }
}
