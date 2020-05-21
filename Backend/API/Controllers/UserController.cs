using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(new
            {
                message = "Hello World"
            });
        }
    }
}