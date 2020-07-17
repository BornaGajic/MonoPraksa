using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExample.Model;

namespace ProjectExample.Service.Common
{
	public interface IService
	{
		Task<List<Person>> GetPersonList ();
		Task<List<Person>> GetPerson (Person person);
		Task<List<List<object>>> GetPersonJobDetails (int? id = null);
		Task<bool> InsertPerson (Person person, string jobName);
		Task<bool> DeletePerson (Person person, string jobName = null);
		Task<bool> UpdateJob (Person person, string newJob, string currentJob);
	}
}
