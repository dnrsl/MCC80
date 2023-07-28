using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Accounts;

public class AccountDto
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    public static implicit operator Account(AccountDto accountDto)
    {
        return new Account
        {
            Guid = accountDto.Guid,
            //Password = accountDto.Password,
            Password = HashingHandler.GenerateHash(accountDto.Password),
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredTime = accountDto.ExpiredTime,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator AccountDto(Account account)
    {
        return new AccountDto
        {
            Guid = account.Guid,
            //Password = account.Password,
            Password = HashingHandler.GenerateHash(account.Password),
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
