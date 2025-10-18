using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    [AllowAnonymous]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        
        [Route("ProductImageDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ViewBag.vo = "Ürün Görsel İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürün Görselleri";
            ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5003/api/ProductImages/ProductImagesByProductId?id=" +id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductImageDto>(JsonData);
                return View(values);
            }
            return View();
        }
        
        [Route("ProductImageDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto updateProductImageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductImageDto);
            StringContent stringcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"http://localhost:5003/api/ProductImages",stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                return RedirectToAction("ProductListByCategory", "Product", new { area = "Admin" });  
            }
            
            return View();
            
        }

    }
}
