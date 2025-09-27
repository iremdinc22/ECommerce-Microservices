using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    [AllowAnonymous]
    public class CategoryController : Controller
    {
       private readonly IHttpClientFactory _httpClientFactory;

       public CategoryController(IHttpClientFactory httpClientFactory)
       {
           _httpClientFactory = httpClientFactory;
       }
       
       [Route("Index")]
       public async Task<ActionResult> Index()
        {
            ViewBag.vo = "Kategori İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync("http://localhost:5003/api/Categories");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var JsonData = await ResponseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(JsonData);
                return View(values);
            }
            return View();
        }
       
       [Route("CreateCategory")]
       [HttpGet]
       public async Task<ActionResult> CreateCategory()
        {
            ViewBag.vo = "Kategori İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Yeni Kategori Girişi";
            return View();
        }
        
        [Route("CreateCategory")]
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringcontent = new StringContent(jsonData , Encoding.UTF8 , "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5003/api/Categories", stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }
        
        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            ViewBag.vo = "Kategori İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Güncelleme Sayfası";
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5003/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(JsonData);
                return View(values);
            }
            return View();
        }
        
        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"http://localhost:5003/api/Categories/",stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index", "Category", new { area = "Admin" });  
            }
            
            return View();
            
        }
        
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5003/api/Categories?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }
        
        
        
        
        
        
       
       

    }
}
