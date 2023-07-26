using API.Contracts;
using API.DTOs.Accounts;
using API.Models;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AccountService (IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<AccountDto> GetAll()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return Enumerable.Empty<AccountDto>();
        }

        var accountDtos = new List<AccountDto>();
        foreach (var account in accounts)
        {
            accountDtos.Add((AccountDto)account);
        }

        return accountDtos;
    }

    public AccountDto? GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if(account is null)
        {
            return null;
        }

        return (AccountDto?)account;
    }

    public AccountDto? Create (NewAccountDto newAccountDto)
    {
        var account = _accountRepository.Create(newAccountDto);
        if (account is null)
        {
            return null;
        }

        return (AccountDto)account;
    }

    public int Update (AccountDto accountDto)
    {
        var account = _accountRepository.GetByGuid(accountDto.Guid);
        if( account is null)
        {
            return -1;
        }

        Account toUpdate = accountDto;
        toUpdate.CreatedDate = account.CreatedDate;
        var result = _accountRepository.Update(toUpdate);
        
        return result ? 1 : 0;
    }

    public int Delete (Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if ( account is null)
        {
            return -1;
        }

        var result = _accountRepository.Delete(account);
        return result ? 1 : 0;
    }

    public int login(LoginDto loginDto)
    {
        var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);

        if(getEmployee is null)
        {
            return 0;
        }

        var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

        if (getAccount.Password == loginDto.Password)
        {
            return 1;
        }

        return 0;
    }
}
