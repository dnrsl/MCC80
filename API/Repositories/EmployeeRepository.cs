using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingDbContext context) : base(context)
    {
    }

    public bool IsNotExist(string value)
    {
        return _context.Set<Employee>()
                       .SingleOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
    }

    public string GetLastNik()
    {
        var employees = _context.Set<Employee>()
                       .ToList().LastOrDefault().Nik;
        //var lastNik = employees.Any() ? employees.Max(e => int.Parse(e.Nik)) : 0;
        return employees;
    }
}
