﻿using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _employeeService.GetAll();
        if (!result.Any())
        {
            return NotFound("No data found");
        }
        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _employeeService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Guid is not found");
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(NewEmployeeDto newEmployeeDto)
    {
        var result = _employeeService.Create(newEmployeeDto);
        if (result is null)
        {
            return StatusCode(500, "Error Retrieve from database");
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EmployeeDto employeeDto)
    {
        var result = _employeeService.Update(employeeDto);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }
        if (result is 0)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Update Success");
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _employeeService.Delete(guid);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }
        if (result is 0)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Delete Success");
    }
}
