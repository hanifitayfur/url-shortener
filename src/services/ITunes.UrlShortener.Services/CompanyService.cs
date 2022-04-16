using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Entities.RequestModel.Company;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Repositories.Abstraction;
using ITunes.UrlShortener.Services.Abstraction;
using ITunes.UrlShortener.Services.Helper;

namespace ITunes.UrlShortener.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ApplicationResponse<bool>> AddCompany(AddCompanyDto addCompanyDto)
        {
            var isValidUser = await _companyRepository.GetAsync(x => x.Name == addCompanyDto.Name.Trim());
            if (isValidUser is not null)
            {
                return new ApplicationResponse<bool>(ResponseState.Error, ResponseMessage.Error.ValidCompany);
            }

            await _companyRepository.AddAsync(new Company
            {
                Name = addCompanyDto.Name,
                Phone = addCompanyDto.Phone
            });


            return new ApplicationResponse<bool>(true);
        }
    }
}