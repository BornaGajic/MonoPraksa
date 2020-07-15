using System;
using System.Data;
using System.Data.SqlClient;
using ProjectExample.Webapi.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectExample.Webapi.Models
{
    static class MyDatabase
    {
        public const string connectionString = 
                    "Data Source=BORNA-PC\\SQLEXPRESS;" +
                    "Initial Catalog=MonoPraksa;" +
                    "Integrated Security=True;";

        /*
            function parameters:
                query (string)
                    SQL query in string format.
                
                parameters (Dictionary<string, object>) [Optional]
                    Dicitonary of SQL variables starting with '@', and values associated with them.

            function returns:
                List<List<object>>
                    Table with 1:1 relationship with the query result.
        */
        public static List<List<object>> ExecuteQuery (string query, Dictionary<string, object> parameters = null)
        {
            var result = new List<List<object>>();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    List<object> row = new List<object>();
                    foreach (int i in Enumerable.Range(0, reader.FieldCount))
                    {
                        row.Add(reader[i]);
                    }

                    result.Add(row);
                }
            }

            return result;
        }

        /*
            function parameters:
                query (string)
                    SQL query in string format.
                
                parameters (Dictionary<string, object>)
                    Dicitonary of SQL variables starting with '@', and values associated with them.

            function returns:
                string
                    null if no problems were encountered, otherwise returns Exception message.
        */
        public static string ExeNonQuery (string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    var command = new SqlCommand(query, connection);
                
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return e.Message;   
                }
            }

            return null;
        }
    }
}