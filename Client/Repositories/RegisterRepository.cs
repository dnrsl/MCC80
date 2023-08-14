using API.DTOs.Accounts;
using Client.Contracts;

namespace Client.Repositories;

public class RegisterRepository : GeneralRepository<RegisterDto, Guid>, IRegisterRepository
{
    public RegisterRepository(string request = "accounts/register/") : base(request)
    {
        
    }
}
