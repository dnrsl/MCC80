using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models;

public class Location
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Postal { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string CountryId { get; set; }

    public List<Location> GetAll()
    {
        var connection = Connection.Get();
        var locations = new List<Location>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM locations;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Location location = new Location();
                    location.Id = reader.GetInt32(0);
                    location.Address = reader.GetString(1);
                    location.Postal = reader.GetString(2);
                    location.City = reader.GetString(3);
                    location.State = reader.GetString(4);
                    location.CountryId = reader.GetString(5);
                    locations.Add(location);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return locations;
        }
        catch
        {
            return new List<Location>();
        }
    }

    public Location GetByID(int id)
    {
        var location = new Location();
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT id, street_address, postal_code, city, state_province, country_id FROM locations WHERE id = @id;";

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                location.Id = reader.GetInt32(0);
                location.Address = reader.GetString(1);
                location.Postal = reader.GetString(2);
                location.City = reader.GetString(3);
                location.State = reader.GetString(4);
                location.CountryId = reader.GetString(5);
            }

            reader.Close();
            connection.Close();
        }
        catch
        {
            return null;
        }

        return location;
    }

    public int Insert(Location location)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT locations VALUES (@id, @address, @postal, @city, @province, @countryid)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = location.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pAddress = new SqlParameter();
            pAddress.ParameterName = "@address";
            pAddress.SqlDbType = SqlDbType.VarChar;
            pAddress.Value = location.Address;
            sqlCommand.Parameters.Add(pAddress);

            SqlParameter pPostal = new SqlParameter();
            pPostal.ParameterName = "@postal";
            pPostal.SqlDbType = SqlDbType.VarChar;
            pPostal.Value = location.Postal;
            sqlCommand.Parameters.Add(pPostal);

            SqlParameter pCity = new SqlParameter();
            pCity.ParameterName = "@city";
            pCity.SqlDbType = SqlDbType.VarChar;
            pCity.Value = location.City;
            sqlCommand.Parameters.Add(pCity);

            SqlParameter pProvince = new SqlParameter();
            pProvince.ParameterName = "@province";
            pProvince.SqlDbType = SqlDbType.VarChar;
            pProvince.Value = location.State;
            sqlCommand.Parameters.Add(pProvince);

            SqlParameter pCountryID = new SqlParameter();
            pCountryID.ParameterName = "@countryid";
            pCountryID.SqlDbType = SqlDbType.Int;
            pCountryID.Value = location.CountryId;
            sqlCommand.Parameters.Add(pCountryID);

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

    public int Update (Location location)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE locations SET street_address = @address, postal_code = @postal, city = @city, state_province = @province, country_id = @countryid WHERE id = @id;";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = location.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pAddress = new SqlParameter();
            pAddress.ParameterName = "@address";
            pAddress.SqlDbType = SqlDbType.VarChar;
            pAddress.Value = location.Address;
            sqlCommand.Parameters.Add(pAddress);

            SqlParameter pPostal = new SqlParameter();
            pPostal.ParameterName = "@postal";
            pPostal.SqlDbType = SqlDbType.VarChar;
            pPostal.Value = location.Postal;
            sqlCommand.Parameters.Add(pPostal);

            SqlParameter pCity = new SqlParameter();
            pCity.ParameterName = "@city";
            pCity.SqlDbType = SqlDbType.VarChar;
            pCity.Value = location.City;
            sqlCommand.Parameters.Add(pCity);

            SqlParameter pProvince = new SqlParameter();
            pProvince.ParameterName = "@province";
            pProvince.SqlDbType = SqlDbType.VarChar;
            pProvince.Value = location.State;
            sqlCommand.Parameters.Add(pProvince);

            SqlParameter pCountryID = new SqlParameter();
            pCountryID.ParameterName = "@countryid";
            pCountryID.SqlDbType = SqlDbType.Int;
            pCountryID.Value = location.CountryId;
            sqlCommand.Parameters.Add(pCountryID);

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

    public int Delete (Location location)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELETE FROM locations WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.Int;
            pID.Value = location.Id;
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
