using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Data.SqlClient;
using ProjectExample.Utility.Common;

namespace ProjectExample.Utility
{
    public class CrudWrapper : ICrudWrapper
    {
        private string connectionString { get; }

        public CrudWrapper (string connString) => connectionString = connString;

        /*
            function parameters:
                query (string)
                    SQL query in string format.
                
                parameters (Dictionary<string, object>) [Optional]
                    Dicitonary of SQL variables starting with '@', and values associated with them.

            function returns:
                object
                    Table with 1:1 relationship with the query result (Idealy List<List<object>>).
        */
        public async Task<List<List<object>>> ExecuteQuery (string query, Dictionary<string, object> parameters = null)
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

                SqlDataReader reader = null;
                try
                {
                    reader = await Task.Run(() => command.ExecuteReader()); 
                }
                catch (Exception _)
                {
                    return null;
                }

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
                bool
                    false if something isn't working, otherwise returns true
        */
        public async Task<bool> ExeNonQuery (string query, Dictionary<string, object> parameters)
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

                    await Task.Run(() => command.ExecuteNonQuery());
                }
                catch (Exception _)
                {
                    return false;   
                }
            }

            return true;
        }
    }
}
