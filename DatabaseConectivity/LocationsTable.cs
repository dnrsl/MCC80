using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class LocationsTable
    {
        private static string _connectionString =
            "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("=== Locations Table ===");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.Write("Input: ");
        }

        public static void LocationsMain()
        {
            Console.Clear();
            int number, id, idCountry;
            string address, postalCode, city, province;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Lokasi Baru");
                            Console.Write("Masukkan ID Lokasi : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Alamat : ");
                            address = Console.ReadLine();
                            Console.Write("Masukkan Kode Pos : ");
                            postalCode = (Console.ReadLine());
                            Console.Write("Masukkan Kota : ");
                            city = (Console.ReadLine());
                            Console.Write("Masukkan Provinsi : ");
                            province = (Console.ReadLine());
                            Console.Write("Masukkan ID Country : ");
                            idCountry = int.Parse(Console.ReadLine());
                            InsertLocations(id, address, postalCode, city, province, idCountry);
                        }

                        catch
                        {
                            Console.WriteLine("Error connecting to database.");
                        }
                        Console.WriteLine();
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Update Lokasi Berdasarkan ID");
                            Console.Write("Masukkan ID Lokasi : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Alamat : ");
                            address = Console.ReadLine();
                            Console.Write("Masukkan Kode Pos : ");
                            postalCode = (Console.ReadLine());
                            Console.Write("Masukkan Kota : ");
                            city = (Console.ReadLine());
                            Console.Write("Masukkan Provinsi : ");
                            province = (Console.ReadLine());
                            Console.Write("Masukkan ID Country : ");
                            idCountry = int.Parse(Console.ReadLine());
                            InsertLocations(id, address, postalCode, city, province, idCountry);
                        }

                        catch
                        {
                            Console.WriteLine("Error connecting to database.");
                        }
                        Console.WriteLine();
                        break;

                    case 3:
                        try
                        {
                            Console.WriteLine("Hapus Lokasi Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            id = int.Parse(Console.ReadLine());
                            DeleteLocations(id);
                        }

                        catch
                        {
                            Console.WriteLine("Error connecting to database.");
                        }
                        Console.WriteLine();
                        break;

                    case 4:
                        try
                        {
                            Console.WriteLine("Menampilkan Lokasi Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            id = int.Parse(Console.ReadLine());
                            GetLocationsByID(id);
                        }

                        catch
                        {
                            Console.WriteLine("Error connecting to database.");
                        }
                        Console.WriteLine();
                        break;

                    case 5:
                        try
                        {
                            GetLocations();
                            Console.WriteLine();
                        }

                        catch
                        {
                            Console.WriteLine("Error connecting to database.");
                        }
                        Console.WriteLine();
                        break;

                    case 6:
                        Console.WriteLine("Terima Kasih");
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        Console.WriteLine();
                        break;
                }

            } while (number != 6);

        }
        //GET ALL Locations
        public static void GetLocations()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM locations;";

            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Street Address: " + reader.GetString(1));
                        Console.WriteLine("Postal Code: " + reader.GetString(2));
                        Console.WriteLine("City: " + reader.GetString(3));
                        Console.WriteLine("State Province: " + reader.GetString(4));
                        Console.WriteLine("Country ID: " + reader.GetString(5));
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No Locations found.");
                }

                reader.Close();
                _connection.Close();
            }

            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //INSERT Locations
        public static void InsertLocations(int id, string street_address, string postal_code,string city, string state_province, int country_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT locations VALUES (@id, @address, @postal, @city, @province, @countryid)";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "@id";
                pID.SqlDbType = SqlDbType.Int;
                pID.Value = id;
                sqlCommand.Parameters.Add(pID);

                SqlParameter pAddress = new SqlParameter();
                pAddress.ParameterName = "@address";
                pAddress.SqlDbType = SqlDbType.VarChar;
                pAddress.Value = street_address;
                sqlCommand.Parameters.Add(pAddress);

                SqlParameter pPostal = new SqlParameter();
                pPostal.ParameterName = "@postal";
                pPostal.SqlDbType = SqlDbType.VarChar;
                pPostal.Value = postal_code;
                sqlCommand.Parameters.Add(pPostal);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = SqlDbType.VarChar;
                pCity.Value = city;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pProvince = new SqlParameter();
                pProvince.ParameterName = "@province";
                pProvince.SqlDbType = SqlDbType.VarChar;
                pProvince.Value = state_province;
                sqlCommand.Parameters.Add(pProvince);

                SqlParameter pCountryID = new SqlParameter();
                pCountryID.ParameterName = "@countryid";
                pCountryID.SqlDbType = SqlDbType.Int;
                pCountryID.Value = country_id;
                sqlCommand.Parameters.Add(pCountryID);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Insert success.");
                }
                else
                {
                    Console.WriteLine("Insert failed.");
                }

                transaction.Commit();
                _connection.Close();
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Error connecting to database.");
            }
        }

        //UPDATE Locations
        public static void UpdateLocations(int id, string street_address, string postal_code, string city, string state_province, int country_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE locations SET street_address = @address, postal_code = @postal, city = @city, state_province = @province, country_id = @countryid WHERE id = @id;";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {

                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "@id";
                pID.SqlDbType = SqlDbType.Int;
                pID.Value = id;
                sqlCommand.Parameters.Add(pID);

                SqlParameter pAddress = new SqlParameter();
                pAddress.ParameterName = "@address";
                pAddress.SqlDbType = SqlDbType.VarChar;
                pAddress.Value = street_address;
                sqlCommand.Parameters.Add(pAddress);

                SqlParameter pPostal = new SqlParameter();
                pPostal.ParameterName = "@postal";
                pPostal.SqlDbType = SqlDbType.VarChar;
                pPostal.Value = postal_code;
                sqlCommand.Parameters.Add(pPostal);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = SqlDbType.VarChar;
                pCity.Value = city;
                sqlCommand.Parameters.Add(pCity);

                SqlParameter pProvince = new SqlParameter();
                pProvince.ParameterName = "@province";
                pProvince.SqlDbType = SqlDbType.VarChar;
                pProvince.Value = state_province;
                sqlCommand.Parameters.Add(pProvince);

                SqlParameter pCountryID = new SqlParameter();
                pCountryID.ParameterName = "@countryid";
                pCountryID.SqlDbType = SqlDbType.Int;
                pCountryID.Value = country_id;
                sqlCommand.Parameters.Add(pCountryID);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Update success.");
                }
                else
                {
                    Console.WriteLine("Update failed.");
                }

                transaction.Commit();
                _connection.Close();
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Error connecting to database.");
            }
        }


        //DELETE Locations
        public static void DeleteLocations(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM locations WHERE id = @id";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "@id";
                pID.SqlDbType = SqlDbType.Int;
                pID.Value = id;
                sqlCommand.Parameters.Add(pID);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Delete success.");
                }
                else
                {
                    Console.WriteLine("Delete failed.");
                }

                transaction.Commit();
                _connection.Close();
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("Error connecting to database.");
            }
        }


        //GET BY ID Locations
        public static void GetLocationsByID(int id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT street_address, postal_code, city, state_province, country_id FROM locations WHERE id = @id;";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "@id";
                pID.SqlDbType = SqlDbType.Int;
                pID.Value = id;
                sqlCommand.Parameters.Add(pID);

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Alamat: " + reader.GetString(0));
                        Console.WriteLine("Kode Pos: " + reader.GetString(1));
                        Console.WriteLine("Kota: " + reader.GetString(2));
                        Console.WriteLine("Provinsi: " + reader.GetString(3));
                        Console.WriteLine("Country ID: " + reader.GetInt32(4));
                    }
                }
                else
                {
                    Console.WriteLine("No locations found.");
                }

                reader.Close();
                _connection.Close();
            }

            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }
    }
}
