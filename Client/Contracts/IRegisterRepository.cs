using API.DTOs.Accounts;

namespace Client.Contracts
{
    public interface IRegisterRepository : IGeneralRepository<RegisterDto, Guid>
    { 
    }
}
