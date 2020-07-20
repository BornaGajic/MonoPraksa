using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExample.Service.Common;
using ProjectExample.Model;
using ProjectExample.Model.Common;
using ProjectExample.Repository;
using ProjectExample.Repository.Common;

namespace ProjectExample.Service
{
    public class Service : IService
    {
        private static int nextID = 6;
        private readonly IRepository repo;

        public Service (IRepository repo) => this.repo = repo;

        public async Task<List<Person>> GetPersonList () => await repo.GetPersonList();
        public async Task<List<Person>> GetPerson (Person person) => await repo.GetPerson(person);
        public async Task<List<List<object>>> GetPersonJobDetails (int? id = null) => await repo.GetPersonJobDetails(id);

        public async Task<bool> InsertPerson (Person person, string jobName)
        {
            int? jobFK = null;
            try
            {
                jobFK = await repo.GetJobID(jobName) ?? throw new Exception("Invalid job.");
            }
            catch
            {
                return false;
            }
            
            person.PersonID = nextID++;
            person.JobFK = jobFK.Value;

            return await repo.InsertPerson(person);
        }

        public async Task<bool> DeletePerson (Person person, string jobName = null)
        {
            if (jobName == null)
            {
                return await repo.DeletePerson(person);
            }

            int? jobFK = null;
            try
            {
                jobFK = await repo.GetJobID(jobName) ?? throw new Exception("Invalid job.");
            }
            catch
            {
                return false;
            }

            person.JobFK = jobFK.Value;

            return await repo.DeletePerson(person, true);
        }

        public async Task<bool> UpdateJob (Person person, string newJob, string currentJob)
        {
           try 
           {
                person.JobFK = await repo.GetJobID(newJob) ?? throw new ArgumentException("Invalid new job.");
           }
           catch (Exception _)
           {
                return false;
           }
           
           return await repo.UpdateJob(person, currentJob);
        }
    }
}
