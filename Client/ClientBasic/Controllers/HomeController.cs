using ClientBasic.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientBasic.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = await client.GetAsync("https://localhost:44388/api/Products");
            List<Product> products = null;
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                products = JsonConvert.DeserializeObject<List<Product>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(products);
        }
    }
}
