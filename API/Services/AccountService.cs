﻿using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using System.Transactions;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly BookingDbContext _bookingDbContext;
    private readonly IEmailHandler _emailHandler;
    private readonly ITokenHandler _tokenHandler;

    public AccountService (IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, BookingDbContext bookingDbContext, IEmailHandler emailHandler, ITokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository)
    {
        _employeeRepository = employeeRepository;
        _accountRepository = accountRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _bookingDbContext = bookingDbContext;
        _emailHandler = emailHandler;
        _tokenHandler = tokenHandler;
        _accountRoleRepository = accountRoleRepository;
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

    public string login(LoginDto loginDto)
    {
    
        //var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);

        //if(getEmployee is null)
        //{
        //    return "0";
        //}

        //var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

        ///*
        //if (getAccount.Password == loginDto.Password)
        //{
        //    return 1;
        //}
        //*/

        //if (HashingHandler.ValidateHash(loginDto.Password, getAccount.Password))
        //{
        //    var employee = _employeeRepository.GetByEmail(loginDto.Email);

        //    var claims = new List<Claim>
        //    {
        //        new Claim ("Guid", employee.Guid.ToString()),
        //        new Claim ("FullName", $"{employee.FirstName} {employee.LastName}"),
        //        new Claim ("Email", employee.Email)
        //    };

        //    var generatedToken = _tokenHandler.GenereateToken(claims);

        //    if (generatedToken is null)
        //    {
        //        return "-1";
        //    }

        //    return generatedToken;
        //}

        //return "0";
        
        var getAccount = (from e in _employeeRepository.GetAll()
                         join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                         where e.Email == loginDto.Email
                         select a).FirstOrDefault();

        if (HashingHandler.ValidateHash(loginDto.Password, getAccount.Password))
        {
            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(employee.Guid);

            var claims = new List<Claim>
                {
                    new Claim ("Guid", employee.Guid.ToString()),
                    new Claim ("FullName", $"{employee.FirstName} {employee.LastName}"),
                    new Claim ("Email", employee.Email)
                };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var generatedToken = _tokenHandler.GenereateToken(claims);

            if (generatedToken is null)
            {
                return "0";
            }

            return generatedToken;
        }

        return null;
    }

    public int Register(RegisterDto registerDto)
    {
        using var transaction = _bookingDbContext.Database.BeginTransaction();

        try
        {
            var existingUniversity = _universityRepository.GetByCode(registerDto.UniversityCode);

            if (existingUniversity is null)
            {
                var university = _universityRepository.Create(new University
                {
                    Guid = new Guid(),
                    Code = registerDto.UniversityCode,
                    Name = registerDto.UniversityName,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                });

                existingUniversity = university; 
            }

            var createdEmployee = _employeeRepository.Create(new Employee
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
            });

            var createdEducation = _educationRepository.Create(new Education
            {
                Guid = createdEmployee.Guid,
                UniversityGuid = existingUniversity.Guid,
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                Gpa = registerDto.Gpa,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            });

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                transaction.Rollback();
                return -1;
            }

            var HashedPassword = HashingHandler.GenerateHash(registerDto.Password);

            var createdAccount = _accountRepository.Create(new Account
            {
                Guid = createdEmployee.Guid,
                //Password = registerDto.Password,
                Password = HashedPassword, //diubah dengan teknik hash
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            });

            var getAccountRole = _accountRoleRepository.Create(new NewAccountRoleDto
            {
                AccountGuid = createdEmployee.Guid,
                RoleGuid = Guid.Parse("083e7d1a-0ae6-4508-9fa7-08db92312884")
            });

            transaction.Commit();
            return 1;
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }
        
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
            Otp = otp,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            IsUsed = false,
            CreatedDate = getAccountDetail.CreatedDate,
            ModifiedDate = DateTime.Now
        };
        _accountRepository.Update(account);
        _emailHandler.SendEmail(forgotPasswordDto.Email, "Account - Forgot Password", $"Your Otp is {otp}");

        return 1;
    }

    public int ChangePassword (ChangePasswordDto changePasswordDto)
    {
        /*
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
        */
        var getAccountDetail = (from e in _employeeRepository.GetAll()
                                join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                where e.Email == changePasswordDto.Email
                                select a).FirstOrDefault();

        _accountRepository.Clear();
        if (getAccountDetail is null)
        {
            return 0; //Email is incorrect
        }

       
        if (getAccountDetail.Otp != changePasswordDto.Otp)
        {
            return -1; //Invalid Otp
        }

        if(getAccountDetail.ExpiredTime <= DateTime.Now)
        {
            return -2; //Otp has been expired
        }

        if(getAccountDetail.IsUsed is true)
        {
            return -3; //Otp is already used
        }

        if(changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
        {
            return -4; //Password is not match
        }

        var hashedPassword = HashingHandler.GenerateHash(changePasswordDto.NewPassword);

        var account = new Account
        {
            Guid = getAccountDetail.Guid,
            //Password = changePasswordDto.NewPassword,
            Password = hashedPassword,
            Otp = Convert.ToInt32(changePasswordDto.Otp),
            ExpiredTime = getAccountDetail.ExpiredTime,
            IsUsed = true,
            CreatedDate = getAccountDetail.CreatedDate,
            ModifiedDate = DateTime.Now,
        };

        _accountRepository.Update(account);
        return 1;
    }
}
