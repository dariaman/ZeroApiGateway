using Microsoft.AspNetCore.Mvc;
using Zero.Gateway.BasePrototype;

namespace Zero.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected MainResponse __response = new();

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            //String token = await _authService.Login(userLoginParamReq);
            //return Ok(new BaseResponse { data = new { token } });
            return Ok();
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }

    }
}
