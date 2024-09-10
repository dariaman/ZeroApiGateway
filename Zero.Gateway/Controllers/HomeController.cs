using Microsoft.AspNetCore.Mvc;

namespace Zero.Gateway.Controllers
{
    public class HomeController : MainController
    {
        [HttpGet("Version")]
        public ContentResult GetVersion() => Content("v1.0.0");

    }
}
