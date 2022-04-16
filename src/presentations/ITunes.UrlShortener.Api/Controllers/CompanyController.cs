using System.Net;
using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Company;
using ITunes.UrlShortener.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITunes.UrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Add(AddCompanyDto request)
        {
            var result = await _companyService.AddCompany(request);
            return Ok(result);
        }
    }
}