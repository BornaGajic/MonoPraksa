using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Webapi.Models
{
	 public class Person
     {
        public int PersonID { get; }
        public int JobFK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person () {}
        public Person (string fname, string lname, int person_id, int job_fk)
        {
            FirstName = fname;
            LastName = lname;
            PersonID = person_id;
            JobFK = job_fk;
        }
     }
}