using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents;

public class _ProductDetailFeatureComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _ProductDetailFeatureComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
        
 
    public async Task <IViewComponentResult> InvokeAsync(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5003/api/Products/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var JsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateProductDto>(JsonData);
            return View(values);
        }
        return View();
    }
    
}