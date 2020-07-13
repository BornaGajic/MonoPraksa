using System;

namespace SportContest
{
    interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        int Age { get; set; }
        void Deconstruct (out string fname, out string lname, out int age);
    }
}