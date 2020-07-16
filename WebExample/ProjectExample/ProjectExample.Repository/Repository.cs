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
using ProjectExample.Repository.Common;
using ProjectExample.Utility;
using ProjectExample.Model;

namespace ProjectExample.Repository
{
    public class Repository : IRepository
    {
        private static readonly CrudWrapper wrapper = new CrudWrapper(connectionString);

        public const string connectionString = "Data Source=BORNA-PC\\SQLEXPRESS;" +
                                            "Initial Catalog=MonoPraksa;" +
                                            "Integrated Security=True;";
        public List<Person> GetPersonList () 
        { 
            var query = "SELECT * FROM dbo.Person";

            var resultList = wrapper.ExecuteQuery(query);
            var result = new List<Person>();

            foreach (List<object> row in resultList)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }

            return result;
        }

        public List<Person> GetPerson (Person person) 
        { 
            string query = "SELECT * FROM dbo.Person " +
                           "WHERE first_name = @personFName AND last_name = @personLName;";
            
            var parameters = person is null ? null : new Dictionary<string, object> ()
            {
                {"@personFName", person.FirstName},
                {"@personLName", person.LastName},
            };

            var resultQuery = wrapper.ExecuteQuery(query, parameters);

            var result = new List<Person>();
            foreach (var row in resultQuery)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }

            return result;
        }

        public List<List<object>> GetPersonJobDetails (int? id = null)
        {
            string query = id is null ? "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id;" : 
                                        "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id WHERE p.person_id = @ID;";

            var parameters = id is null ? null : new Dictionary<string, object>(){{"@ID", id}};

            return wrapper.ExecuteQuery(query, parameters);
        }

        public int? GetJobID (string jobName)
        {
            string query = "SELECT job_id FROM dbo.Job WHERE job_name = @jobName;";

            var parameters = new Dictionary<string, object>(){{"@jobName", jobName}};
            
            var result = wrapper.ExecuteQuery(query, parameters);
            
            return  result == null ? null : (int?)result[0][0];
        }

        public string InsertPerson (Person person)
        {
            string query = "INSERT INTO dbo.Person VALUES (@id, @fname, @lname, @jobid)";

            var parameters = new Dictionary<string, object>()
            {
                {"@id", person.PersonID},
                {"@fname", person.FirstName},
                {"@lname", person.LastName},
                {"@jobid", person.JobFK}
            };
            
            return wrapper.ExeNonQuery(query, parameters);
        }

        public string DeletePerson (Person person)
        {
            string query = "DELETE FROM dbo.Person WHERE first_name = @fname AND last_name = @lname;";

            var parameters = new Dictionary<string, object>()
            {
                {"@fname", person.FirstName},
                {"@lname", person.LastName}
            };

            string result = wrapper.ExeNonQuery(query, parameters);

            return result;
        }

        public string UpdateJob (Person person, int jobFK)
        {
           string query = "UPDATE dbo.Person SET job_fk = @newFK WHERE first_name = @fname AND last_name = @lname;";
           
           var parameters = new Dictionary<string, object>()
           {
                {"@newFK", jobFK},
                {"@fname", person.FirstName},
                {"@lname", person.LastName}
           };

           string result = wrapper.ExeNonQuery(query, parameters);

           return result;
        }
    }
}
