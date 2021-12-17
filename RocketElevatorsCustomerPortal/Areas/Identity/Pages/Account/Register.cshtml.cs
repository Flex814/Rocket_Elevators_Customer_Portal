using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;

namespace RocketElevatorsCustomerPortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<RocketElevatorsCustomerPortalUser> _signInManager;
        private readonly UserManager<RocketElevatorsCustomerPortalUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<RocketElevatorsCustomerPortalUser> userManager,
            SignInManager<RocketElevatorsCustomerPortalUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        

        private async Task<Boolean> GetExternalResponse(string email)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/customer/" + email);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            if (result.Replace('"', ' ').Trim() == "Not Found")
            {
                return false;
            } else if (result.Replace('"', ' ').Trim()== "Found") {
                

                return true;

            }
            else
            {
                return false;

            }



            
      
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                bool getEmailResult = await GetExternalResponse(Input.Email);
                Console.WriteLine(getEmailResult);
                if (getEmailResult == true)
                {
                    var user = new RocketElevatorsCustomerPortalUser { UserName = Input.Email, Email = Input.Email };
                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");


                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "The email provided is not allowed to register");
                }
            
               
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
