
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class JobsTable
    {
        private static string _connectionString = "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("=== Jobs Table ===");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.Write("Input: ");
        }

        public static void JobsMain()
        {
            Console.Clear();
            int number, id, min, max;
            string title;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Pekerjaan Baru");
                            Console.Write("Masukkan ID Pekerjaan : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Nama Pekerjaan : ");
                            title = Console.ReadLine();
                            Console.Write("Masukkan Gaji Minimal : ");
                            min = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Gaji Maksimal : ");
                            max = int.Parse(Console.ReadLine());
                            InsertJobs(id, title, min, max);
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
                            Console.WriteLine("Mengubah Data Department");
                            Console.Write("Masukkan ID Pekerjaan : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Nama Pekerjaan (Baru) : ");
                            title = Console.ReadLine();
                            Console.Write("Masukkan Gaji Minimal : ");
                            min = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Gaji Maksimal : ");
                            max = int.Parse(Console.ReadLine());
                            UpdateJobs(id, title, min, max);
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
                            Console.WriteLine("Hapus Pekerjaan Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            id = int.Parse(Console.ReadLine());
                            DeleteJobs(id);
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
                            GetJobsByID(id);
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
                            GetJobs();
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
        //GET ALL Jobs
        public static void GetJobs()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM jobs;";

            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetString(0));
                        Console.WriteLine("Title: " + reader.GetString(1));
                        Console.WriteLine("Gaji Minimal: " + reader.GetInt32(2));
                        Console.WriteLine("Gaji Maksimal: " + reader.GetInt32(3));
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No Jobs found.");
                }

                reader.Close();
                _connection.Close();
            }

            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //INSERT Jobs
        public static void InsertJobs(int id, string title, int min_salary, int max_salary)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT jobs VALUES (@id, @title, @min, @max)";

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

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = SqlDbType.VarChar;
                pTitle.Value = title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMin = new SqlParameter();
                pMin.ParameterName = "@min";
                pMin.SqlDbType = SqlDbType.Int;
                pMin.Value = min_salary;
                sqlCommand.Parameters.Add(pMin);

                SqlParameter pMax = new SqlParameter();
                pMax.ParameterName = "@max";
                pMax.SqlDbType = SqlDbType.Int;
                pMax.Value = max_salary;
                sqlCommand.Parameters.Add(pMax);

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

        //UPDATE Jobs
        public static void UpdateJobs(int id, string title, int min_salary, int max_salary)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE jobs SET id = @id, title = @title, min_salary = @min, max_salary = @max WHERE id = @id;";

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

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = SqlDbType.VarChar;
                pTitle.Value = title;
                sqlCommand.Parameters.Add(pTitle);

                SqlParameter pMin = new SqlParameter();
                pMin.ParameterName = "@min";
                pMin.SqlDbType = SqlDbType.Int;
                pMin.Value = min_salary;
                sqlCommand.Parameters.Add(pMin);

                SqlParameter pMax = new SqlParameter();
                pMax.ParameterName = "@max";
                pMax.SqlDbType = SqlDbType.Int;
                pMax.Value = max_salary;
                sqlCommand.Parameters.Add(pMax);

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


        //DELETE Jobs
        public static void DeleteJobs(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM Jobs WHERE id = @id";

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


        //GET BY ID Jobs
        public static void GetJobsByID(int id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT title, min_salary, max_salary FROM jobs WHERE id = @id;";

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
                        Console.WriteLine("Nama Pekerjaan: " + reader.GetString(0));
                        Console.WriteLine("Gaji Minimal: " + reader.GetInt32(1));
                        Console.WriteLine("Gaji Maksimal: " + reader.GetInt32(2));
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
