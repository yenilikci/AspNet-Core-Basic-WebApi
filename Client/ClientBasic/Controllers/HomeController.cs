using ClientBasic.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public IActionResult Add()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44388/api/Products",content).Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Ekleme İşlemi Başarısız");
            return View();
      
        }   

        public IActionResult Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.GetAsync($"https://localhost:44388/api/Products/{id}").Result;
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var product = JsonConvert.DeserializeObject<Product>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(product);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44388/api/Products/{product.Id}",content).Result;
            /*
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {

            }
            */
            return RedirectToAction("Index");
        }

    }
}
