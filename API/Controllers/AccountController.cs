﻿using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRepository.GetAll();
        if (!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        var result = _accountRepository.Create(account);
        if (result is null)
        {
            return StatusCode(500, "Error Retrieve from database");
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Account account)
    {
        var check = _accountRepository.GetByGuid(account.Guid);
        if (check is null)
        {
            return NotFound("Guid is not found");
        }

        var result = _accountRepository.Update(account);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Update Success");
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var data = _accountRepository.GetByGuid(guid);
        if (data is null)
        {
            return NotFound("Guid Is Not Found");
        }

        var result = _accountRepository.Delete(data);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Delete Success");
    }
}