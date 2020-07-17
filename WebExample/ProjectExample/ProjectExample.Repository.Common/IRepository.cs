using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExample.Model;

namespace ProjectExample.Repository.Common
{
	public interface IRepository
	{
		Task<List<Person>> GetPersonList ();
		Task<List<Person>> GetPerson (Person person);
		Task<List<List<object>>> GetPersonJobDetails (int? id = null);
		Task<int?> GetJobID (string jobName);
		Task<bool> InsertPerson (Person person);
		Task<bool> DeletePerson (Person person, bool isJobSpecified = false);
		Task<bool> UpdateJob (Person person, string currentJob = null);
	}
}
