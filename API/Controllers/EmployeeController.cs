﻿using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            return NotFound(new ResponseHandler<EmployeeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Data Found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeDto>>
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
        var result = _employeeService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<EmployeeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }
        return Ok(new ResponseHandler<EmployeeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewEmployeeDto newEmployeeDto)
    {
        var result = _employeeService.Create(newEmployeeDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewEmployeeDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<NewEmployeeDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to create data",
            Data = newEmployeeDto
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeeDto employeeDto)
    {
        var result = _employeeService.Update(employeeDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Success"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _employeeService.Delete(guid);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete Success"
        });
    }
}
