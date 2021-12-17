using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;

namespace RocketElevatorsCustomerPortal.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<RocketElevatorsCustomerPortalUser> _userManager;
        private readonly SignInManager<RocketElevatorsCustomerPortalUser> _signInManager;

        static HttpClient client = new HttpClient();

        public IndexModel(
            UserManager<RocketElevatorsCustomerPortalUser> userManager,
            SignInManager<RocketElevatorsCustomerPortalUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(RocketElevatorsCustomerPortalUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/address/" + user.Email + "/phone");

            response.EnsureSuccessStatusCode();
            TempData["phone"] = await response.Content.ReadAsStringAsync();
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/address/" + user.Email +"/" +Input.PhoneNumber);

            
            var jsonString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(jsonString);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
