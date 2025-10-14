using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feature")]
    [AllowAnonymous]
    
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        [Route("Index")]
       public async Task<ActionResult> Index()
        {
            ViewBag.vo = "Anasayfa Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Alanlar";
            ViewBag.v3 = "Öne Çıkan Alan Listesi";
            
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync("http://localhost:5003/api/Features");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var JsonData = await ResponseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(JsonData);
                return View(values);
            }
            return View();
        }
       
       [Route("CreateFeature")]
       [HttpGet]
       public async Task<ActionResult> CreateFeature()
        {
            ViewBag.vo = "Anasayfa Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Alanlar";
            ViewBag.v3 = "Yeni Öne Çıkan Alan Girişi";
            return View();
        }
        
        [Route("CreateFeature")]
        [HttpPost]
        public async Task<ActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringcontent = new StringContent(jsonData , Encoding.UTF8 , "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5003/api/Features", stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }
        
        [Route("UpdateFeature/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            ViewBag.vo = "Anasayfa Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Alanlar";
            ViewBag.v3 = "Öne Çıkan Alan Güncelleme Sayfası";
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5003/api/Features/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(JsonData);
                return View(values);
            }
            return View();
        }
        
        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            StringContent stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"http://localhost:5003/api/Features/",stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index", "Feature", new { area = "Admin" });  
            }
            
            return View();
            
        }
        
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5003/api/Features?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }
        
        

    }
}
