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
		List<Person> GetPersonList ();
		List<Person> GetPerson (Person person);
		List<List<object>> GetPersonJobDetails (int? id = null);
		int? GetJobID (string jobName);
		string InsertPerson (Person person);
		string DeletePerson (Person person);
		string UpdateJob (Person person, int jobFK);
	}
}
