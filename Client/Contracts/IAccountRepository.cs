using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Client.Contracts;

public interface IAccountRepository : IGeneralRepository<LoginDto, Guid>
{
    public Task<ResponseHandler<TokenDto>> Login(LoginDto loginDto);
}
