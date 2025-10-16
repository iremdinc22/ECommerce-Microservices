using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        [Route("Index")]
       public async Task<ActionResult> Index()
        {
            ViewBag.vo = "Hakkımda İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
            
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync("http://localhost:5003/api/Abouts");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var JsonData = await ResponseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(JsonData);
                return View(values);
            }
            return View();
        }
       
       [Route("CreateAbout")]
       [HttpGet]
       public async Task<ActionResult> CreateAbout()
        {
            ViewBag.vo = "Hakkımda İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Yeni Hakkımda Girişi";
            return View();
        }
        
        [Route("CreateAbout")]
        [HttpPost]
        public async Task<ActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            StringContent stringcontent = new StringContent(jsonData , Encoding.UTF8 , "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5003/api/Abouts", stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }
        
        [Route("UpdateAbout/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            ViewBag.vo = "Hakkımda İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Güncelleme Sayfası";
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5003/api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(JsonData);
                return View(values);
            }
            return View();
        }
        
        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
            StringContent stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"http://localhost:5003/api/Abouts/",stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index", "About", new { area = "Admin" });  
            }
            
            return View();
            
        }
        
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5003/api/Abouts?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }
        
        
        
        
        
        

    }
}
