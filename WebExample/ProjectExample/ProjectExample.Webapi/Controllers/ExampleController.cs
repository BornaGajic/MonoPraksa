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

namespace ProjectExample.Webapi.Controllers
{
    public class ExampleController : ApiController
    {   
        [Route("api/Get/PersonList")]
        public HttpResponseMessage GetPersonList ()
        {
            string selectAll = "SELECT * FROM dbo.Person";

            List<Person> result = new List<Person>();
            var queryResult = MyDatabase.ExecuteQuery(selectAll);

            foreach (List<object> row in queryResult)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/Person")]
        public HttpResponseMessage GetPerson (string personName)
        {
            string query = "SELECT * FROM dbo.Person WHERE first_name = @personName;";

            var resultQuery = MyDatabase.ExecuteQuery(query, new Dictionary<string, object>(){{"@personName", personName}});
            var result = new List<Person>();

            foreach (var row in resultQuery)
            {
                result.Add(new Person(row[1].ToString(), row[2].ToString(), (int)row[0], (int)row[3]));
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/PersonAndJobs")]
        public HttpResponseMessage GetPersonJobDetails (int? id = null)
        {   
            string query = id is null ? "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id;" : 
                                        "SELECT * FROM dbo.Person AS p JOIN dbo.Job AS j ON p.job_fk = j.job_id WHERE p.person_id = @ID;";

            var parameters = id is null ? null : new Dictionary<string, object>(){{"@ID", id}};

            var result = MyDatabase.ExecuteQuery(query, parameters);
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/Insert/Construct/Person")]
        public HttpResponseMessage InsertPerson (string fname, string lname, int id, int jobid)
        {
            string query = "INSERT INTO dbo.Person VALUES (@id, @fname, @lname, @jobid)";

            var parameters = new Dictionary<string, object>()
            {
                {"@id", id},
                {"@fname", fname},
                {"@lname", lname},
                {"@jobid", jobid}
            };

            string result = MyDatabase.ExeNonQuery(query, parameters);

            return result is null ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpDelete]
        [Route("api/Delete/PersonWithID")]
        public HttpResponseMessage DeletePerson (int ID)
        {
            string query = "DELETE FROM dbo.Person WHERE person_id = @ID;";

            string result = MyDatabase.ExeNonQuery(query, new Dictionary<string, object>(){{"@ID", ID}});

            return result is null ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpPut]
        [Route("api/Put/UpdateJobFK")]
        public IHttpActionResult UpdateJob (int ID, int newFK)
        {
           string query = "UPDATE dbo.Person SET job_fk = @newFK WHERE person_id = @ID;";
           
           var parameters = new Dictionary<string, object>()
           {
                {"@newFK", newFK},
                {"@ID", ID}
           };

           string result = MyDatabase.ExeNonQuery(query, parameters);
            
           return result is null ? new MyTextResult("Update successful.", Request) : new MyTextResult(result, Request);
        }
    }

    public class MyTextResult : IHttpActionResult
    {
        string _value;
        HttpRequestMessage _request;

        public MyTextResult (string value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync (CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
                RequestMessage = _request
            };

            return Task.FromResult(response);
        }
    }
}
