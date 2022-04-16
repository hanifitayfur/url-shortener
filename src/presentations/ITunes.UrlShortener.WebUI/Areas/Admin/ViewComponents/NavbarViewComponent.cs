using System.Threading.Tasks;
using ITunes.UrlShortener.WebUI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.WebUI.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "NavBar")]
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ISecurityTokenHandler _securityTokenHandler;

        public NavbarViewComponent(ISecurityTokenHandler securityTokenHandler)
        {
            _securityTokenHandler = securityTokenHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sessionUser = await _securityTokenHandler.GetSessionUser();
            return View(sessionUser);
        }
    }
}