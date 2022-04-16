using System.Threading.Tasks;
using ITunes.UrlShortener.Entities.Entities.MongoDB;
using ITunes.UrlShortener.Entities.RequestModel.User;
using ITunes.UrlShortener.Entities.ResponseModel;
using ITunes.UrlShortener.Repositories.Abstraction;
using ITunes.UrlShortener.Services.Abstraction;
using ITunes.UrlShortener.Services.Helper;
using ltunes.Security.Tools;
using MongoDB.Bson;

namespace ITunes.UrlShortener.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public UserService(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        public async Task<ApplicationResponse<bool>> AddUser(AddUserDto addUserDto)
        {
            var isValidUser = await _userRepository.GetAsync(x => x.EMail == addUserDto.EMail);
            if (isValidUser is not null)
            {
                return new ApplicationResponse<bool>(ResponseState.Error, ResponseMessage.Error.ValidUser);
            }

            var company = await _companyRepository.GetAsync(x => x.Id == addUserDto.CompanyId);
            if (company is null)
            {
                return new ApplicationResponse<bool>(ResponseState.Error, ResponseMessage.Error.CompanyNotFound);
            }

            // test kullanıcı olarak eklendi, kaldırılacaktır
            var salt = SecurityHelper.GenerateSalt(70);
            var hashPassword = SecurityHelper.HashPassword(addUserDto.Password, salt);

            var mongoUser = new User
            {
                EMail = addUserDto.EMail,
                Password = hashPassword,
                PasswordSalt = salt,
                Name = addUserDto.Name,
                Surname = addUserDto.Surname,
                Company = company
            };

            await _userRepository.AddAsync(mongoUser);

            return new ApplicationResponse<bool>(true);
        }

        public async Task<User> GetUserById(string userId)
        {
            var result = await _userRepository.GetByIdAsync(userId);
            return result;
        }
    }
}