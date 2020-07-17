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
        private readonly CrudWrapper wrapper = new CrudWrapper(connectionString);

        public const string connectionString = "Data Source=BORNA-PC\\SQLEXPRESS;" +
                                            "Initial Catalog=MonoPraksa;" +
                                            "Integrated Security=True;";
        public async Task<List<Person>> GetPersonList () 
        { 
            var query = "SELECT * FROM dbo.Person";

            var resultList = await wrapper.ExecuteQuery(query);
            var result = new List<Person>();

            foreach (List<object> row in resultList)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }

            return result;
        }

        public async Task<List<Person>> GetPerson (Person person) 
        { 
            string query = "SELECT * FROM dbo.Person " +
                           "WHERE first_name = @personFName AND last_name = @personLName;";
            
            var parameters = person is null ? null : new Dictionary<string, object> ()
            {
                {"@personFName", person.FirstName},
                {"@personLName", person.LastName},
            };

            var resultQuery = await wrapper.ExecuteQuery(query, parameters);

            var result = new List<Person>();
            foreach (var row in resultQuery)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }

            return result;
        }

        public async Task<List<List<object>>> GetPersonJobDetails (int? id = null)
        {
            string query = id is null ? "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id;" : 
                                        "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id WHERE p.person_id = @ID;";

            var parameters = id is null ? null : new Dictionary<string, object>(){{"@ID", id.Value}};

            return await wrapper.ExecuteQuery(query, parameters);
        }

        public async Task<int?> GetJobID (string jobName)
        {
            string query = "SELECT job_id FROM dbo.Job WHERE job_name = @jobName;";

            var parameters = new Dictionary<string, object>(){{"@jobName", jobName}};
            
            List<List<object>> result = null;
            try
            {
                result = await wrapper.ExecuteQuery(query, parameters) ?? throw new Exception();
            }
            catch (Exception _)
            {
                return null;
            }
            
            return (int?)result[0][0];
        }

        public async Task<bool> InsertPerson (Person person)
        {
            string query = "INSERT INTO dbo.Person VALUES (@id, @fname, @lname, @jobid)";

            var parameters = new Dictionary<string, object>()
            {
                {"@id", person.PersonID},
                {"@fname", person.FirstName},
                {"@lname", person.LastName},
                {"@jobid", person.JobFK}
            };
            
            return await wrapper.ExeNonQuery(query, parameters);
        }

        public async Task<bool> DeletePerson (Person person, bool isJobSpecified = false)
        {
            string query =  isJobSpecified ? "DELETE FROM dbo.Person WHERE first_name = @fname AND last_name = @lname AND job_fk = @jobfk;" :
                                             "DELETE FROM dbo.Person WHERE first_name = @fname AND last_name = @lname;";
            var parameters = isJobSpecified ?
                new Dictionary<string, object>()
                {
                    {"@fname", person.FirstName},
                    {"@lname", person.LastName},
                    {"@jobfk", person.JobFK}
                }
                :
                new Dictionary<string, object>()
                {
                    {"@fname", person.FirstName},
                    {"@lname", person.LastName}
                };

            return await wrapper.ExeNonQuery(query, parameters);
        }

        public async Task<bool> UpdateJob (Person person, string currentJob = null)
        {
           string query = currentJob == null ? "UPDATE dbo.Person SET job_fk = @newFK WHERE first_name = @fname AND last_name = @lname;" :
                                               "UPDATE dbo.Person SET job_fk = @newFK WHERE first_name = @fname AND last_name = @lname AND job_fk = @currentJob;";
           
           int? currentJobID = currentJob != null ? await (GetJobID(currentJob) ?? null) : null;
           
           var parameters = currentJobID == null ?
               new Dictionary<string, object>()
               {
                    {"@newFK", person.JobFK},
                    {"@fname", person.FirstName},
                    {"@lname", person.LastName}
               } 
               :
               new Dictionary<string, object>()
               {
                    {"@newFK", person.JobFK},
                    {"@fname", person.FirstName},
                    {"@lname", person.LastName},           
                    {"@currentJob", currentJobID.Value}
               };

           return await wrapper.ExeNonQuery(query, parameters);
        }
    }
}
