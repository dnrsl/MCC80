using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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
        var employee = _employeeRepository.Create(newEmployeeDto);
        if (employee is null)
        {
            return null;
        }

        return (EmployeeDto)employee;
    }

    public int Update (EmployeeDto employeeDto)
    {
        var employee = _employeeRepository.GetByGuid(employeeDto.Guid);
        if(employee is null)
        {
            return -1;
        }

        Employee toUpdate = employeeDto;
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
}
