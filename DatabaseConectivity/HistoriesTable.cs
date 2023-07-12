using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class HistoriesTable
    {
        private static string _connectionString = "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("=== Histories Table ===");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.Write("Input: ");
        }

        public static void HistoriesMain()
        {
            Console.Clear();
            int number, empID, jobID, depID;
            string start, end;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Riwayat Baru");
                            Console.Write("Tanggal Mulai : ");
                            start = Console.ReadLine();
                            Console.Write("Masukkan ID Karyawan : ");
                            empID = int.Parse(Console.ReadLine());
                            Console.Write("Tanggal Keluar : ");
                            end = Console.ReadLine();
                            Console.Write("Masukkan ID Department : ");
                            depID = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan ID Pekerjaan : ");
                            jobID = int.Parse(Console.ReadLine());
                            InsertHistories(start, empID, end, depID, jobID);
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
                            Console.WriteLine("Mengubah Data Department berdasarkan ID Karyawan");
                            Console.Write("Masukkan ID Karyawan : ");
                            empID = int.Parse(Console.ReadLine());
                            Console.Write("Tanggal Mulai : ");
                            start = Console.ReadLine();
                            Console.Write("Tanggal Keluar : ");
                            end = Console.ReadLine();
                            Console.Write("Masukkan ID Department : ");
                            depID = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan ID Pekerjaan : ");
                            jobID = int.Parse(Console.ReadLine());
                            UpdateHistories(start, empID, end, depID, jobID);
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
                            Console.WriteLine("Hapus Pekerjaan Berdasarkan ID Karyawan");
                            Console.Write("Masukkan ID :");
                            empID = int.Parse(Console.ReadLine());
                            DeleteHistories(empID);
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
                            Console.WriteLine("Menampilkan Riwayat Berdasarkan ID Karyawan");
                            Console.Write("Masukkan ID :");
                            empID = int.Parse(Console.ReadLine());
                            GetHistoriesByID(empID);
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
                            GetHistories();
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
        //GET ALL Histories
        public static void GetHistories()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM histories;";

            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Start Date: " + reader.GetDateTime(0));
                        Console.WriteLine("Employee ID: " + reader.GetInt32(1));
                        Console.WriteLine("End Date: " + reader.GetDateTime(2));
                        Console.WriteLine("Department ID: " + reader.GetInt32(3));
                        Console.WriteLine("Job ID: " + reader.GetString(4));
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

        //INSERT Histories
        public static void InsertHistories(string start_date, int employee_id, string end_date, int department_id, int job_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT histories VALUES (@start, @employeeid, @end, @departmentid, @jobid)";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pStart = new SqlParameter();
                pStart.ParameterName = "@start";
                pStart.SqlDbType = SqlDbType.DateTime;
                pStart.Value = start_date;
                sqlCommand.Parameters.Add(pStart);

                SqlParameter pEmpID = new SqlParameter();
                pEmpID.ParameterName = "@employeeid";
                pEmpID.SqlDbType = SqlDbType.Int;
                pEmpID.Value = employee_id;
                sqlCommand.Parameters.Add(pEmpID);

                SqlParameter pEnd = new SqlParameter();
                pEnd.ParameterName = "@end";
                pEnd.SqlDbType = SqlDbType.DateTime;
                pEnd.Value = end_date;
                sqlCommand.Parameters.Add(pEnd);

                SqlParameter pDepID = new SqlParameter();
                pDepID.ParameterName = "@departmentid";
                pDepID.SqlDbType = SqlDbType.Int;
                pDepID.Value = department_id;
                sqlCommand.Parameters.Add(pDepID);

                SqlParameter pJobID = new SqlParameter();
                pJobID.ParameterName = "@jobid";
                pJobID.SqlDbType = SqlDbType.Int;
                pJobID.Value = job_id;
                sqlCommand.Parameters.Add(pJobID);


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

        //UPDATE Histories
        public static void UpdateHistories(string start_date, int employee_id, string end_date, int department_id, int job_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE histories SET start_date = @start, end_date = @end, department_id = @departmentid, job_id = @jobid WHERE employee_id = @employeeid";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {

                SqlParameter pStart = new SqlParameter();
                pStart.ParameterName = "@start";
                pStart.SqlDbType = SqlDbType.DateTime;
                pStart.Value = start_date;
                sqlCommand.Parameters.Add(pStart);

                SqlParameter pEmpID = new SqlParameter();
                pEmpID.ParameterName = "@employeeid";
                pEmpID.SqlDbType = SqlDbType.Int;
                pEmpID.Value = employee_id;
                sqlCommand.Parameters.Add(pEmpID);

                SqlParameter pEnd = new SqlParameter();
                pEnd.ParameterName = "@end";
                pEnd.SqlDbType = SqlDbType.DateTime;
                pEnd.Value = end_date;
                sqlCommand.Parameters.Add(pEnd);

                SqlParameter pDepID = new SqlParameter();
                pDepID.ParameterName = "@departmentid";
                pDepID.SqlDbType = SqlDbType.Int;
                pDepID.Value = department_id;
                sqlCommand.Parameters.Add(pDepID);

                SqlParameter pJobID = new SqlParameter();
                pJobID.ParameterName = "@jobid";
                pJobID.SqlDbType = SqlDbType.Int;
                pJobID.Value = job_id;
                sqlCommand.Parameters.Add(pJobID);

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


        //DELETE Histories
        public static void DeleteHistories(int employee_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM Histories WHERE employee_id = @employeeid";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pEmpID = new SqlParameter();
                pEmpID.ParameterName = "@employeeid";
                pEmpID.SqlDbType = SqlDbType.Int;
                pEmpID.Value = employee_id;
                sqlCommand.Parameters.Add(pEmpID);

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


        //GET BY ID Histories
        public static void GetHistoriesByID(int employee_id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT start_date, employee_id, end_date, department_id, job_id FROM Histories WHERE employee_id = @employeeid";

            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "@employeeid";
                pID.SqlDbType = SqlDbType.Int;
                pID.Value = employee_id;
                sqlCommand.Parameters.Add(pID);

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Start Date " + reader.GetDateTime(0));
                        Console.WriteLine("Employee ID " + reader.GetInt32(1));
                        Console.WriteLine("End Date " + reader.GetDateTime(2));
                        Console.WriteLine("Department ID: " + reader.GetInt32(3));
                        Console.WriteLine("Job ID " + reader.GetString(4));
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
