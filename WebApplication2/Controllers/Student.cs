using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class Student : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            //itsme
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }




    }
}
