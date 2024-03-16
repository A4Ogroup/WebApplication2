using Microsoft.AspNetCore.Mvc;

namespace first.Controllers
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




    }
}
