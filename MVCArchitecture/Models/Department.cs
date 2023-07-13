using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVCArchitecture.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public int? ManagerId { get; set; }

    public List<Department> GetAll()
    {
        var connection = Connection.Get();
        var departments = new List<Department>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM departments;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Department department = new Department();
                    department.Id = reader.GetInt32(0);
                    department.Name = reader.GetString(1);
                    department.LocationId = reader.GetInt32(2);
                    if (!reader.IsDBNull(3))
                    {
                        department.ManagerId = reader.GetInt32(3);
                    }

                    departments.Add(department);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return departments;
        }
        catch
        {
            return new List<Department>();
        }
    }

    public Department GetByID (int id)
    {
        var department = new Department();
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT id, name, location_id, manager_id FROM departments WHERE id = @id;";
        sqlCommand.Parameters.AddWithValue("id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                department.Id = reader.GetInt32(0);
                department.Name = reader.GetString(1);
                department.LocationId = reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                {
                    department.ManagerId = reader.GetInt32(3);
                }
            }

            reader.Close();
            connection.Close();
        }
        catch 
        {
            return null;
        }
        return department;
    }

    public int Insert(Department department)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT departments VALUES (@id, @name, @locationid, @managerid)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = department.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = department.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pLocationID = new SqlParameter();
            pLocationID.ParameterName = "@locationid";
            pLocationID.SqlDbType = SqlDbType.Int;
            pLocationID.Value = department.LocationId;
            sqlCommand.Parameters.Add(pLocationID);

            SqlParameter pManagerID = new SqlParameter();
            pManagerID.ParameterName = "@managerid";
            pManagerID.SqlDbType = SqlDbType.Int;
            pManagerID.Value = department.ManagerId;
            sqlCommand.Parameters.Add(pManagerID);

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

    public int Update (Department department)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE departments SET id = @id, name = @name, location_id = @locationid, manager_id = @managerid WHERE id = @id;";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = department.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = department.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pLocationID = new SqlParameter();
            pLocationID.ParameterName = "@locationid";
            pLocationID.SqlDbType = SqlDbType.Int;
            pLocationID.Value = department.LocationId;
            sqlCommand.Parameters.Add(pLocationID);

            SqlParameter pManagerID = new SqlParameter();
            pManagerID.ParameterName = "@managerid";
            pManagerID.SqlDbType = SqlDbType.Int;
            pManagerID.Value = department.ManagerId;
            sqlCommand.Parameters.Add(pManagerID);

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

    public int Delete (Department department)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM departments WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = department.Id;
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
