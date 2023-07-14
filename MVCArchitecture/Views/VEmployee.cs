using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MVCArchitecture.Models;


namespace MVCArchitecture.Views;

public class VEmployee
{
    public int Menu()
    {
        Console.WriteLine("=== Employee Table ===");
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Update");
        Console.WriteLine("3. Delete");
        Console.WriteLine("4. Get By ID");
        Console.WriteLine("5. Get All");
        Console.WriteLine("6. Back");
        Console.Write("Input: ");

        int input = Int32.Parse(Console.ReadLine());
        return input;
    }

    public void GetAll(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            DisplayEmployee(employee);
        }
    }

    public Employee Insert()
    {
        Console.WriteLine("Membuat Data Karyawan Baru");
        Console.Write("Employee ID : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("First Name : ");
        string fName = Console.ReadLine();
        Console.Write("Last Name : ");
        string lName = Console.ReadLine();
        Console.Write("Email : ");
        string email = Console.ReadLine();
        Console.Write("Phone Number : ");
        string pNumber = Console.ReadLine();
        Console.Write("Hire Date : ");
        DateTime hire = DateTime.Parse(Console.ReadLine());
        Console.Write("Salary : ");
        int salary = int.Parse(Console.ReadLine());
        Console.Write("Comission pct : ");
        decimal comission = decimal.Parse(Console.ReadLine());
        Console.Write("Manager ID : ");
        int manID = int.Parse(Console.ReadLine());
        Console.Write("Job ID : ");
        string jobID = Console.ReadLine();
        Console.Write("Department ID : ");
        int depID = int.Parse(Console.ReadLine());

        return new Employee
        {
            Id = id,
            FName = fName,
            LName = lName,
            Email = email,
            Phone = pNumber,
            Hire = hire,
            Salary = salary,
            Comission = comission,
            ManagerId = manID,
            JobId = jobID,
            DepartmentId = depID

        };
    }

    public Employee Update()
    {
        Console.WriteLine("Update Data Karyawan");
        Console.Write("Employee ID : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("First Name : ");
        string fName = Console.ReadLine();
        Console.Write("Last Name : ");
        string lName = Console.ReadLine();
        Console.Write("Email : ");
        string email = Console.ReadLine();
        Console.Write("Phone Number : ");
        string pNumber = Console.ReadLine();
        Console.Write("Hire Date : ");
        DateTime hire = DateTime.Parse(Console.ReadLine());
        Console.Write("Salary : ");
        int salary = int.Parse(Console.ReadLine());
        Console.Write("Comission pct : ");
        decimal comission = decimal.Parse(Console.ReadLine());
        Console.Write("Manager ID : ");
        int manID = int.Parse(Console.ReadLine());
        Console.Write("Job ID : ");
        string jobID = Console.ReadLine();
        Console.Write("Department ID : ");
        int depID = int.Parse(Console.ReadLine());

        return new Employee
        {
            Id = id,
            FName = fName,
            LName = lName,
            Email = email,
            Phone = pNumber,
            Hire = hire,
            Salary = salary,
            Comission = comission,
            ManagerId = manID,
            JobId = jobID,
            DepartmentId = depID

        };
    }

    public Employee Delete()
    {
        Console.WriteLine("Hapus Employee Berdasarkan ID");
        Console.Write("Masukkan ID :");
        int id = int.Parse(Console.ReadLine());
        return new Employee
        {
            Id = id,
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Employee ID: ");
        int id = int.Parse(Console.ReadLine());
        return id;
    }

    public void DataEmpty()
    {
        Console.WriteLine("Data Not Found!");
    }

    public void Success()
    {
        Console.WriteLine("Success!");
    }

    public void Failure()
    {
        Console.WriteLine("Fail, Id not found!");
    }

    public void Error()
    {
        Console.WriteLine("Error retrieving from database!");
    }

    public void DisplayEmployee(Employee employee)
    {
        Console.WriteLine("Id : " +  employee.Id);
        Console.WriteLine("First Name : " + employee.FName);
        Console.WriteLine("Last Name : " + employee.LName);
        Console.WriteLine("Email : " + employee.Email);
        Console.WriteLine("Phone Number : " + employee.Phone);
        Console.WriteLine("Hire Date : " + employee.Hire);
        Console.WriteLine("Salary : " + employee.Salary);
        Console.WriteLine("Comission pct : " + employee.Comission);
        Console.WriteLine("Manager ID : " + employee.ManagerId);
        Console.WriteLine("Job Id : " + employee.JobId);
        Console.WriteLine("Department Id : " + employee.DepartmentId);
    }

}
