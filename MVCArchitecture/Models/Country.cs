using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVCArchitecture.Models;

public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }

    public List<Country> GetAll()
    {
        var connection = Connection.Get();
        var countries = new List<Country>();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM countries;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Country country = new Country();
                    country.Id = reader.GetString(0);
                    country.Name = reader.GetString(1);
                    country.RegionId = reader.GetInt32(2);
                    countries.Add(country);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return countries;
        }
        catch
        {
            return new List<Country>();
        }
    }

    public Country GetByID(int id)
    {
        var country = new Country();

        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT id, name, region_id FROM countries WHERE id = @id";
        sqlCommand.Parameters.AddWithValue("id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                country.Id = reader.GetString(0);
                country.Name = reader.GetString(1);
                country.RegionId = reader.GetInt32(2);
            }

            reader.Close();
            connection.Close();
        }

        catch
        {
            return null;
        }

        return country;
    }

    public int Insert(Country country)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT countries VALUES (@id, @name, @regionid)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = country.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = country.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegionID = new SqlParameter();
            pRegionID.ParameterName = "@regionid";
            pRegionID.SqlDbType = SqlDbType.Int;
            pRegionID.Value = country.RegionId;
            sqlCommand.Parameters.Add(pRegionID);

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
    
    public int Update(Country country)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE countries SET name = @name, region_id = @regionid WHERE id = @id;";
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = country.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.SqlDbType = SqlDbType.VarChar;
            pName.Value = country.Name;
            sqlCommand.Parameters.Add(pName);

            SqlParameter pRegionID = new SqlParameter();
            pRegionID.ParameterName = "@regionid";
            pRegionID.SqlDbType = SqlDbType.Int;
            pRegionID.Value = country.RegionId;
            sqlCommand.Parameters.Add(pRegionID);

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

    public int Delete (Country country)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM countries WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = country.Id;
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
