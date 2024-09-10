using Microsoft.AspNetCore.Mvc;
using Zero.Gateway.Param;

namespace Zero.Gateway.Controllers
{
    public class AuthController : MainController
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            // LoginReq loginReq
            //String token = await _authService.Login(userLoginParamReq);
            //throw new Exception(" test error");

            return Ok(__response);
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }
    }
}
