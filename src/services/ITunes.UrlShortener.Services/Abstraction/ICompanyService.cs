using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.RequestModel.Company;
using ITunes.UrlShortener.Entities.ResponseModel;

namespace ITunes.UrlShortener.Services.Abstraction
{
    public interface ICompanyService
    {
        Task<ApplicationResponse<bool>> AddCompany(AddCompanyDto addCompanyDto);
    }
}