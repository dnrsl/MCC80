using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;

namespace API.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<BookingDto> GetAll()
    {
        var booking = _bookingRepository.GetAll();
        if (!booking.Any())
        {
            return Enumerable.Empty<BookingDto>();
        }

        var bookingDtos = new List<BookingDto>();
        foreach (var bookingDto in booking)
        {
            bookingDtos.Add((BookingDto)bookingDto);
        }

        return bookingDtos;
    }

    public BookingDto? GetByGuid(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if(booking is null)
        {
            return null;
        }

        return (BookingDto)booking;
    }

    public BookingDto? Create (NewBookingDto newBookingDto)
    {
        var education = _bookingRepository.Create(newBookingDto);
        if (education is null)
        {
            return null;
        }

        return (BookingDto)education;
    }

    public int Update(BookingDto bookingDto)
    {
        var booking = _bookingRepository.GetByGuid(bookingDto.Guid);

        if (booking is null)
        {
            return -1;
        }

        Booking toUpdate = bookingDto;
        toUpdate.CreatedDate = booking.CreatedDate;
        var result = _bookingRepository.Update(toUpdate);

        return result ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return -1;
        }

        var result = _bookingRepository.Delete(booking);
        return result ? 1 : 0; 
    }

    public IEnumerable<RoomDto> FreeRoomsToday()
    {
        List<RoomDto> roomDtos = new List<RoomDto>();
        var bookings = GetAll();
        var freeBookings = bookings.Where(b => b.Status == Utilities.Enums.StatusLevel.Done);

        var freeBookingsToday = freeBookings.Where(b => b.EndDate < DateTime.Now);
        foreach(var booking in freeBookingsToday)
        {
            var roomGuid = booking.Guid;
            var room = _roomRepository.GetByGuid(roomGuid);
            RoomDto roomDto = new RoomDto()
            {
                Guid = roomGuid,
                Name = room.Name,
                Capacity = room.Capacity,
                Floor = room.Floor
            };
            roomDtos.Add(roomDto);
        }

        if (!roomDtos.Any())
        {
            return null;
        }

        return roomDtos;
    }

    public IEnumerable<BookingLengthDto> BookingLength()
    {
        List<BookingLengthDto> listBookingLength = new List<BookingLengthDto>();
        TimeSpan workingHour = new TimeSpan(8, 30, 0);
        var timeSpan = new TimeSpan();
        var bookings = GetAll();
        foreach( var booking in bookings)
        {
            var currentDate = booking.StartDate;
            var endDate = booking.EndDate;
            
            while( currentDate <= endDate )
            {
                if(currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday )
                {
                    DateTime openRooom = currentDate.Date.AddHours(9);
                    DateTime closeRoom = currentDate.Date.AddHours(17).AddMinutes(30);

                    TimeSpan dayTime = closeRoom - openRooom;
                    timeSpan += dayTime;
                }

                currentDate = currentDate.AddDays(1);
            }

            var room = _roomRepository.GetByGuid(booking.RoomGuid);
            var bookingLengthDto = new BookingLengthDto()
            {
                RoomGuid = booking.RoomGuid,
                RoomName = room.Name,
                BookingLength = timeSpan.TotalHours
            };

            listBookingLength.Add(bookingLengthDto);
        }

        if (!listBookingLength.Any())
        {
            return null;
        }
        return listBookingLength;
    }

    public IEnumerable<DetailBookingDto> GetAllBookingDetail()
    {
        var resultBooking = _bookingRepository.GetAll();
        if (!resultBooking.Any())
        {
            return Enumerable.Empty<DetailBookingDto>();
        }

        var detailDtos = new List<DetailBookingDto>();
        foreach ( var result in resultBooking)
        {
            var resultEmployee = _employeeRepository.GetByGuid(result.EmployeeGuid);
            if (resultEmployee != null)
            {
                return Enumerable.Empty<DetailBookingDto>();
            }

            var resultRoom = _roomRepository.GetByGuid(result.RoomGuid);
            if (resultRoom != null)
            {
                return Enumerable.Empty<DetailBookingDto>();
            }

            var toDto = new DetailBookingDto
            {
                BookingGuid = result.Guid,
                BookedNik = resultEmployee.Nik,
                BookedBy = resultEmployee.FirstName+" "+resultEmployee.LastName,
                RoomName = resultRoom.Name,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                Remarks = result.Remarks,
            };

            detailDtos.Add(toDto);
        }

        return detailDtos;
    }

    public DetailBookingDto? GetDetailBookingByGuid(Guid guid)
    {
        var resultBooking = _bookingRepository.GetByGuid(guid);
        if (resultBooking is null )
        {
            return null;
        }

        var resultEmployee = _employeeRepository.GetByGuid(resultBooking.EmployeeGuid);
        if (resultEmployee is null )
        {
            return null;
        }

        var resultRoom = _roomRepository.GetByGuid(resultBooking.RoomGuid);
        if (resultRoom is null)
        {
            return null;
        }

        var toDto = new DetailBookingDto
        {
            BookingGuid = resultBooking.Guid,
            BookedNik = resultEmployee.Nik,
            BookedBy = resultEmployee.FirstName + " " + resultEmployee.LastName,
            RoomName = resultRoom.Name,
            StartDate = resultBooking.StartDate,
            EndDate = resultBooking.EndDate,
            Status = resultBooking.Status,
            Remarks = resultBooking.Remarks
        };

        return toDto;
    }
}
