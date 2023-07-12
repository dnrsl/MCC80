using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class RegionsTable
    {
        private static string _connectionString =
        "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Exit");
            Console.Write("Input: ");
        }

        public static void RegionsMain()
        {
            Console.Clear();
            int number, idRegion;
            string nRegion;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Region Baru");
                            Console.Write("Masukkan Nama Region : ");
                            nRegion = Console.ReadLine();
                            InsertRegions(nRegion);
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
                            Console.WriteLine("Update Region Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            idRegion = int.Parse(Console.ReadLine());
                            Console.Write("Nama Region Baru: ");
                            nRegion = Console.ReadLine();
                            UpdateRegions(idRegion, nRegion);
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
                            Console.WriteLine("Hapus Region Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            idRegion = int.Parse(Console.ReadLine());
                            DeleteRegions(idRegion);
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
                            Console.WriteLine("Menampilkan Region Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            idRegion = int.Parse(Console.ReadLine());
                            GetRegionsByID(idRegion);
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
                            GetRegions();
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
        //GET ALL REGION
        public static void GetRegions()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM regions";

            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("Name: " + reader.GetString(1));
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No regions found.");
                }

                reader.Close();
                _connection.Close();
            }

            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //INSERT REGION
        public static void InsertRegions(string name)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT INTO regions VALUES (@name)";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = SqlDbType.VarChar;
                pName.Value = name;
                sqlCommand.Parameters.Add(pName);

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

        //UPDATE REGION
        public static void UpdateRegions(int id, string name)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE regions SET name = @name WHERE id = @id;";

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

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = SqlDbType.VarChar;
                pName.Value = name;
                sqlCommand.Parameters.Add(pName);

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


        //DELETE REGION
        public static void DeleteRegions(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM regions WHERE id = @id";

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


        //GET BY ID REGION
        public static void GetRegionsByID(int id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT name FROM regions WHERE id = @id;";

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
                        Console.WriteLine(reader.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("No regions found.");
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
