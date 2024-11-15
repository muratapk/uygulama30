using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using uygulama30.Models;

namespace uygulama30.Controllers
{
    public class CategoryApiController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7176/api");
        private readonly HttpClient _httpClient;
        public CategoryApiController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> category = new List<Category>();
            HttpResponseMessage response=_httpClient.GetAsync(baseAdress).Result;
            if(response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                category=JsonConvert.DeserializeObject<List<Category>>(data);
            }

            return View(category);
        }
    }
}
