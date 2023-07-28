using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Data Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<AccountDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }
        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewAccountDto newAccountDto)
    {
        var result = _accountService.Create(newAccountDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewAccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<NewAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to create data",
            Data = newAccountDto
        });
    }

    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {

        var result = _accountService.Update(accountDto);

        if (result is -1)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Success"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _accountService.Delete(guid);

        if (result is -1)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete Success"
        });
    }

    [HttpPost("login")]
    public IActionResult Login (LoginDto loginDto)
    {
        var result = _accountService.login(loginDto);

        if (result is 0)
        {
            return NotFound(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email or Password is incorrect"
            });
        }

        return Ok(new ResponseHandler<LoginDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success"
        });
    }

    [HttpPost("register")]
    public IActionResult Register (RegisterDto registerDto)
    {
        var result = _accountService.Register(registerDto);
        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        else if (result is -1)
        {
            return BadRequest(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Your password is not match"
            });
        }

        return Ok(new ResponseHandler<RegisterDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to register",
            Data = registerDto
        });
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var result = _accountService.ForgotPassword(forgotPasswordDto);

        if (result is 0)
        {
            return NotFound(new ResponseHandler<ForgotPasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email is incorrect "
            });
        }

        return Ok(new ResponseHandler<ForgotPasswordDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to send otp, please check your email"
        }); ;
    }

    [HttpPost("change-password")]
    public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var result = _accountService.ChangePassword(changePasswordDto);
        if (result is 0)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email is incorrect"
            });
        }

        else if(result is -1)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Invalid Otp"
            });
        }

        else if (result is -2)
        {
            return BadRequest(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp has been expired"
            });
        }

        else if (result is -3)
        {
            return BadRequest(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is already used"
            });
        }

        else if (result is -4)
        {
            return BadRequest(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "New Password and Confirm Password is not match"
            });
        }

        return Ok(new ResponseHandler<ChangePasswordDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to change password",
            Data = changePasswordDto
        });
    }
}
