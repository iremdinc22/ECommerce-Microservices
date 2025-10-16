using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _VendorDefaultComponentPartial :  ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _VendorDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task <IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var ResponseMessage = await client.GetAsync("http://localhost:5003/api/Brands");
        if (ResponseMessage.IsSuccessStatusCode)
        {
            var JsonData = await ResponseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(JsonData);
            return View(values);
        }
        return View();
    }
}