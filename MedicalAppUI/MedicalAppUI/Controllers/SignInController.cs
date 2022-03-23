using MedicalAppUI.Models;
using MedicalAppUI.SQL;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppUI.Controllers
{
    public class SignInController : Controller
    {

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignOut() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        public IActionResult ValidateSignIn(CViewModel cViewModel)
        {
            if (cViewModel.PersonGUIDfld == null || cViewModel.PersonGUIDfld == Guid.Empty)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            SetSessionData(cViewModel);

            return Ok(1);
        }

        void SetSessionData(CViewModel cViewModel)
        {
            string encryptedPassword = Helper.EncryptWithMD5(cViewModel.Passwordfld);

            HttpContext.Session.SetString("FullName", cViewModel.FullNamefld);
            HttpContext.Session.SetString("Typefld", cViewModel.Typefld);
            HttpContext.Session.SetString("Passwordfld", encryptedPassword);
            HttpContext.Session.SetString("PersonGuid", cViewModel.PersonGUIDfld.ToString());

            ViewBag.Heading = "Action Management Center";
        }
    }
}
