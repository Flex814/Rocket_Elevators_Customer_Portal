using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RocketElevatorsCustomerPortal.Models;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Vereyon.Web;
using System.Net.Http.Headers;
using System.Web;

namespace RocketElevatorsCustomerPortal.Controllers
{
    [Route("[controller]")]
    public class InterventionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly jacobgomezContext _context;
        private readonly UserManager<RocketElevatorsCustomerPortalUser> _userManager;
        private readonly IFlashMessage _flashmessage;

        public string buildingId = "";
        public string batteryId = "";
        public string columnId = "";
        public string elevatorId = "";
        public InterventionController(jacobgomezContext context, UserManager<RocketElevatorsCustomerPortalUser> userManager, ILogger<HomeController> logger, IFlashMessage flashMessage)
        {
            _flashmessage = flashMessage;
            _userManager = userManager;
            _context = context;
            _logger = logger;

        
    
        }


        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    string customerId = await getCustomer(user.Email);
                    ViewBag.building = await getBuilding(customerId);
                    TempData["preload"] = "none";
                    TempData["buildingId"] = "0";
                }
                return View();

            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{product}/{id}")]
        public async Task<IActionResult> Index(string product , string id)
        {

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    
                    string customerId = await getCustomer(user.Email);

                    ViewBag.building = await getBuilding(customerId);
                    await getSpecs(customerId,product, id);

                    TempData["buildingId"] = buildingId;
                    TempData["batteryId"] = batteryId;
                    TempData["columnId"] = columnId;
                    TempData["elevatorId"] = elevatorId;

                }
                return View();

            }
            catch
            {
                return BadRequest();
            }
        }

        


        [HttpPost]
        public async Task<IActionResult> SendIntervention(Intervention intervention)
        {
            var user = await _userManager.GetUserAsync(User);
            string customerId = await getCustomer(user.Email);
            intervention.UserId = Convert.ToInt64(customerId);
            intervention.CustomerId = Convert.ToInt64(customerId);
            HttpClient client = new HttpClient();

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://jakerocket.azurewebsites.net/");
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"intervention/insert", intervention);
           

            // Deserialize the updated product from the response body.
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            
            _flashmessage.Confirmation("Your Intervention has been successfully submitted");
            return Redirect(Url.Content("~/"));
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

        public async Task<List<Building>> getSpecs(string customer,string product, string id)
        {


            try
            {
                
                TempData["preload"] = "preload";
                var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/product/building/" + customer);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();


                var jo = JArray.Parse(jsonString);

                List<Building> buidlinglist = new List<Building>();
                void Work()
                {
                    foreach (var key in jo)
                    {

                        buildingId = key["id"].ToString();

                        Console.WriteLine("Building: " + buildingId);
                        foreach (var batt in key["batteries"])
                        {


                            TempData["function"] = "battery";

                            batteryId = batt["id"].ToString();
                            if (product == "battery" && id == batt["id"].ToString())
                            {

                                Console.WriteLine("Battery: " + batteryId);
                                return;

                            }
                            foreach (var col in batt["columns"])
                            {

                                Console.WriteLine("Column: " + columnId);
                                TempData["function"] = "column";
                                columnId = col["id"].ToString();
                                if (product == "column" && id == col["id"].ToString())
                                {
                                    return;
                                }
                                foreach (var el in col["elevators"])
                                {

                                    TempData["function"] = "elevator";
                                    elevatorId = el["id"].ToString();
                                    if (product == "elevator" && id == el["id"].ToString())
                                    {
                                        return;
                                    }
                                }
                            }


                        }
                    }
                };
                Work();
               


                return buidlinglist;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;

        }


        [Produces("application/json")]
        [HttpGet("getBuilding")]
        public async Task<List<Building>> getBuilding(string customer)
        {

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/interventions/building/"+ customer);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jo = JArray.Parse(jsonString);
            List<Building> buildinglist = new List<Building>();
            foreach (var key in jo)
            {
                buildinglist.Add(JsonConvert.DeserializeObject<Building>(key.ToString()));
                

            }

            return buildinglist;

        }
      



        [Produces("application/json")]
        [HttpGet("getBattery/{id}")]
        public async Task<List<Battery>> getBattery(string id)
        {

            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/interventions/battery/" + id);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jo = JArray.Parse(jsonString);
            List<Battery> batterylist = new List<Battery>();
            foreach (var key in jo)
            {
                batterylist.Add(JsonConvert.DeserializeObject<Battery>(key.ToString()));


            }
            return batterylist;

        }

        [Produces("application/json")]
        [HttpGet("getColumn/{id}")]
        public async Task<List<Column>> getColumn(string id)
        {

            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/interventions/column/" + id);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
              var jo = JArray.Parse(jsonString);
            List<Column> columnList = new List<Column>();
            foreach (var key in jo)
            {
                columnList.Add(JsonConvert.DeserializeObject<Column>(key.ToString()));


            }
            return columnList;

        }

        [Produces("application/json")]
        [HttpGet("getElevator/{id}")]
        public async Task<List<Elevator>> getElevator(string id)
        {

            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/interventions/elevator/" + id);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jo = JArray.Parse(jsonString);
            List<Elevator> elevatorlist = new List<Elevator>();
            foreach (var key in jo)
            {
                elevatorlist.Add(JsonConvert.DeserializeObject<Elevator>(key.ToString()));


            }
            return elevatorlist;

        }
        [Produces("application/json")]
        [HttpGet("getEmployee")]
        public async Task<List<Employee>> getEmployee()
        {

            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://jakerocket.azurewebsites.net/interventions/employee");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jo = JArray.Parse(jsonString);
            List<Employee> employeelist = new List<Employee>();
            foreach (var key in jo)
            {
                employeelist.Add(JsonConvert.DeserializeObject<Employee>(key.ToString()));


            }
            return employeelist;

        }
    }
}
