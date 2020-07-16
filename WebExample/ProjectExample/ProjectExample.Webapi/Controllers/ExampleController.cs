using System;
using System.Data;
using System.Data.SqlClient;
using ProjectExample.Service;
using ProjectExample.Model;
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
            Service.Service service = new Service.Service();

            var result = service.GetPersonList();
            
            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/Person")]
        public HttpResponseMessage GetPerson (PersonRestModel personRestM)
        {
            Service.Service service = new Service.Service();

            Person person = new Person()
            {
                FirstName = personRestM.FirstName,
                LastName = personRestM.LastName
            };

            var result = service.GetPerson(person);
           
            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/PersonAndJobs")]
        public HttpResponseMessage GetPersonJobDetails (int? id = null)
        {   
            Service.Service service = new Service.Service();
            
            var result = service.GetPersonJobDetails(id);
            
            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/Insert/Construct/Person")]
        public HttpResponseMessage InsertPerson (PersonRestModel personRest)
        {   
            Service.Service service = new Service.Service();

            Person person = null;
            try
            {
                person = new Person ()
                {
                    FirstName = personRest.FirstName,
                    LastName = personRest.LastName,
                    JobFK = GetJobID(personRest.JobName) ?? throw new Exception("Invalid job.")
                };            
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var result = service.InsertPerson(person);

            return result is null ? Request.CreateResponse(HttpStatusCode.OK, "Successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpDelete]
        [Route("api/Delete/Person")]
        public HttpResponseMessage DeletePerson (PersonRestModel personRest)
        {
            Service.Service service = new Service.Service();

            Person person = new Person()
            {
                FirstName = personRest.FirstName,
                LastName = personRest.LastName,
            };

            var result = service.DeletePerson(person);

            return result is null ? Request.CreateResponse(HttpStatusCode.OK, "Successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpPut]
        [Route("api/Put/UpdateJob")]
        public IHttpActionResult UpdateJob (PersonRestModel personRest, string newJob)
        {
           Service.Service service = new Service.Service();
           
           int newJobID = GetJobID(newJob) ?? throw new ArgumentException("Invalid new job.");

           Person person = new Person ()
           {
                FirstName = personRest.FirstName,
                LastName = personRest.LastName,
                JobFK = GetJobID(personRest.JobName) ?? throw new ArgumentException("Invalid current job.")
           };

           var result = service.UpdateJob(person, newJobID);
            
           return result is null ? new MyTextResult("Update successful.", Request) : new MyTextResult(result, Request);
        }

        [HttpGet]
        private int? GetJobID (string jobName)
        {
            Service.Service service = new Service.Service();
            
            return service.GetJobID(jobName); 
        }
    }

    public class PersonRestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobName { get; set; }

        public PersonRestModel () {}
        public PersonRestModel (string fname, string lname, string jobname)
        {
            FirstName = fname;
            LastName = lname;
            JobName = jobname;
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
