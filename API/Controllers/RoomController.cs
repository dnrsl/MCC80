using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomRepository.GetAll();
        if (!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        var result = _roomRepository.Create(room);
        if (result is null)
        {
            return StatusCode(500, "Error Retrieve from database");
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Room room)
    {
        var check = _roomRepository.GetByGuid(room.Guid);
        if (check is null)
        {
            return NotFound("Guid is not found");
        }

        var result = _roomRepository.Update(room);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Update Success");
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var data = _roomRepository.GetByGuid(guid);
        if (data is null)
        {
            return NotFound("Guid Is Not Found");
        }

        var result = _roomRepository.Delete(data);
        if (!result)
        {
            return StatusCode(500, "Error Retrieve from Database");
        }
        return Ok("Delete Success");
    }
}
