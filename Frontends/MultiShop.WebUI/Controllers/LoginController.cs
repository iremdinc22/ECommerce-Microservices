using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MultiShop.WebUI.Models;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using AspNetCoreGeneratedDocument;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IIdentityService _identityService; 

        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(
                JsonSerializer.Serialize(createLoginDto),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("http://localhost:5001/api/Logins", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(
                    jsonData,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler= new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("multishoptoken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        //var id = _loginService.GetUserId;
                        return RedirectToAction("Index", "Default");
                    }
                }
            }

            return View();
        }

        //[HttpGet]
        //public IActionResult SignIn()
        //{
            ///return View();
        //}

       // [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            signInDto.Username = "iremd211";
            signInDto.Password = "id211DNC616*";
            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "Test");
        }

    }
}