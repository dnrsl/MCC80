using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    bool IsNotExist(string value);

    string GetLastNik();

    Employee? GetByEmail(string email);

    //bool SameOrIsExist(string value2);
}
