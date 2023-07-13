using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Xml.Linq;
using MVCArchitecture.Views;

namespace MVCArchitecture.Models;

public class Region
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<Region> GetAll()
    {
        var connection = Connection.Get();
        var regions = new List<Region>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM regions";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Region region = new Region();
                    region.Id = reader.GetInt32(0);
                    region.Name = reader.GetString(1);
                    regions.Add(region);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }

            return regions;
        }
        catch
        {
            return new List<Region>();
        }
    }

    public Region GetByID(int id)
    {
        var region = new Region();

        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM regions WHERE id = @id";
        sqlCommand.Parameters.AddWithValue("id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                region.Id = reader.GetInt32(0);
                region.Name = reader.GetString(1);
            }

            reader.Close();
            connection.Close();
        }
        catch
        {
            return null;
        }

        return region;
    }

    public int Insert(Region region)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO regions VALUES (@name)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = region.Name;
            sqlCommand.Parameters.Add(pName);

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

    public int Update(Region region)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE regions SET name = @name WHERE id = @id;";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {

            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = region.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = region.Name;
            sqlCommand.Parameters.Add(pName);

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

    public int Delete(Region region)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM regions WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = region.Id;
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
