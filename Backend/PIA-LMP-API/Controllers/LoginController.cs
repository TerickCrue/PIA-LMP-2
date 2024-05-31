using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIA_LMP_API.Data.Dto;
using PIA_LMP_API.Services;

namespace PIA_LMP_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task <IActionResult> Authenticate(LoginRequest loginRequest)
        {
            var loginResult = loginService.Authenticate(loginRequest);
            return Ok(loginResult);

        }


    }
}
