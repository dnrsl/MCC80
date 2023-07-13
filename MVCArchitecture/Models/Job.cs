using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Models;

public class Job
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }

    public List<Job> GetAll()
    {
        var connection = Connection.Get();
        var jobs = new List<Job>();

        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM jobs;";

        try
        {
            connection.Open();
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Job job = new Job();
                    job.Id = reader.GetString(0);
                    job.Title = reader.GetString(1);
                    job.Min = reader.GetInt32(2);
                    job.Max = reader.GetInt32(3);
                    jobs.Add(job);
                }
            }
            else
            {
                reader.Close();
                connection.Close();
            }
            return jobs;
        }
        catch
        {
            return new List<Job>();
        }
    }

    public Job GetByID(string id)
    {
        var job = new Job();
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT title, min_salary, max_salary FROM jobs WHERE id = @id;";
        sqlCommand.Parameters.AddWithValue("id", id);

        try
        {
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                job.Id = reader.GetString(0);
                job.Title = reader.GetString(1);
                job.Min = reader.GetInt32(2);
                job.Max = reader.GetInt32(3);
            }
            reader.Close();
            connection.Close();
        }
        catch
        {
            return null;
        }

        return job;
    }

    public int Insert(Job job)
    {
        var connection = Connection.Get();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT jobs VALUES (@id, @title, @min, @max)";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = job.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = SqlDbType.VarChar;
            pTitle.Value = job.Title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMin = new SqlParameter();
            pMin.ParameterName = "@min";
            pMin.SqlDbType = SqlDbType.Int;
            pMin.Value = job.Min;
            sqlCommand.Parameters.Add(pMin);

            SqlParameter pMax = new SqlParameter();
            pMax.ParameterName = "@max";
            pMax.SqlDbType = SqlDbType.Int;
            pMax.Value = job.Max;
            sqlCommand.Parameters.Add(pMax);

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

    public int Update(Job job)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "UPDATE jobs SET id = @id, title = @title, min_salary = @min, max_salary = @max WHERE id = @id;";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = job.Id;
            sqlCommand.Parameters.Add(pID);

            SqlParameter pTitle = new SqlParameter();
            pTitle.ParameterName = "@title";
            pTitle.SqlDbType = SqlDbType.VarChar;
            pTitle.Value = job.Title;
            sqlCommand.Parameters.Add(pTitle);

            SqlParameter pMin = new SqlParameter();
            pMin.ParameterName = "@min";
            pMin.SqlDbType = SqlDbType.Int;
            pMin.Value = job.Min;
            sqlCommand.Parameters.Add(pMin);

            SqlParameter pMax = new SqlParameter();
            pMax.ParameterName = "@max";
            pMax.SqlDbType = SqlDbType.Int;
            pMax.Value = job.Max;
            sqlCommand.Parameters.Add(pMax);

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

    public int Delete (Job job)
    {
        var connection = Connection.Get();

        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;

        sqlCommand.CommandText = "DELETE FROM Jobs WHERE id = @id";

        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        sqlCommand.Transaction = transaction;

        try
        {
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@id";
            pID.SqlDbType = SqlDbType.VarChar;
            pID.Value = job.Id;
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
