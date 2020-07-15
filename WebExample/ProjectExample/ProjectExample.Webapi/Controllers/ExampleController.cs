using System;
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
        List<Person> PersonList = new List<Person>()
        {
            new Person ("Borna", "Gajić", 20),
            new Person ("Fran", "Frankopan", 190),
            new Person ("Marko", "Marulić", 50)
        };

        [Route("api/Get/PersonList")]
        public IEnumerable<Person> GetPersonList () => PersonList.ToArray();

        [HttpGet]
        [Route("api/Get/Person")]
        public HttpResponseMessage GetPerson (string personName)
        {
            Person p = PersonList.Find(per => per.FirstName == personName);
            
            if (p is null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, personName);
            }
                

            return Request.CreateResponse(HttpStatusCode.OK, p);
        }

        [HttpPost]
        [Route("api/Insert/Construct/Person")]
        public void AddToPersonList (string fname, string lname, int age)
        {
            Person p = new Person(fname, lname, age);

            PersonList.Add(p);
        }

        [HttpPost]
        [Route("api/Insert/Person")]
        public void InsertPerson ([FromUri] Person p) => PersonList.Add(p);

        [HttpDelete]
        [Route("api/Delete/PersonWithAge")]
        public IHttpActionResult DeletePersonWithAge (int age)
        {
            if (PersonList.Exists(per => per.Age.Equals(age)))
            {
                PersonList.RemoveAll(per => per.Age.Equals(age));

                return new MyTextResult("Delete successful", Request);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("api/Put/UpdateAge")]
        public IHttpActionResult UpdateAge (string fname, string lname, int newAge)
        {
            var existingPersones = PersonList.Where(per => per.FirstName == fname && per.LastName == lname).ToList();

            if (existingPersones is null) return NotFound();

            existingPersones.ForEach(per => per.Age = newAge);

            return Ok(newAge);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public Person () {}
        public Person (string fname, string lname, int age)
        {
           FirstName = fname;
           LastName = lname;
           Age = age;
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
