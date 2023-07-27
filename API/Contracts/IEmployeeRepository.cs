using API.DTOs.Accounts;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    bool IsNotExist(string value);

    string GetLastNik();

    Employee? GetByEmail(string email);

    Guid GetLastEmployeeGuid();
    //bool SameOrIsExist(string value2);
}
