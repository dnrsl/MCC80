using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public AccountService (IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
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

    public RegisterDto? Register(RegisterDto registerDto)
    {
        var newEmployee = new Employee
        {
            Nik = GenerateNikHandler.GenereateNewNik(_employeeRepository.GetLastNik()),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            BirthDate = registerDto.BirthDate,
            Gender = registerDto.Gender,
            HiringDate = registerDto.HiringDate,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        };

        var createdEmployee = _employeeRepository.Create(newEmployee);
        if (createdEmployee is null)
        {
            return null;
        }

        var existingUniversity = _universityRepository.GetByCode(registerDto.UniversityCode);


        if (existingUniversity is null)
        {
            var university = new University
            {
                Guid = new Guid(),
                Code = registerDto.UniversityCode,
                Name = registerDto.UniversityName,
                CreatedDate = createdEmployee.CreatedDate,
                ModifiedDate = createdEmployee.ModifiedDate,
            };

            _universityRepository.Create(university);
        }

        var newEducation = new Education
        {
            Guid = createdEmployee.Guid,
            UniversityGuid = existingUniversity.Guid,
            Major = registerDto.Major,
            Degree = registerDto.Degree,
            Gpa = registerDto.Gpa,
            CreatedDate = createdEmployee.CreatedDate,
            ModifiedDate = createdEmployee.ModifiedDate,
        };

        var createdEducation = _educationRepository.Create(newEducation);
        if (createdEducation is null)
        {
            _employeeRepository.Delete(createdEmployee);
            return null;
        }

        var newAccount = new Account
        {
            Guid = createdEmployee.Guid,
            Password = registerDto.Password,
            CreatedDate = createdEmployee.CreatedDate,
            ModifiedDate = createdEmployee.ModifiedDate,
        };

        if (registerDto.Password != registerDto.RepeatPassword)
        {
            return null;
        }

        var createdAccount = _accountRepository.Create(newAccount);
        if (createdAccount is null)
        {
            _employeeRepository.Delete(createdEmployee);
            return null;
        }

        return registerDto;
    }

    
    public int ForgotPassword (ForgotPasswordDto forgotPasswordDto)
    {
        /*
        var getEmployee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);

        if (getEmployee is null)
        {
            return 0;
        }


        var updateAccount = _accountRepository.GetByGuid(getEmployee.Guid);

        if (updateAccount is null)
        {
            return 0;
        }
        */

        var getAccountDetail = (from e in _employeeRepository.GetAll()
                               join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                               where e.Email == forgotPasswordDto.Email
                               select a).FirstOrDefault();

        _accountRepository.Clear();

        if (getAccountDetail is null)
        {
            return 0;
        }

        var otp = GenerateOtp.Otp();
        var account = new Account
        {
            Guid = getAccountDetail.Guid,
            Password = getAccountDetail.Password,
            Otp = Convert.ToInt32(otp),
            ExpiredTime = DateTime.Now.AddMinutes(5),
            IsUsed = false,
            CreatedDate = getAccountDetail.CreatedDate,
            ModifiedDate = DateTime.Now
        };

        _accountRepository.Update(account);
        return 1;
    }

    public int ChangePassword (ChangePasswordDto changePasswordDto)
    {
        var getEmployee = _employeeRepository.GetByEmail(changePasswordDto.Email);
        if(getEmployee is null)
        {
            return 0;
        }

        var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
        if(getAccount.IsUsed is true)
        {
            return -1;
        }

        var getOtp = Convert.ToString(getAccount.Otp);

        if(getOtp != changePasswordDto.Otp)
        {
            return -2;
        }

        if(getAccount.ExpiredTime <= DateTime.Now)
        {
            return -3;
        }

        if(changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
        {
            return -4;
        }

        getAccount.Password = changePasswordDto.NewPassword;
        getAccount.Otp = getAccount.Otp;
        getAccount.ExpiredTime = DateTime.Now;
        getAccount.IsUsed = true;

        _accountRepository.Update(getAccount);

        return 1;
    }
}
