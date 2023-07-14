using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;

namespace MVCArchitecture.Controllers;

public class LinqController
{
    private Employee _employee;
    private Department _department;
    private Location _location;
    private Country _country;
    private Region _region;

    public LinqController(Employee employee, Department department, Location location, Country country, Region region)
    {
        _employee = employee;
        _department = department;
        _location = location;
        _country = country;
        _region = region;
    }

    public void FullData()
    {
        var getEmployee = _employee.GetAll();
        var getDepartment = _department.GetAll();
        var getLocation = _location.GetAll();
        var getCountry = _country.GetAll();
        var getRegion = _region.GetAll();

        var fullData = (from e in getEmployee 
                        join d in getDepartment on e.DepartmentId equals d.Id 
                        join l in getLocation on d.LocationId equals l.Id
                        join c in getCountry on l.CountryId equals c.Id
                        join r in getRegion on c.RegionId equals r.Id
                        select new
                        {
                            Id = e.Id,
                            FName = e.FName,
                            LName = e.LName,
                            Email = e.Email,
                            Phone = e.Phone,
                            Salary = e.Salary,
                            DName = d.Name,
                            Address = l.Address,
                            CName = c.Name,
                            RName = r.Name
                        }).ToList();
        foreach (var data in fullData)
        {
            Console.WriteLine($"Id : {data.Id}");
            Console.WriteLine($"Full Name : {data.FName} {data.LName}");
            Console.WriteLine($"Email : {data.Email}");
            Console.WriteLine($"Phone Number : {data.Phone}");
            Console.WriteLine($"Salary : {data.Salary}");
            Console.WriteLine($"Department : {data.DName}");
            Console.WriteLine($"Address : {data.Address}");
            Console.WriteLine($"Country : {data.CName}");
            Console.WriteLine($"Region : {data.RName}");
            Console.WriteLine("------------------------------------");

        }
    }
}
