using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents;

public class _FeatureDefaultComponentPartial :  ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _FeatureDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task <IViewComponentResult> InvokeAsync()
    {
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
}