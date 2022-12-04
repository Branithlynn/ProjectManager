using Microsoft.AspNetCore.Mvc;

namespace ProjectManager.Controllers
{
    public class HomeController : Controller
    {
        //--------HOME PAGE-------------------//
        public IActionResult HomePage()
        {
            return View();
        }
        //------------------------------------//
    }
}