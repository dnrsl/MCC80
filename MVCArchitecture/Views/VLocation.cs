using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MVCArchitecture.Models;


namespace MVCArchitecture.Views;

public class VLocation
{
    public int Menu()
    {
        Console.WriteLine("=== Location Table ===");
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

    public void GetAll(List<Location> locations)
    {
        foreach(var location in  locations)
        {
            DisplayLocation(location);
        }
    }

    public Location Insert()
    {

        Console.WriteLine("Membuat Data Lokasi Baru");
        Console.Write("Masukkan ID Lokasi : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Alamat : ");
        string address = Console.ReadLine();
        Console.Write("Masukkan Kode Pos : ");
        string postal = (Console.ReadLine());
        Console.Write("Masukkan Kota : ");
        string city = (Console.ReadLine());
        Console.Write("Masukkan Provinsi : ");
        string state = (Console.ReadLine());
        Console.Write("Masukkan ID Country : ");
        string countryId = (Console.ReadLine());

        return new Location
        {
            Id = id,
            Address = address,
            Postal = postal,
            City = city,
            State = state,
            CountryId = countryId
        };
    }

    public Location Update()
    {
        Console.WriteLine("Update Data Lokasi");
        Console.Write("Masukkan ID Lokasi : ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Alamat : ");
        string address = Console.ReadLine();
        Console.Write("Masukkan Kode Pos : ");
        string postal = (Console.ReadLine());
        Console.Write("Masukkan Kota : ");
        string city = (Console.ReadLine());
        Console.Write("Masukkan Provinsi : ");
        string state = (Console.ReadLine());
        Console.Write("Masukkan ID Country : ");
        string countryId = (Console.ReadLine());

        return new Location
        {
            Id = id,
            Address = address,
            Postal = postal,
            City = city,
            State = state,
            CountryId = countryId
        };
    }

    public Location Delete()
    {
        Console.WriteLine("Hapus Lokasi Berdasarkan ID");
        Console.Write("Masukkan ID :");
        int id = int.Parse(Console.ReadLine());
        return new Location
        {
            Id = id,
        };
    }

    public int GetByID()
    {
        Console.WriteLine("Enter Lokasi ID: ");
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

    public void DisplayLocation(Location location)
    {
        Console.WriteLine("Id :" + location.Id);
        Console.WriteLine("Street Address :" + location.Address);
        Console.WriteLine("Postal Code :" + location.Postal);
        Console.WriteLine("City :" + location.City);
        Console.WriteLine("State Province :" + location.State);
        Console.WriteLine("Country Id :" + location.CountryId);
    }
}
