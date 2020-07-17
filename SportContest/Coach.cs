using System;

namespace SportContest
{
    public class Coach : IPerson
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; set; }

        public Coach (string fname, string lname, int age)
        {
            FirstName = fname;
            LastName = lname;
            Age = age;
        }
        public void Deconstruct (out string fname, out string lname, out int age)
        {
            fname = FirstName;
            lname = LastName;
            age = Age;
        }
    }
}