using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _bookingService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No Data Found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<BookingDto>>
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
        var result = _bookingService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }
        return Ok(new ResponseHandler<BookingDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Create(NewBookingDto newBookingDto)
    {
        var result = _bookingService.Create(newBookingDto);
        if (result is null)
        {
            return NotFound(new ResponseHandler<NewBookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        return Ok(new ResponseHandler<NewBookingDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = newBookingDto
        });
    }

    [HttpPut]
    public IActionResult Update(BookingDto bookingDto)
    {
        var result = _bookingService.Update(bookingDto);
        if (result is -1)
        {
            return NotFound(new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found"
            });
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error Retrieve from database"
            });
        }
        return Ok(new ResponseHandler<BookingDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update Success"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _bookingService.Delete(guid);
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

    [HttpGet("booked-today")]
    public IActionResult GetAllBookedBy()
    {
        var result = _bookingService.GetAllBookedBy();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<BookedByDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No bookings have been made today."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<BookedByDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });

    }

    [HttpGet("available-today")]
    public IActionResult GetAllAvailableRoom()
    {
        var result = _bookingService.GetAllAvailableRoom();
        if (result is null)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Room is not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<RoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieving data",
            Data = result
        });
    }

    [HttpGet("length")]
    public IActionResult BookingLength()
    {
        var result = _bookingService.BookingLength();
        if (result is null)
        {
            return NotFound(new ResponseHandler<BookingLengthDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Room is not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<BookingLengthDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieving data",
            Data = result
        });
    }

    [HttpGet("detail")]
    public IActionResult GetALlDetailBooking()
    {
        var result = _bookingService.GetAllBookingDetail();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<DetailBookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<DetailBookingDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieving data",
            Data = result
        });
    }

    [HttpPost("detail/{guid}")]
    public IActionResult GetDetailBookingByGuid(Guid guid)
    {
        var result = _bookingService.GetDetailBookingByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<DetailBookingDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<DetailBookingDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieving data",
            Data = result
        });

    }
}
