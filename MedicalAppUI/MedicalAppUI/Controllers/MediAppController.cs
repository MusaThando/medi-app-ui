using MedicalAppUI.DAL;
using MedicalAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MedicalAppUI.Controllers
{
    public class MediAppController : Controller
    {
        public const string baseUrl = "https://rest.mediapp.co.za/";
        public Guid userType = Guid.Parse("7BE8F6AD-8B8F-411D-9FE3-1DFD94207034");
        public Guid AdminType = Guid.Parse("CA8BF789-875E-441A-B751-9BA336738091");
        public Guid doctorType = Guid.Parse("E367B4B5-519A-40A8-908E-949219354F08");
        DataAccess dataAccess = new DataAccess();

        #region Get Functions
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("PersonGuid") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            GetSessionVariables();

            ViewBag.Heading = "Management Center";

            return View();
        }

        public async Task<IActionResult> Transaction() 
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            var cDisease = new CDisease();

            ViewBag.Heading = "Transaction Management";

            GetSessionVariables();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-transactions", baseUrl)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        cDisease.DiseaseList = JsonConvert.DeserializeObject<List<CDisease>>(apiResponse);
                        cDisease.DoctorGUIDfld = Guid.Parse(HttpContext.Session.GetString("PersonGuid"));
                        ViewBag.Users = new SelectList(GetDropdownUserList(), "PersonGUIDfld", "FullNamefld");

                        return View(cDisease);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> Profile()
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            _ = new CViewModel();

            ViewBag.Heading = "Profile Management";
            GetSessionVariables();

            Guid personGuid = Guid.Parse(HttpContext.Session.GetString("PersonGuid"));

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-user-profile?personGuid={1}", baseUrl, personGuid)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        CViewModel? profile = JsonConvert.DeserializeObject<CViewModel>(apiResponse);

                        if (profile != null)
                        {
                            profile.PasswordCheck = profile.Passwordfld;
                            profile.PersonGUIDfld = personGuid;
                        }
                        return View(profile);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> Doctors()
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            var doctors = new CViewModel();

            ViewBag.Heading = "Doctor Management";

            GetSessionVariables();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-doctors", baseUrl)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        doctors.DoctorList = JsonConvert.DeserializeObject<List<CViewModel>>(apiResponse);
                        doctors.UserTypeGUIDfld = doctorType;

                        return View(doctors);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> Users()
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            var user = new CUsers();
            ViewBag.Heading = "Patient Management";
            GetSessionVariables();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-users", baseUrl)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        user.UsersList = JsonConvert.DeserializeObject<List<CUsers>>(apiResponse);
                        user.UserTypeGUIDfld = userType;
                        return View(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> UserDetails(Guid personGuid)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            _ = new CUsers();

            GetSessionVariables();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-user-details?personGuid={1}", baseUrl, personGuid)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        CUsers? user = JsonConvert.DeserializeObject<CUsers>(apiResponse);
                        if (user != null) 
                        {
                            if (user.UserTypeGUIDfld == userType) 
                            {
                                ViewBag.Heading = "Patient: " + user.FirstNamefld + " " + user.LastNamefld;
                            }

                            if (user.UserTypeGUIDfld == AdminType)
                            {
                                ViewBag.Heading = "Admin: " + user.FirstNamefld + " " + user.LastNamefld;
                            }

                            if (user.UserTypeGUIDfld == doctorType)
                            {
                                ViewBag.Heading = "Doctor: " + user.FirstNamefld + " " + user.LastNamefld;
                            }
                        }
                        return View(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> UploadDocument(Guid personGuid)
        {
            _ = new CDocumentRepository();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/document-upload?personGuid={1}", baseUrl, personGuid)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        CDocumentRepository? cDocumentRepository = JsonConvert.DeserializeObject<CDocumentRepository>(apiResponse);

                        if (cDocumentRepository != null)
                        {
                            ViewBag.UserType = cDocumentRepository.UserTypefld;
                            return PartialView("UploadDocument", cDocumentRepository);
                        }
                        { return NotFound(); }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> DocumentView(Guid personGuid, string requestorPage)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            _ = new CDocumentRepository();
            GetSessionVariables();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/document-view?personGuid={1}", baseUrl, personGuid)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        CDocumentRepository? cDocumentRepository = JsonConvert.DeserializeObject<CDocumentRepository>(apiResponse);

                        if (cDocumentRepository != null)
                        {
                            cDocumentRepository.RequestorPage = requestorPage;
                            cDocumentRepository.SourceRecordGUIDfld = personGuid;
                            return PartialView("DocumentView", cDocumentRepository);
                        }
                        { return NotFound(); }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> Documents()
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            var documents = new CDocumentRepository();
            ViewBag.Heading = "Document Management";
            GetSessionVariables();


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-user-documents", baseUrl)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        documents.DocumentList = JsonConvert.DeserializeObject<List<CDocumentRepository>>(apiResponse);
                        documents.SourceRecordGUIDfld = Guid.Parse(HttpContext.Session.GetString("PersonGuid")); 
                        return View(documents);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        public async Task<IActionResult> Administration()
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            var admin = new CViewModel();

            ViewBag.Heading = "Admin Management";

            GetSessionVariables();

            using var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-admin-users", baseUrl)))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    admin.AdminList = JsonConvert.DeserializeObject<List<CViewModel>>(apiResponse);
                    admin.UserTypeGUIDfld = AdminType;

                    return View(admin);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public async Task<IActionResult> DownloadDocument(Guid documentRepository)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }

            _ = new CDocumentRepository();

            ViewBag.Heading = "Admin Management";

            GetSessionVariables();

            using var httpClient = new HttpClient();
            using (var response = await httpClient.GetAsync(string.Format("{0}/mediApp/download-document?documentRepository={1}", baseUrl, documentRepository)))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    CDocumentRepository? cDocumentRepository = JsonConvert.DeserializeObject<CDocumentRepository>(apiResponse);

                    if (cDocumentRepository != null && cDocumentRepository.FileContentfld != null)
                        return File(cDocumentRepository.FileContentfld, "application/octetstream", cDocumentRepository.FileNamefld);
                    else
                        return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }
        }
        #endregion

        #region Post Functions
        public IActionResult CreateUserDocument(CDocumentRepository cDocumentRepository)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignOut", "SignIn");
            }

            cDocumentRepository.FileTypefld = GetFileType(cDocumentRepository);

            Byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                cDocumentRepository.File.OpenReadStream().CopyTo(ms);
                bytes = ms.ToArray();
            }

            cDocumentRepository.FileContentfld = bytes;
            cDocumentRepository.FileNamefld = cDocumentRepository.File.FileName;

            dataAccess.PostDocument(cDocumentRepository);

            return RedirectToAction("Users");
        }

        public IActionResult CreateAdminDocument(CDocumentRepository cDocumentRepository)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignOut", "SignIn");
            }

            cDocumentRepository.FileTypefld = GetFileType(cDocumentRepository);

            Byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                cDocumentRepository.File.OpenReadStream().CopyTo(ms);
                bytes = ms.ToArray();
            }

            cDocumentRepository.FileContentfld = bytes;
            cDocumentRepository.FileNamefld = cDocumentRepository.File.FileName;

            dataAccess.PostDocument(cDocumentRepository);

            return RedirectToAction("Administration");
        }

        public IActionResult CreateDoctorDocument(CDocumentRepository cDocumentRepository)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignOut", "SignIn");
            }

            cDocumentRepository.FileTypefld = GetFileType(cDocumentRepository);

            Byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                cDocumentRepository.File.OpenReadStream().CopyTo(ms);
                bytes = ms.ToArray();
            }

            cDocumentRepository.FileContentfld = bytes;
            cDocumentRepository.FileNamefld = cDocumentRepository.File.FileName;

            dataAccess.PostDocument(cDocumentRepository);

            return RedirectToAction("Doctors");
        }

        public IActionResult CreateDocument(CDocumentRepository cDocumentRepository)
        {
            if (HttpContext.Session.GetString("Typefld") == null)
            {
                return RedirectToAction("SignOut", "SignIn");
            }

            cDocumentRepository.FileTypefld = GetFileType(cDocumentRepository);

            Byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                cDocumentRepository.File.OpenReadStream().CopyTo(ms);
                bytes = ms.ToArray();
            }

            cDocumentRepository.FileContentfld = bytes;
            cDocumentRepository.FileNamefld = cDocumentRepository.File.FileName;

            dataAccess.PostDocument(cDocumentRepository);

            return RedirectToAction("Documents");
        }


        #endregion

        #region Custom Functions
        private void GetSessionVariables()
        {
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.UserType = HttpContext.Session.GetString("Typefld");
            ViewBag.Password = HttpContext.Session.GetString("Passwordfld");
            ViewBag.PersonGuid = HttpContext.Session.GetString("PersonGuid");
        }
        public string GetFileType(CDocumentRepository cDocumentRepository)
        {
            string sFileType = "";

            if (cDocumentRepository.File.FileName.Contains(".doc") || cDocumentRepository.File.FileName.Contains(".rtf"))
            {
                sFileType = "doc";
            }

            if (cDocumentRepository.File.FileName.Contains(".pdf"))
            {
                sFileType = "pdf";
            }

            if (cDocumentRepository.File.FileName.Contains(".xls"))
            {
                sFileType = "xls";
            }

            if (cDocumentRepository.File.FileName.Contains(".txt"))
            {
                sFileType = "txt";
            }

            if (cDocumentRepository.File.FileName.Contains(".jpeg") || cDocumentRepository.File.FileName.Contains(".jpg"))
            {
                sFileType = "jpeg";
            }

            if (cDocumentRepository.File.FileName.Contains(".png") || cDocumentRepository.File.FileName.Contains(".PNG"))
            {
                sFileType = "png";
            }

            return sFileType;
        }
        public List<CViewModel> GetDropdownUserList()
        {
            var list = new List<CViewModel>();  

            using (var httpClient = new HttpClient())
            using (var response =  httpClient.GetAsync(String.Format("{0}/mediApp/get-dropdown-user-list", baseUrl)))
            {
                var res = response.Result;
                if (res.IsSuccessStatusCode)
                {
                    string apiResponse = res.Content.ReadAsStringAsync().Result;

                    list = JsonConvert.DeserializeObject<List<CViewModel>>(apiResponse);
                }
            }
            
            return list;
        }
        #endregion

        #region Get Partials 
        public async Task<IActionResult> GetSearchResultsByCode(string code) 
        {
            var disease = new CDisease();

            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-disease-by-code?code={1}", baseUrl,code)))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    disease.DiseaseList = JsonConvert.DeserializeObject<List<CDisease>>(apiResponse).ToList();

                    return PartialView("SearchResulstPartial",disease);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        public async Task<IActionResult> GetSearchResultsByDescription(string description)
        {
            var disease = new CDisease();

            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(String.Format("{0}/mediApp/get-disease-by-description?description={1}", baseUrl, description)))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    disease.DiseaseList = JsonConvert.DeserializeObject<List<CDisease>>(apiResponse).ToList();

                    return PartialView("SearchResulstPartial", disease);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        #endregion

    }
}
