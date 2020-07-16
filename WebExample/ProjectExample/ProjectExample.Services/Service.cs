using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExample.Service.Common;
using ProjectExample.Model;
using ProjectExample.Repository;

namespace ProjectExample.Service
{
    public class Service : IService
    {
        private static int nextID = 6;
       
        public List<Person> GetPersonList () 
        { 
            Repository.Repository repo = new Repository.Repository();

            var result = repo.GetPersonList();

            return result;
        }
        public List<Person> GetPerson (Person person) 
        { 
            Repository.Repository repo = new Repository.Repository();

            var result = repo.GetPerson(person);

            return result;
        }

        public List<List<object>> GetPersonJobDetails (int? id = null)
        {
            Repository.Repository repo = new Repository.Repository();
            
            return repo.GetPersonJobDetails(id);
        }

        public string InsertPerson (Person person)
        {
            Repository.Repository repo = new Repository.Repository();
            
            person.PersonID = nextID++;

            return repo.InsertPerson(person);
        }

        public int? GetJobID (string jobName)
        {
            Repository.Repository repo = new Repository.Repository();

            return repo.GetJobID(jobName);
        }

        public string DeletePerson (Person person)
        {
            Repository.Repository repo = new Repository.Repository();

            string result = repo.DeletePerson(person);

            return result;
        }

        public string UpdateJob (Person person, int jobFK)
        {
           Repository.Repository repo = new Repository.Repository();

           string result = repo.UpdateJob(person, jobFK);

           return result;
        }
    }
}
