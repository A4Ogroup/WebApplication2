using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}
