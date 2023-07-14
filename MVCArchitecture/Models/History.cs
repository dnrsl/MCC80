using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models;

public class History
{
    public DateTime StartDate { get; set; }
    public int EmployeeId { get; set; }
    public DateTime EndDate { get; set; }
    public int DepartmentId { get; set; }
    public string JobId { get; set; }

    public List<History> GetAll()
    {
        var connection = Connection.Get();

        var histories = new List<History>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM histories;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    History history = new History();
                    history.StartDate = reader.GetDateTime(0);
                    history.EmployeeId = reader.GetInt32(1);
                    history.EndDate = reader.GetDateTime(2);
                    history.DepartmentId = reader.GetInt32(3);
                    history.JobId = reader.GetString(4);
                    histories.Add(history);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return histories;
        }
        catch
        {
            return new List<History>();
        }
    }

    public History GetByID (int empID)
    {
        var history = new History();
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM histories WHERE employee_id = @employee_id;";
        sqlCommand.Parameters.AddWithValue("@employee_id", empID);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                history.StartDate = reader.GetDateTime(0);
                history.EmployeeId = reader.GetInt32(1);
                history.EndDate = reader.GetDateTime(2);
                history.DepartmentId = reader.GetInt32(3);
                history.JobId = reader.GetString(4);
            }
            reader.Close();
            connection.Close();

            return history;
        }

        catch
        {
            return null;
        }

        
    }

    public int Insert (History history)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT histories VALUES (@start, @employeeid, @end, @departmentid, @jobid)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;
        try
        {
            SqlParameter pStart = new SqlParameter();
            pStart.ParameterName = "@start";
            pStart.SqlDbType = SqlDbType.DateTime;
            pStart.Value = history.StartDate;
            sqlCommand.Parameters.Add(pStart);

            SqlParameter pEmpID = new SqlParameter();
            pEmpID.ParameterName = "@employeeid";
            pEmpID.SqlDbType = SqlDbType.Int;
            pEmpID.Value = history.EmployeeId;
            sqlCommand.Parameters.Add(pEmpID);

            SqlParameter pEnd = new SqlParameter();
            pEnd.ParameterName = "@end";
            pEnd.SqlDbType = SqlDbType.DateTime;
            pEnd.Value = history.EndDate;
            sqlCommand.Parameters.Add(pEnd);

            SqlParameter pDepID = new SqlParameter();
            pDepID.ParameterName = "@departmentid";
            pDepID.SqlDbType = SqlDbType.Int;
            pDepID.Value = history.DepartmentId;
            sqlCommand.Parameters.Add(pDepID);

            SqlParameter pJobID = new SqlParameter();
            pJobID.ParameterName = "@jobid";
            pJobID.SqlDbType = SqlDbType.VarChar;
            pJobID.Value = history.JobId;
            sqlCommand.Parameters.Add(pJobID);


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

    public int Update(History history)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE histories SET start_date = @start, end_date = @end, department_id = @departmentid, job_id = @jobid WHERE employee_id = @employeeid";
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pStart = new SqlParameter();
            pStart.ParameterName = "@start";
            pStart.SqlDbType = SqlDbType.DateTime;
            pStart.Value = history.StartDate;
            sqlCommand.Parameters.Add(pStart);

            SqlParameter pEmpID = new SqlParameter();
            pEmpID.ParameterName = "@employeeid";
            pEmpID.SqlDbType = SqlDbType.Int;
            pEmpID.Value = history.EmployeeId;
            sqlCommand.Parameters.Add(pEmpID);

            SqlParameter pEnd = new SqlParameter();
            pEnd.ParameterName = "@end";
            pEnd.SqlDbType = SqlDbType.DateTime;
            pEnd.Value = history.EndDate;
            sqlCommand.Parameters.Add(pEnd);

            SqlParameter pDepID = new SqlParameter();
            pDepID.ParameterName = "@departmentid";
            pDepID.SqlDbType = SqlDbType.Int;
            pDepID.Value = history.DepartmentId;
            sqlCommand.Parameters.Add(pDepID);

            SqlParameter pJobID = new SqlParameter();
            pJobID.ParameterName = "@jobid";
            pJobID.SqlDbType = SqlDbType.VarChar;
            pJobID.Value = history.JobId;
            sqlCommand.Parameters.Add(pJobID);


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

    public int Delete(History history)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM Histories WHERE employee_id = @employeeid";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pEmpID = new SqlParameter();
            pEmpID.ParameterName = "@employeeid";
            pEmpID.SqlDbType = SqlDbType.Int;
            pEmpID.Value = history.EmployeeId;
            sqlCommand.Parameters.Add(pEmpID);
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
