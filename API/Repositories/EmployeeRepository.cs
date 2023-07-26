using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
        return employees;
    }

    public Employee? GetByEmail(string email)
    {
        return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
    }

    //public bool SameOrIsExist(string value2)
    //{
    //    var employees = _context.Set<Employee>()
    //                   .SingleOrDefault(e => e.Email.Contains(value2) || e.PhoneNumber.Contains(value2)) is null;
    //    if (employees == null)
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}
