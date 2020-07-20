using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ProjectExample.Webapi.Controllers;
using ProjectExample.Service;
using ProjectExample.Service.Common;
using ProjectExample.Model;
using ProjectExample.Model.Common;
using ProjectExample.Webapi;
using Autofac;

namespace ProjectExample.Webapi.Controllers
{
    public class ExampleController : ApiController
    {   
        private readonly IService service;

        private readonly Mapper mapper = new Mapper(Webapi.WebApiApplication.config);

        public ExampleController (IService service) => this.service = service;
        
        [Route("api/Get/PersonList")]
        public async Task<HttpResponseMessage> GetPersonList ()
        {
            var result = await service.GetPersonList();

            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/Person")]
        public async Task<HttpResponseMessage> GetPerson (PersonRestModel personRest)
        {
            Person person = mapper.Map<Person>(personRest);

            var result = await service.GetPerson(person);
           
            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("api/Get/PersonAndJobs")]
        public async Task<HttpResponseMessage> GetPersonJobDetails (int? id = null)
        {   
            var result = await service.GetPersonJobDetails(id);
            
            return result is null ? Request.CreateResponse(HttpStatusCode.NoContent, result) : Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/Insert/Construct/Person")]
        public async Task<HttpResponseMessage> InsertPerson (PersonRestModel personRest)
        {   
            Person person = mapper.Map<Person>(personRest);
            
            bool result = personRest.JobName == null ? false : await service.InsertPerson(person, personRest.JobName);

            return result == true ? Request.CreateResponse(HttpStatusCode.OK, "Successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpDelete]
        [Route("api/Delete/Person")]
        public async Task<HttpResponseMessage> DeletePerson (PersonRestModel personRest)
        {
            Person person = mapper.Map<Person>(personRest);

            bool result = personRest.JobName == null ? await service.DeletePerson(person) : await service.DeletePerson(person, personRest.JobName);

            return result == true ? Request.CreateResponse(HttpStatusCode.OK, "Successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        [HttpPut]
        [Route("api/Put/UpdateJob")]
        public async Task<HttpResponseMessage> UpdateJob (PersonRestModel personRest, string newJob)
        {
           Person person = mapper.Map<Person>(personRest);

           var result = await service.UpdateJob(person, newJob, personRest.JobName);
            
           return result == true ? Request.CreateResponse(HttpStatusCode.OK, "Successful.") : Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
    }

    public class PersonRestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobName { get; set; }
        public PersonRestModel () {}
        public PersonRestModel (string fname, string lname)
        {
            FirstName = fname;
            LastName = lname;
        }
    }
}