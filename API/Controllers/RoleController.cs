using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roleRepository.GetAll();
        if (!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Role role)
    {
        var result = _roleRepository.Create(role);
        if (result is null)
        {
            return StatusCode(500, "Error Retrieve from database");
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Role role)
    {
        var check = _roleRepository.GetByGuid(role.Guid);
        if (check is null)
        {
            return NotFound("Guid is not found");
        }

        var result = _roleRepository.Update(role);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Update Success");
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var data = _roleRepository.GetByGuid(guid);
        if (data is null)
        {
            return NotFound("Guid Is Not Found");
        }

        var result = _roleRepository.Delete(data);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Delete Success");
    }
}
