using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models;

public class Employee
{
    public int Id { get; set; }
    public string FName { get; set; }
    public string? LName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime Hire { get; set; }
    public int? Salary { get; set; }
    public decimal? Comission { get; set; }
    public int? ManagerId { get; set; }
    public string? JobId { get; set; }
    public int? DepartmentId { get; set; }

    public List<Employee> GetAll()
    {
        var connection = Connection.Get();
        var employees = new List<Employee>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM Employees;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32(0);
                    employee.FName = reader.GetString(1);
                    employee.LName = reader.GetString(2);
                    employee.Email = reader.GetString(3);
                    employee.Phone = reader.GetString(4);
                    employee.Hire = reader.GetDateTime(5);
                    employee.Salary = reader.GetInt32(6);
                    employee.Comission = reader.GetDecimal(7);
                    if (!reader.IsDBNull(3))
                    {
                        employee.ManagerId = reader.GetInt32(8);
                    }
                    employee.JobId = reader.GetString(9);
                    employee.DepartmentId = reader.GetInt32(10);
                    employees.Add(employee);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return employees;
        }
        catch
        {
            return new List<Employee>();
        }
    }

    public Employee GetByID(int id)
    {
        var employee = new Employee();

        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM Employees WHERE id = @id;";
        sqlCommand.Parameters.AddWithValue("id", id);
        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                employee.Id = reader.GetInt32(0);
                employee.FName = reader.GetString(1);
                employee.LName = reader.GetString(2);
                employee.Email = reader.GetString(3);
                employee.Phone = reader.GetString(4);
                employee.Hire = reader.GetDateTime(5);
                employee.Salary = reader.GetInt32(6);
                employee.Comission = reader.GetDecimal(7);
                if (!reader.IsDBNull(3))
                {
                    employee.ManagerId = reader.GetInt32(8);
                }
                employee.JobId = reader.GetString(9);
                employee.DepartmentId = reader.GetInt32(10);
            }
            reader.Close();
            connection.Close();
        }
        catch
        {
            return null;
        }
        return employee;
    }

    public int Insert(Employee employee)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT jobs VALUES (@id, @fname, @lname, @email, @pnumber, @hire, @salary, @comission, @manid, @jobid, @depid)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = employee.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pFName = new SqlParameter();
            pFName.ParameterName = "@fname";
            pFName.SqlDbType = SqlDbType.VarChar;
            pFName.Value = employee.FName;
            sqlCommand.Parameters.Add(pFName);

            SqlParameter pLName = new SqlParameter();
            pLName.ParameterName = "@lname";
            pLName.SqlDbType = SqlDbType.VarChar;
            pLName.Value = employee.LName;
            sqlCommand.Parameters.Add(pLName);

            SqlParameter pEmail = new SqlParameter();
            pEmail.ParameterName = "@email";
            pEmail.SqlDbType = SqlDbType.VarChar;
            pEmail.Value = employee.Email;
            sqlCommand.Parameters.Add(pEmail);

            SqlParameter pPNum = new SqlParameter();
            pPNum.ParameterName = "@pnumber";
            pPNum.SqlDbType = SqlDbType.VarChar;
            pPNum.Value = employee.Phone;
            sqlCommand.Parameters.Add(pPNum);

            SqlParameter pHire = new SqlParameter();
            pHire.ParameterName = "@hire";
            pHire.SqlDbType = SqlDbType.DateTime;
            pHire.Value = employee.Hire;
            sqlCommand.Parameters.Add(pHire);

            SqlParameter pSalary = new SqlParameter();
            pSalary.ParameterName = "@salary";
            pSalary.SqlDbType = SqlDbType.Int;
            pSalary.Value = employee.Salary;
            sqlCommand.Parameters.Add(pSalary);

            SqlParameter pComission = new SqlParameter();
            pComission.ParameterName = "@comission";
            pComission.SqlDbType = SqlDbType.Decimal;
            pComission.Value = employee.Comission;
            sqlCommand.Parameters.Add(pComission);

            SqlParameter pManID = new SqlParameter();
            pManID.ParameterName = "@manid";
            pManID.SqlDbType = SqlDbType.Int;
            pManID.Value = employee.ManagerId;
            sqlCommand.Parameters.Add(pManID);

            SqlParameter pJobID = new SqlParameter();
            pJobID.ParameterName = "@jobid";
            pJobID.SqlDbType = SqlDbType.VarChar;
            pJobID.Value = employee.JobId;
            sqlCommand.Parameters.Add(pJobID);

            SqlParameter pDepID = new SqlParameter();
            pDepID.ParameterName = "@depid";
            pDepID.SqlDbType = SqlDbType.Int;
            pDepID.Value = employee.DepartmentId;
            sqlCommand.Parameters.Add(pDepID);

            int result = sqlCommand.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();

            return result;
        }
        catch
        {
            transaction.Rollback();
            return -1;
        }
    }

    public int Update(Employee employee)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE Employees SET id = @id, first_name = @fname, last_name = @lname, @email, phone_number = @pnumber, hire_date = @hire, salary = @salary, comission_pct = @comission, manager_id = @manid, job_id = @jobid, department_id = @depid WHERE id = @id;";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = employee.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pFName = new SqlParameter();
            pFName.ParameterName = "@fname";
            pFName.SqlDbType = SqlDbType.VarChar;
            pFName.Value = employee.FName;
            sqlCommand.Parameters.Add(pFName);

            SqlParameter pLName = new SqlParameter();
            pLName.ParameterName = "@lname";
            pLName.SqlDbType = SqlDbType.VarChar;
            pLName.Value = employee.LName;
            sqlCommand.Parameters.Add(pLName);

            SqlParameter pEmail = new SqlParameter();
            pEmail.ParameterName = "@email";
            pEmail.SqlDbType = SqlDbType.VarChar;
            pEmail.Value = employee.Email;
            sqlCommand.Parameters.Add(pEmail);

            SqlParameter pPNum = new SqlParameter();
            pPNum.ParameterName = "@pnumber";
            pPNum.SqlDbType = SqlDbType.VarChar;
            pPNum.Value = employee.Phone;
            sqlCommand.Parameters.Add(pPNum);

            SqlParameter pHire = new SqlParameter();
            pHire.ParameterName = "@hire";
            pHire.SqlDbType = SqlDbType.DateTime;
            pHire.Value = employee.Hire;
            sqlCommand.Parameters.Add(pHire);

            SqlParameter pSalary = new SqlParameter();
            pSalary.ParameterName = "@salary";
            pSalary.SqlDbType = SqlDbType.Int;
            pSalary.Value = employee.Salary;
            sqlCommand.Parameters.Add(pSalary);

            SqlParameter pComission = new SqlParameter();
            pComission.ParameterName = "@comission";
            pComission.SqlDbType = SqlDbType.Decimal;
            pComission.Value = employee.Comission;
            sqlCommand.Parameters.Add(pComission);

            SqlParameter pManID = new SqlParameter();
            pManID.ParameterName = "@manid";
            pManID.SqlDbType = SqlDbType.Int;
            pManID.Value = employee.ManagerId;
            sqlCommand.Parameters.Add(pManID);

            SqlParameter pJobID = new SqlParameter();
            pJobID.ParameterName = "@jobid";
            pJobID.SqlDbType = SqlDbType.VarChar;
            pJobID.Value = employee.JobId;
            sqlCommand.Parameters.Add(pJobID);

            SqlParameter pDepID = new SqlParameter();
            pDepID.ParameterName = "@depid";
            pDepID.SqlDbType = SqlDbType.Int;
            pDepID.Value = employee.DepartmentId;
            sqlCommand.Parameters.Add(pDepID);

            int result = sqlCommand.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();

            return result;
        }

        catch
        {
            transaction.Rollback();
            return -1;
        }
    }

    public int Delete (Employee employee)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM Employees WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = employee.Id;
            sqlCommand.Parameters.Add(pID);

            int result = sqlCommand.ExecuteNonQuery();

            transaction.Commit();
            connection.Close();

            return result;
        }

        catch
        {
            transaction.Rollback();
            return -1;
        }
    }
}
