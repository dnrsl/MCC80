using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConectivity
{
    internal class EmployeesTable
    {
        private static string _connectionString = "Data Source=DESKTOP-HDKJSS4;Database=db_work;Integrated Security = True; Connect Timeout = 30;";

        private static SqlConnection _connection;

        static void Menu()
        {
            Console.WriteLine("=== Employees Table ===");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Get By ID");
            Console.WriteLine("5. Get All");
            Console.WriteLine("6. Back");
            Console.Write("Input: ");
        }

        public static void EmployeesMain()
        {
            Console.Clear();
            int number, id, salary, manID, depID;
            decimal comission;
            string fName, lName, email, pNumber, hire, jobID;

            do
            {
                Menu();
                number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Membuat Data Karyawan Baru");
                            Console.Write("Employee ID : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("First Name : ");
                            fName = Console.ReadLine();
                            Console.Write("Last Name : ");
                            lName = Console.ReadLine();
                            Console.Write("Email : ");
                            email = Console.ReadLine();
                            Console.Write("Phone Number : ");
                            pNumber = Console.ReadLine();
                            Console.Write("Hire Date : ");
                            hire = Console.ReadLine();
                            Console.Write("Salary : ");
                            salary = int.Parse(Console.ReadLine());
                            Console.Write("Comission pct : ");
                            comission = decimal.Parse(Console.ReadLine());
                            Console.Write("Manager ID : ");
                            manID = int.Parse(Console.ReadLine());
                            Console.Write("Job ID : ");
                            jobID = Console.ReadLine();
                            Console.Write("Department ID : ");
                            depID = int.Parse(Console.ReadLine());

                            InsertEmployees(id, fName, lName, email, pNumber, hire, salary, comission, manID, jobID, depID);
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
                            Console.WriteLine("Mengubah Data Karyawan Berdasarkan ID Karyawan");
                            Console.Write("Employee ID : ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("First Name : ");
                            fName = Console.ReadLine();
                            Console.Write("Last Name : ");
                            lName = Console.ReadLine();
                            Console.Write("Email : ");
                            email = Console.ReadLine();
                            Console.Write("Phone Number : ");
                            pNumber = Console.ReadLine();
                            Console.Write("Hire Date : ");
                            hire = Console.ReadLine();
                            Console.Write("Salary : ");
                            salary = int.Parse(Console.ReadLine());
                            Console.Write("Comission pct : ");
                            comission = decimal.Parse(Console.ReadLine());
                            Console.Write("Manager ID : ");
                            manID = int.Parse(Console.ReadLine());
                            Console.Write("Job ID : ");
                            jobID = Console.ReadLine();
                            Console.Write("Department ID : ");
                            depID = int.Parse(Console.ReadLine());
                            UpdateEmployees(id, fName, lName, email, pNumber, hire, salary, comission, manID, jobID, depID);
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
                            Console.WriteLine("Hapus Karyawan Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            id = int.Parse(Console.ReadLine());
                            DeleteEmployees(id);
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
                            Console.WriteLine("Menampilkan Karyawan Berdasarkan ID");
                            Console.Write("Masukkan ID :");
                            id = int.Parse(Console.ReadLine());
                            GetEmployeesByID(id);
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
                            GetEmployees();
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
        //GET ALL Employees
        public static void GetEmployees()
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT * FROM Employees;";

            try
            {
                _connection.Open();
                using SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Id: " + reader.GetInt32(0));
                        Console.WriteLine("First Name: " + reader.GetString(1));
                        Console.WriteLine("Last Name: " + reader.GetString(2));
                        Console.WriteLine("Email: " + reader.GetString(3));
                        Console.WriteLine("Phone Number: " + reader.GetString(4));
                        Console.WriteLine("Hire Date: " + reader.GetDateTime(5));
                        Console.WriteLine("Salary: " + reader.GetInt32(6));
                        Console.WriteLine("Commision pct: " + reader.GetDecimal(7));
                        Console.WriteLine("Manager ID: " + reader.GetInt32(8));
                        Console.WriteLine("Job ID: " + reader.GetString(9));
                        Console.WriteLine("Department ID: " + reader.GetInt32(10));
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

        //INSERT Employees
        public static void InsertEmployees(int id, string first_name, string last_name, string email, string phone_number, string hire_date, int salary, decimal comission_pct, int manager_id, string job_id, int department_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "INSERT jobs VALUES (@id, @fname, @lname, @email, @pnumber, @hire, @salary, @comission, @manid, @jobid, @depid)";

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

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@fname";
                pFName.SqlDbType = SqlDbType.VarChar;
                pFName.Value = first_name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@lname";
                pLName.SqlDbType = SqlDbType.VarChar;
                pLName.Value = last_name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = SqlDbType.VarChar;
                pEmail.Value = email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPNum = new SqlParameter();
                pPNum.ParameterName = "@pnumber";
                pPNum.SqlDbType = SqlDbType.VarChar;
                pPNum.Value = phone_number;
                sqlCommand.Parameters.Add(pPNum);

                SqlParameter pHire = new SqlParameter();
                pHire.ParameterName = "@hire";
                pHire.SqlDbType = SqlDbType.DateTime;
                pHire.Value = hire_date;
                sqlCommand.Parameters.Add(pHire);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = SqlDbType.Int;
                pSalary.Value = salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComission = new SqlParameter();
                pComission.ParameterName = "@comission";
                pComission.SqlDbType = SqlDbType.Decimal;
                pComission.Value = comission_pct;
                sqlCommand.Parameters.Add(pComission);

                SqlParameter pManID = new SqlParameter();
                pManID.ParameterName = "@manid";
                pManID.SqlDbType = SqlDbType.Int;
                pManID.Value = manager_id;
                sqlCommand.Parameters.Add(pManID);

                SqlParameter pJobID = new SqlParameter();
                pJobID.ParameterName = "@jobid";
                pJobID.SqlDbType = SqlDbType.VarChar;
                pJobID.Value = job_id;
                sqlCommand.Parameters.Add(pJobID);

                SqlParameter pDepID = new SqlParameter();
                pDepID.ParameterName = "@depid";
                pDepID.SqlDbType = SqlDbType.Int;
                pDepID.Value = department_id;
                sqlCommand.Parameters.Add(pDepID);

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
        public static void UpdateEmployees(int id, string first_name, string last_name, string email, string phone_number, string hire_date, int salary, decimal comission_pct, int manager_id, string job_id, int department_id)
        {
            _connection = new SqlConnection(_connectionString);

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "UPDATE Employees SET id = @id, first_name = @fname, last_name = @lname, @email, phone_number = @pnumber, hire_date = @hire, salary = @salary, comission_pct = @comission, manager_id = @manid, job_id = @jobid, department_id = @depid WHERE id = @id;";

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

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@fname";
                pFName.SqlDbType = SqlDbType.VarChar;
                pFName.Value = first_name;
                sqlCommand.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@lname";
                pLName.SqlDbType = SqlDbType.VarChar;
                pLName.Value = last_name;
                sqlCommand.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = SqlDbType.VarChar;
                pEmail.Value = email;
                sqlCommand.Parameters.Add(pEmail);

                SqlParameter pPNum = new SqlParameter();
                pPNum.ParameterName = "@pnumber";
                pPNum.SqlDbType = SqlDbType.VarChar;
                pPNum.Value = phone_number;
                sqlCommand.Parameters.Add(pPNum);

                SqlParameter pHire = new SqlParameter();
                pHire.ParameterName = "@hire";
                pHire.SqlDbType = SqlDbType.DateTime;
                pHire.Value = hire_date;
                sqlCommand.Parameters.Add(pHire);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = SqlDbType.Int;
                pSalary.Value = salary;
                sqlCommand.Parameters.Add(pSalary);

                SqlParameter pComission = new SqlParameter();
                pComission.ParameterName = "@comission";
                pComission.SqlDbType = SqlDbType.Decimal;
                pComission.Value = comission_pct;
                sqlCommand.Parameters.Add(pComission);

                SqlParameter pManID = new SqlParameter();
                pManID.ParameterName = "@manid";
                pManID.SqlDbType = SqlDbType.Int;
                pManID.Value = manager_id;
                sqlCommand.Parameters.Add(pManID);

                SqlParameter pJobID = new SqlParameter();
                pJobID.ParameterName = "@jobid";
                pJobID.SqlDbType = SqlDbType.VarChar;
                pJobID.Value = job_id;
                sqlCommand.Parameters.Add(pJobID);

                SqlParameter pDepID = new SqlParameter();
                pDepID.ParameterName = "@depid";
                pDepID.SqlDbType = SqlDbType.Int;
                pDepID.Value = department_id;
                sqlCommand.Parameters.Add(pDepID);

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


        //DELETE Employees
        public static void DeleteEmployees(int id)
        {
            _connection = new SqlConnection(_connectionString);


            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "DELETE FROM Employees WHERE id = @id";

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


        //GET BY ID Employees
        public static void GetEmployeesByID(int id)
        {
            _connection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = _connection;
            sqlCommand.CommandText = "SELECT first_name, last_name, email, phone_number, hire_date, salary, comission_pct, manager_id, job_id, department_id FROM Employees WHERE id = @id;";

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
                        Console.WriteLine("First Name: " + reader.GetString(0));
                        Console.WriteLine("Last Name: " + reader.GetString(1));
                        Console.WriteLine("Email: " + reader.GetString(2));
                        Console.WriteLine("Phone Number: " + reader.GetString(3));
                        Console.WriteLine("Hire Date: " + reader.GetDateTime(4));
                        Console.WriteLine("Salary: " + reader.GetInt32(5));
                        Console.WriteLine("Commision pct: " + reader.GetDecimal(6));
                        Console.WriteLine("Manager ID: " + reader.GetInt32(7));
                        Console.WriteLine("Job ID " + reader.GetString(8));
                        Console.WriteLine("Department ID" + reader.GetInt32(9));
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
