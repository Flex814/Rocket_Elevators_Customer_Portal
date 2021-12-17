using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Text;
using RocketElevatorsCustomerPortal.Models;
using System.Net.Http.Headers;

namespace RocketElevatorsCustomerPortal.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<RocketElevatorsCustomerPortalUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<RocketElevatorsCustomerPortalUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        static HttpClient client = new HttpClient();

        [BindProperty]
        public InputPersonalModel Input { get; set; }
     
        public string CurrentAddress { get; private set; }


        public class InputPersonalModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "NumberAndStreet")]
            public string NumberAndStreet { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "City")]
            public string City { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "ZipCode")]
            public string ZipCode { get; set; }
           
            [DataType(DataType.Text)]
            [Display(Name = "Suite")]
            public string Suite { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            public string Country { get; set; }
            


        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            Console.WriteLine(user);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Address current = (Address) await PostCallAPI(user.Email);

            CurrentAddress = $"\n { current.NumberAndStreet } , { current.City } , { current.Country } , { current.PostalCode } , { current.SuiteAndApartment }";
            return Page();
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var user = await _userManager.GetUserAsync(User);
            Address address = new Address
            {
                NumberAndStreet = Input.NumberAndStreet,
                City = Input.City,
                Country = Input.Country,
                PostalCode = Input.ZipCode,
                SuiteAndApartment = Input.Suite
            };
            var updateResult = await UpdateProductAsync(address,user.Email);

            if (updateResult)
            {
                ModelState.AddModelError(string.Empty, "Your Data Has been Successfully updated.");
                Address current = (Address)await PostCallAPI(user.Email);

                CurrentAddress = $"\n { current.NumberAndStreet } , { current.City } , { current.Country } , { current.PostalCode } , { current.SuiteAndApartment }";

            }
            else
            {
                ModelState.AddModelError(string.Empty, "There was an error while Updating your address");
            }
            
   
            return Page();
        }
        public static async Task<Address>PostCallAPI(string user)
        {
            try
            {

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/address/" + user);

                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var addy =  JsonConvert.DeserializeObject<object>(jsonString);
                Address currentAddy = JsonConvert.DeserializeObject<Address>(jsonString);
                
                return currentAddy;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }




        static async Task<Boolean> UpdateProductAsync(Address product,string user)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://jakerocket.azurewebsites.net/");
            }
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"address/{user}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            var result = await response.Content.ReadAsStringAsync();


            if (result.Contains("successfully"))
            {
                return true;
            }
            return false;
        }
   
    }
}