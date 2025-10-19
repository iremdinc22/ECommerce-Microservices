using System.Text;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
   
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index(string id)
        {
            ViewBag.i = id;
            return View();
        }
        
        public IActionResult ProductDetail(string id)
        {
            ViewBag.x = id;
            return View();
        }
        
        [HttpGet]
        public async Task  <PartialViewResult> AddComment(string id)
        {
            return PartialView();
        }
        
        [HttpPost]
        public async Task <IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.ImageUrl = "test";
            createCommentDto.Rating = 1;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status = false;
            createCommentDto.ProductId = "68ca9d589220c249f18ef89b";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringcontent = new StringContent(jsonData , Encoding.UTF8 , "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5013/api/Comments", stringcontent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }



    }
}
