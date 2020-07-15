using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Webapi.Models
{
	public class Job
	{
		public int JobID { get; }
		public int Salary { get; set; }

		public string Name { get; set; }

		public Job () {}
		public Job (int id, int sal, string name)
		{
			JobID = id;
			Salary = sal;
			Name = name;
		}
	}
}