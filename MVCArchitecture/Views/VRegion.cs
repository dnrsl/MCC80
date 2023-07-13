using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MVCArchitecture.Models;

namespace MVCArchitecture.Views;

public class VRegion
{
    public int Menu()
    {
        Console.WriteLine("=== Regions Table ===");
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

    public void GetAll (List<Region> regions)
    {
        foreach (var region in regions)
        {
            DisplayRegion(region);
        }
    }

    public Region Insert()
    {
        Console.WriteLine("Membuat Data Region Baru");
        Console.Write("Masukkan Nama Region : ");
        string name = Console.ReadLine();

        return new Region
        {
            Id = 0,
            Name = name,
        };
    }

    public Region Update()
    {
        Console.WriteLine("Update Region Berdasarkan ID");
        Console.Write("Masukkan ID :");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nama Region Baru: ");
        string name = Console.ReadLine();
        return new Region
        {
            Id = id,
            Name = name,
        };

    }

    public Region Delete()
    {
        Console.WriteLine("Hapus Region Berdasarkan ID");
        Console.Write("Masukkan ID :");
        int id = int.Parse(Console.ReadLine());
        return new Region
        {
            Id = id
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Region ID: ");
        int id = int.Parse(Console.ReadLine());
        return id;
    }

    public void DisplayRegion( Region region)
    {
        Console.WriteLine("Id: " + region.Id);
        Console.WriteLine("Name: " + region.Name);
        Console.WriteLine("==========================");
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
}
