using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        
    }
}