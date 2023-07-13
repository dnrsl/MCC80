using MVCArchitecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Views;

public class VJob
{
    public int Menu()
    {
        Console.WriteLine("=== Job Table ===");
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

    public void GetAll(List<Job> jobs)
    {
        foreach(var  job in jobs)
        {
            DisplayJob(job);
        }
    }

    public Job Insert()
    {
        Console.WriteLine("Membuat Data Pekerjaan Baru");
        Console.Write("Masukkan ID Pekerjaan : ");
        string id = (Console.ReadLine());
        Console.Write("Masukkan Nama Pekerjaan : ");
        string title = Console.ReadLine();
        Console.Write("Masukkan Gaji Minimal : ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Gaji Maksimal : ");
        int max = int.Parse(Console.ReadLine());

        return new Job
        {
            Id = id,
            Title = title,
            Min = min,
            Max = max
        };
    }

    public Job Update()
    {
        Console.WriteLine("Update Data Pekerjaan Baru");
        Console.Write("Masukkan ID Pekerjaan : ");
        string id = (Console.ReadLine());
        Console.Write("Masukkan Nama Pekerjaan : ");
        string title = Console.ReadLine();
        Console.Write("Masukkan Gaji Minimal : ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Gaji Maksimal : ");
        int max = int.Parse(Console.ReadLine());

        return new Job
        {
            Id = id,
            Title = title,
            Min = min,
            Max = max
        };
    }

    public Job Delete()
    {
        Console.WriteLine("Hapus Job Berdasarkan ID");
        Console.Write("Masukkan ID :");
        string id = (Console.ReadLine());
        return new Job
        {
            Id = id,
        };
    }

    public string GetByID()
    {
        Console.WriteLine("Enter Job ID: ");
        string id = (Console.ReadLine());
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

    public void DisplayJob(Job job)
    {
        Console.WriteLine("Id : " + job.Id);
        Console.WriteLine("Title : " + job.Title);
        Console.WriteLine("Minimal Salaray : " + job.Min);
        Console.WriteLine("Maksimal Salary : " + job.Max);
    }
}
