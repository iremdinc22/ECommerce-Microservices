using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents;

public class _ProductDetailImageSliderComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
        
 
    public async Task <IViewComponentResult> InvokeAsync(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5003/api/ProductImages/ProductImagesByProductId?id=" +id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var JsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(JsonData);
            return View(values);
        }
        return View();
     }
     
}