using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;


namespace MVCArchitecture.Views;

public class VHistory
{
    public int Menu()
    {
        Console.WriteLine("=== History Table ===");
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

    public void GetAll(List<History> histories)
    {
        foreach (var history in  histories)
        {
            DisplayHistory(history);
        }
    }

    public History Insert()
    {
        Console.WriteLine("Membuat Data Riwayat Baru");
        Console.Write("Tanggal Mulai : ");
        DateTime start = DateTime.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Karyawan : ");
        int empID = int.Parse(Console.ReadLine());
        Console.Write("Tanggal Keluar : ");
        DateTime end = DateTime.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Department : ");
        int depID = int.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Pekerjaan : ");
        string jobID = (Console.ReadLine());

        return new History
        {
            StartDate = start,
            EmployeeId = empID,
            EndDate = end,
            DepartmentId = depID,
            JobId = jobID
        };
    }

    public History Update()
    {
        Console.WriteLine("Uodate Data Riwayat");
        Console.Write("Masukkan ID Karyawan : ");
        int empID = int.Parse(Console.ReadLine());
        Console.Write("Tanggal Mulai : ");
        DateTime start = DateTime.Parse(Console.ReadLine());
        Console.Write("Tanggal Keluar : ");
        DateTime end = DateTime.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Department : ");
        int depID = int.Parse(Console.ReadLine());
        Console.Write("Masukkan ID Pekerjaan : ");
        string jobID = (Console.ReadLine());

        return new History
        {
            StartDate = start,
            EmployeeId = empID,
            EndDate = end,
            DepartmentId = depID,
            JobId = jobID
        };
    }

    public History Delete()
    {
        Console.WriteLine("Hapus History Berdasarkan ID Karyawan");
        Console.Write("Masukkan ID :");
        int empID = int.Parse(Console.ReadLine());
        return new History
        {
            EmployeeId = empID
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Karyawan ID : ");
        int empID = int.Parse(Console.ReadLine());
        return empID;
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

    public void DisplayHistory(History history)
    {
        Console.WriteLine("Start Date : " + history.StartDate);
        Console.WriteLine("Employee ID : " + history.EmployeeId);
        Console.WriteLine("End Date : " + history.EndDate);
        Console.WriteLine("Department ID : " + history.DepartmentId);
        Console.WriteLine("Job ID : " + history.JobId);
    }
}
