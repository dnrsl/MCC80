using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class DepartmentTable
    {
        private static string _connectionString = "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("=== Departments Table ===");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.Write("Input: ");
        }

        public static void DepartmentsMain()
        {
            Console.Clear();
            int number, id, idlocation, idManager;
            string name;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Department Baru");
                            Console.Write("Masukkan ID Department : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Nama Department : ");
                            name = Console.ReadLine();
                            Console.Write("Masukkan ID Lokasi : ");
                            idlocation = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan ID Manager : ");
                            idManager = int.Parse(Console.ReadLine());
                            InsertDepartments(id, name, idlocation, idManager);
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
                            Console.WriteLine("Merubah Data Department");
                            Console.Write("Masukkan ID Department : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan Nama Department : ");
                            name = Console.ReadLine();
                            Console.Write("Masukkan ID Lokasi : ");
                            idlocation = int.Parse(Console.ReadLine());
                            Console.Write("Masukkan ID Manager : ");
                            idManager = int.Parse(Console.ReadLine());
                            UpdateDepartments(id, name, idlocation, idManager);
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
                            DeleteDepartments(id);
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
                            GetDepartmentsByID(id);
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
                            GetDepartments();
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
        //GET ALL Departments
        public static void GetDepartments()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM departments;";

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
                        Console.WriteLine("Location ID: " + reader.GetInt32(2));
                        if (!reader.IsDBNull(3))
                        {
                            Console.WriteLine("Manager ID: " + reader.GetInt32(3));
                        }
                        else
                        {
                            Console.WriteLine("Manager ID: belum ada");
                        }
                        //Console.WriteLine("Manager ID: " + reader.GetInt32(3) ?? "NULL");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No Departments found.");
                }

                reader.Close();
                _connection.Close();
            }

            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //INSERT Departments
        public static void InsertDepartments(int id, string name, int location_id, int manager_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT departments VALUES (@id, @name, @locationid, @managerid)";

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

                SqlParameter pLocationID = new SqlParameter();
                pLocationID.ParameterName = "@locationid";
                pLocationID.SqlDbType = SqlDbType.Int;
                pLocationID.Value = location_id;
                sqlCommand.Parameters.Add(pLocationID);

                SqlParameter pManagerID = new SqlParameter();
                pManagerID.ParameterName = "@managerid";
                pManagerID.SqlDbType = SqlDbType.Int;
                pManagerID.Value = manager_id;
                sqlCommand.Parameters.Add(pManagerID);

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

        //UPDATE Departments
        public static void UpdateDepartments(int id, string name, int location_id, int manager_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE departments SET id = @id, name = @name, location_id = @locationid, manager_id = @managerid WHERE id = @id;";

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

                SqlParameter pLocationID = new SqlParameter();
                pLocationID.ParameterName = "@locationid";
                pLocationID.SqlDbType = SqlDbType.Int;
                pLocationID.Value = location_id;
                sqlCommand.Parameters.Add(pLocationID);

                SqlParameter pManagerID = new SqlParameter();
                pManagerID.ParameterName = "@managerid";
                pManagerID.SqlDbType = SqlDbType.Int;
                pManagerID.Value = manager_id;
                sqlCommand.Parameters.Add(pManagerID);

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


        //DELETE Departments
        public static void DeleteDepartments(int id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM departments WHERE id = @id";

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


        //GET BY ID Departments
        public static void GetDepartmentsByID(int id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT name, location_id, manager_id FROM departments WHERE id = @id;";

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
                        Console.WriteLine("Nama Department: " + reader.GetString(0));
                        Console.WriteLine("Location ID: " + reader.GetInt32(1));
                        Console.WriteLine("Manager ID: " + reader.GetInt32(2));
                    }
                }
                else
                {
                    Console.WriteLine("No Departments found.");
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
