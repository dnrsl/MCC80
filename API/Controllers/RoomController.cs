using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Roles;
using API.DTOs.Rooms;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
//[Authorize]
[EnableCors]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;
    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Data Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<RoomDto>>
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
        var result = _roomService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }
        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewRoomDto newRoomDto)
    {
        var result = _roomService.Create(newRoomDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<NewRoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<NewRoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success to create data",
            Data = newRoomDto
        });
    }

    [HttpPut]
    public IActionResult Update(RoomDto roomDto)
    {
        var result = _roomService.Update(roomDto);

        if (result is -1)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Success"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _roomService.Delete(guid);

        if (result is -1)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete Success"
        });
    }
}
