﻿using API.Contracts;
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

    //public bool IsNotExist(string value)
    //{
    //    return _context.Set<Employee>()
    //                   .SingleOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
    //}

    public bool IsNotExist(string value)
    {
        return _context.Set<Employee>()
                       .FirstOrDefault(e => e.Email == value || e.PhoneNumber == value) is null;
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

    public Guid GetLastEmployeeGuid()
    {
        return _context.Set<Employee>()
                       .ToList()
                       .LastOrDefault().Guid;
    }

    public bool IsDataUnique(string value2)
    {
        var existingEmployee = _context.Set<Employee>().SingleOrDefault(e => e.Email == value2  || e.PhoneNumber == value2);

        if (existingEmployee is null)
        {
            return true;
        }

        else
        {
            bool isSameAsExistingData = existingEmployee.Email == value2 || existingEmployee.PhoneNumber == value2;
            return isSameAsExistingData;
        }
    }
}
