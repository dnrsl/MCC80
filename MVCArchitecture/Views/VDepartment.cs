using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MVCArchitecture.Models;


namespace MVCArchitecture.Views;

public class VDepartment
{
    public int Menu()
    {
        Console.WriteLine("=== Department Table ===");
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

    public void GetAll(List<Department> departments)
    {
        foreach(var  department in departments)
        {
            DisplayDepartment(department);
        }
    }

    public Department Insert()
    {
        Console.WriteLine("Membuat Data Department Baru");
        Console.Write("Masukkan ID Department : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Nama Department : ");
        string name = Console.ReadLine();
        Console.Write("Masukkan ID Lokasi : ");
        int locationid = int.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Manager : ");
        int managerid = int.Parse(Console.ReadLine());

        return new Department
        {
            Id = id,
            Name = name,
            LocationId = locationid,
            ManagerId = managerid
        };
    }

    public Department Update()
    {
        Console.WriteLine("Update Data Department");
        Console.Write("Masukkan ID Department : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Nama Department : ");
        string name = Console.ReadLine();
        Console.Write("Masukkan ID Lokasi : ");
        int locationid = int.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Manager : ");
        int managerid = int.Parse(Console.ReadLine());

        return new Department
        {
            Id = id,
            Name = name,
            LocationId = locationid,
            ManagerId = managerid
        };
    }

    public Department Delete()
    {
        Console.WriteLine("Hapus Department Berdasarkan ID");
        Console.Write("Masukkan ID :");
        int id = int.Parse(Console.ReadLine());
        return new Department
        {
            Id = id,
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Department ID: ");
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

    public void DisplayDepartment(Department department)
    {
        Console.WriteLine("Id : " + department.Id);
        Console.WriteLine("Name : " + department.Name);
        Console.WriteLine("Location Id : " + department.LocationId);
        if (department.ManagerId != null)
        {
            Console.WriteLine("Manager Id: " + department.ManagerId);
        }
        else
        {
            Console.WriteLine("Manager Id: belum ada");
        }
    }
}
