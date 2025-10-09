using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSlidersController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureSlidersList()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> FeatureSliderById(string id)
        {
            var value = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSliders(CreateFeatureSliderDto createFeatureSliderDto)
        {
           await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
           return Ok("Feature Slider created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSliders(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Feature Slider updated successfully");
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSliders(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Feature Slider deleted successfully");
        }

        
        
    }
}
