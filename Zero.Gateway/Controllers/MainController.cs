using Microsoft.AspNetCore.Mvc;
using Zero.Gateway.BasePrototype;

namespace Zero.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected MainResponse __response = new();

     

    }
}
