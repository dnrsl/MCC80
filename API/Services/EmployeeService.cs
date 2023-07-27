﻿using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    public IEnumerable<EmployeeDto> GetAll()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return Enumerable.Empty<EmployeeDto>();
        }

        var employeeDtos = new List<EmployeeDto>();
        foreach (var employee in employees)
        {
            employeeDtos.Add((EmployeeDto)employee);
        }
        return employeeDtos;
    }

    public EmployeeDto? GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return null;
        }

        return (EmployeeDto)employee;
    }

    public EmployeeDto? Create(NewEmployeeDto newEmployeeDto)
    {
        
        Employee newNik = newEmployeeDto;
        newNik.Nik = GenerateNikHandler.GenereateNewNik(_employeeRepository.GetLastNik());
        var employee = _employeeRepository.Create(newNik);
        if (employee is null)
        {
            return null;
        }
        return (EmployeeDto)employee;
    }

    public int Update (UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);
        if(employee is null)
        {
            return -1;
        }

        Employee toUpdate = updateEmployeeDto;
        toUpdate.Nik = employee.Nik;
        toUpdate.CreatedDate = employee.CreatedDate;
        var result = _employeeRepository.Update(toUpdate);

        return result ? 1 : 0;
    }
    
    public int Delete (Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return -1;
        }

        var result = _employeeRepository.Delete(employee);
        return result ? 1 : 0;
    }

    public IEnumerable<EmployeeDetailDto> GetAllEmployeeDetail()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return Enumerable.Empty<EmployeeDetailDto>();
        }

        var employeeDetailDto = new List<EmployeeDetailDto>();

        foreach (var emp in employees)
        {
            var education = _educationRepository.GetByGuid(emp.Guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            EmployeeDetailDto employeeDetail = new EmployeeDetailDto
            {
                EmployeeGuid = emp.Guid,
                Nik = emp.Nik,
                FullName = emp.FirstName + " " + emp.LastName,
                BirthDate = emp.BirthDate,
                Gender = emp.Gender,
                HiringDate = emp.HiringDate,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityName = university.Name
            };

            employeeDetailDto.Add(employeeDetail);
        }
        return employeeDetailDto;
    }

    public EmployeeDetailDto? GetEmployeeDetailByGuid (Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);

        if (employee is null)
        {
            return null;
        }

        var education = _educationRepository.GetByGuid(employee.Guid);
        var university = _universityRepository.GetByGuid(education.UniversityGuid);

        return new EmployeeDetailDto
        {
            EmployeeGuid = employee.Guid,
            Nik = employee.Nik,
            FullName = employee.FirstName + " " + employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.Gpa,
            UniversityName = university.Name
        };
    }
}
