﻿using static System.Net.Mime.MediaTypeNames;
using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using MVCArchitecture.Models;
using MVCArchitecture.Views;
using MVCArchitecture.Controllers;

namespace MVCArchitecture;
public class Progam
{
    public static void Main()
    {
        MainMenu();
    }

    private static void MainMenu()
    {
        bool repeat = true;
        do
        {
            Console.WriteLine("Created by : Muhdani Rosuli");
            Console.WriteLine("== Menu Database HR ==");
            Console.WriteLine("1. Regions");
            Console.WriteLine("2. Countries");
            Console.WriteLine("3. Locations");
            Console.WriteLine("4. Departments");
            Console.WriteLine("5. Jobs");
            Console.WriteLine("6. Employees");
            Console.WriteLine("7. Histories");
            Console.WriteLine("8. Exit");
            Console.Write("Input: ");

            try
            {
                int pilihMenu = Int32.Parse(Console.ReadLine());
                switch (pilihMenu)
                {
                    case 1:
                        RegionMenu();
                        Console.WriteLine();
                        break;

                    case 2:
                        CountryMenu();
                        Console.WriteLine();
                        break;

                    case 3:
                        LocationMenu();
                        Console.WriteLine();
                        break;

                    case 4:
                        DepartmentMenu();
                        Console.WriteLine();
                        break;

                    case 5:
                        Console.WriteLine();
                        break;

                    case 6:
                        Console.WriteLine();
                        break;

                    case 7:
                        Console.WriteLine();
                        break;

                    case 8:
                        repeat = false;
                        Console.WriteLine("Terima Kasih");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        Console.WriteLine();
                        break;
                }
            }

            catch
            {
                Console.WriteLine("Input Hanya diantara 1-7!");
            }
        } while (repeat);

    }

    private static void RegionMenu()
    {
        Region region = new Region();
        VRegion vRegion = new VRegion();
        RegionController regionController = new RegionController(region, vRegion);

        bool isTrue = true;
        do
        {
            int pilihMenu = vRegion.Menu();

            switch (pilihMenu)
            {
                case 1:
                    regionController.Insert();
                    PressAnyKey();
                    break;

                case 2:
                    regionController.Update();
                    PressAnyKey();
                    break;

                case 3:
                    regionController.Delete();
                    PressAnyKey();
                    break;

                case 4:
                    regionController.GetByID();
                    PressAnyKey();
                    break;

                case 5:
                    regionController.GetAll();
                    PressAnyKey();
                    break;

                case 6:
                    isTrue = false;
                    break;

                default:
                    InvalidInput();
                    break;
            }

        } while (isTrue);
    }

    private static void CountryMenu()
    {
        Country country = new Country();
        VCountry vCountry = new VCountry();
        CountryController countryController = new CountryController(country, vCountry);

        bool isTrue = true;
        do
        {
            int pilihMenu = vCountry.Menu();

            switch (pilihMenu)
            {
                case 1:
                    countryController.Insert();
                    PressAnyKey();
                    break;

                case 2:
                    countryController.Update();
                    PressAnyKey();
                    break;

                case 3:
                    countryController.Delete();
                    PressAnyKey();
                    break;

                case 4:
                    countryController.GetByID();
                    PressAnyKey();
                    break;

                case 5:
                    countryController.GetAll();
                    PressAnyKey();
                    break;

                case 6:
                    isTrue = false;
                    break;

                default:
                    InvalidInput();
                    break;
            }

        } while (isTrue);
    }
    private static void LocationMenu()
    {
        Location location = new Location();
        VLocation vLocation = new VLocation();
        LocationController locationController = new LocationController(location, vLocation);

        bool isTrue = true;
        do
        {
            int pilihMenu = vLocation.Menu();

            switch (pilihMenu)
            {
                case 1:
                    locationController.Insert();
                    PressAnyKey();
                    break;

                case 2:
                    locationController.Update();
                    PressAnyKey();
                    break;

                case 3:
                    locationController.Delete();
                    PressAnyKey();
                    break;

                case 4:
                    locationController.GetByID();
                    PressAnyKey();
                    break;

                case 5:
                    locationController.GetAll();
                    PressAnyKey();
                    break;

                case 6:
                    isTrue = false;
                    break;

                default:
                    InvalidInput();
                    break;
            }

        } while (isTrue);
    }

    private static void DepartmentMenu()
    {
        Department department = new Department();
        VDepartment vDepartment = new VDepartment();
        DepartmentController departmentController = new DepartmentController(department, vDepartment);

        bool isTrue = true;
        do
        {
            int pilihMenu = vDepartment.Menu();

            switch (pilihMenu)
            {
                case 1:
                    departmentController.Insert();
                    PressAnyKey();
                    break;

                case 2:
                    departmentController.Update();
                    PressAnyKey();
                    break;

                case 3:
                    departmentController.Delete();
                    PressAnyKey();
                    break;

                case 4:
                    departmentController.GetByID();
                    PressAnyKey();
                    break;

                case 5:
                    departmentController.GetAll();
                    PressAnyKey();
                    break;

                case 6:
                    isTrue = false;
                    break;

                default:
                    InvalidInput();
                    break;
            }

        } while (isTrue);
    }

    private static void InvalidInput()
    {
        Console.WriteLine("Your input is not valid!");
    }

    private static void PressAnyKey()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}