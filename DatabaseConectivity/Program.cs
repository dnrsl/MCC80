using static System.Net.Mime.MediaTypeNames;
using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseConectivity;

public class Program
{
    //private static string _connectionString =
    //    "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

    //private static SqlConnection _connection;

    static void Menu()
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
    }

    public static void Main()
    {
        int number;
        
        do
        {
            
            Menu();
            number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    RegionsTable.RegionsMain();
                    Console.WriteLine("Data user berhasil dibuat");
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine();
                    break;

                case 3:
                    Console.WriteLine();
                    break;

                case 4:
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
                    Console.WriteLine("Terima Kasih");
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    Console.WriteLine();
                    break;
            }
        } while (number != 8);
    }

}