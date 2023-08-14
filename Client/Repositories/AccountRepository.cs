using API.DTOs.Accounts;
using API.Utilities.Handlers;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : GeneralRepository<LoginDto, Guid>, IAccountRepository
{
    private readonly string request;
    private readonly HttpClient httpClient;
    public AccountRepository(string request = "accounts/") : base(request)
    {
        this.request = request;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7181/api/")
        };
    }

    public async Task<ResponseHandler<TokenDto>> Login(LoginDto loginDto)
    {
        ResponseHandler<TokenDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "Login", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenDto>>(apiResponse);
        }
        return entityVM;
    }

}

