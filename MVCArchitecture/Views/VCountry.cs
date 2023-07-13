using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;

namespace MVCArchitecture.Views;

public class VCountry
{
    public int Menu()
    {
        Console.WriteLine("=== Countries Table ===");
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

    public void GetAll(List<Country> countries)
    {
        foreach(var country in countries)
        {
            DisplayCountry(country);
        }
    }

    public Country Insert()
    {
        Console.WriteLine("Membuat Data Country Baru");
        Console.Write("Masukkan ID Country : ");
        string id = Console.ReadLine();
        Console.Write("Masukkan Nama Country : ");
        string name = Console.ReadLine();
        Console.Write("Masukkan ID Region : ");
        int regionid = int.Parse(Console.ReadLine());

        return new Country
        {
            Id = id,
            Name = name,
            RegionId = regionid
        };

    }

    public Country Update()
    {
        Console.WriteLine("Update Country Berdasarkan ID");
        Console.Write("Masukkan ID Country : ");
        string id = Console.ReadLine();
        Console.Write("Masukkan Nama Country : ");
        string name = Console.ReadLine();
        Console.Write("Masukkan ID Region : ");
        int regionid = int.Parse(Console.ReadLine());

        return new Country
        {
            Id = id,
            Name = name,
            RegionId = regionid
        };
    }

    public Country Delete()
    {
        Console.WriteLine("Hapus Country Berdasarkan ID");
        Console.Write("Masukkan ID :");
        string id = Console.ReadLine();
        return new Country
        {
            Id = id,
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Country ID: ");
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

    public void DisplayCountry(Country country)
    {
        Console.WriteLine("Id: " + country.Id);
        Console.WriteLine("Name: " + country.Name);
        Console.WriteLine("Region Id: " + country.RegionId);
    }
}
