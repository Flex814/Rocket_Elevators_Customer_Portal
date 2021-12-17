using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RocketElevatorsCustomerPortal.Models;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RocketElevatorsCustomerPortal.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Vereyon.Web;

namespace RocketElevatorsCustomerPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly jacobgomezContext _context;
        private readonly IFlashMessage _flashmessage;
        private readonly UserManager<RocketElevatorsCustomerPortalUser> _userManager;
      
        public List<Battery> batterylist = new List<Battery>();
        public List<Column> columnlist = new List<Column>();
        public List<Elevator> elevatorlist = new List<Elevator>();


        public HomeController(jacobgomezContext context, UserManager<RocketElevatorsCustomerPortalUser> userManager, ILogger<HomeController> logger, IFlashMessage flashMessage)
        {
            _flashmessage = flashMessage;
            _userManager = userManager;
            _context = context;
            _logger = logger;

        }
    
      
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                TempData["user"] = user.Email;
                string customerId = await getCustomer(user.Email);
                List<Building> building = await getBuilding(customerId);
                

                ViewBag.building = building;
                ViewBag.battery = batterylist;
                ViewBag.column = columnlist;
                ViewBag.elevator = elevatorlist;

            }
           
            return View();
        }

       
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        private async Task<String> getCustomer(string user)
        {


            try
            {

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/customer/id/" + user);

                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        private async Task<List<Building>> getBuilding(string customer)
        {


            try
            {

                var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/product/building/" + customer);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();


                var jo = JArray.Parse(jsonString);

                List<Building> buidlinglist = new List<Building>();
                
                foreach (var key in jo)
                {
                    buidlinglist.Add(JsonConvert.DeserializeObject<Building>(key.ToString()));

                    foreach (var batt in key["batteries"])
                    {
                       
                       try
                        {
                            
                            batterylist.Add(JsonConvert.DeserializeObject<Battery>(batt.ToString()));
                            foreach (var col in batt["columns"])
                            {
                                columnlist.Add(JsonConvert.DeserializeObject<Column>(col.ToString()));
                                foreach (var el in col["elevators"])
                                {
                                    elevatorlist.Add(JsonConvert.DeserializeObject<Elevator>(el.ToString()));
                                }
                            }
                        }
                        catch (NullReferenceException err)
                        {
                            Console.WriteLine("Please check the string str.");
                            Console.WriteLine(err.Message);
                        }

                    }
                }



                return buidlinglist;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
