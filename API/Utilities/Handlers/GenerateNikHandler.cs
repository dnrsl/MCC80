using API.Contracts;

namespace API.Utilities.Handlers;

public class GenerateNikHandler
{
    //private readonly IEmployeeRepository _employeeRepository;
    private int startNik = 0;

    //public GenerateNikHandler(IEmployeeRepository employeeRepository)
    //{
    //    _employeeRepository = employeeRepository;
    //    startNik = _employeeRepository.GetLastNik();
    //}

    public static string GenereateNewNik(string nik)
    {
        if (nik == null)
        {
            return "11111";
        }
        var newNikGenerate = Convert.ToInt32(nik)+1;
        return newNikGenerate.ToString("D6");
    }
}
