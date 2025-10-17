using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents;

public class _ProductListComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ProductListComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task <IViewComponentResult> InvokeAsync(string id)
    {
        var client = _httpClientFactory.CreateClient();
        var ResponseMessage = await client.GetAsync("http://localhost:5003/api/Products/ProductListWithCategoryByCategoryId?id=" +id);
        if (ResponseMessage.IsSuccessStatusCode)
        {
            var JsonData = await ResponseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductsWithCategoryDto>>(JsonData);
            return View(values);
        }
        return View(); 
    }
    
}