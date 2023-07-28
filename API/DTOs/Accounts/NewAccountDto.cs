using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Accounts;

public class NewAccountDto
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    public static implicit operator Account(NewAccountDto newAccountDto)
    {
        return new Account
        {
            Guid = newAccountDto.Guid,
            //Password = newAccountDto.Password,
            Password = HashingHandler.GenerateHash(newAccountDto.Password),
            Otp = newAccountDto.Otp,
            IsUsed = newAccountDto.IsUsed,
            ExpiredTime = newAccountDto.ExpiredTime,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
