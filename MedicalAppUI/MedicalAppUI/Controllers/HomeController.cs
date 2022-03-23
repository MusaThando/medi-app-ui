using Microsoft.AspNetCore.Mvc;

namespace MedicalAppUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.UserType = HttpContext.Session.GetString("Typefld");
            ViewBag.Password = HttpContext.Session.GetString("Passwordfld");
            ViewBag.PersonGuid = HttpContext.Session.GetString("PersonGuid");

            ViewBag.Heading = "Management Center";

            return View();
        }
    }
}
