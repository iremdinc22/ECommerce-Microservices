using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents;

public class _NavbarUILayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _NavbarUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
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
    
}