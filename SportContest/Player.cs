using System;

namespace SportContest
{
    public class Player : IPerson
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; set; }

        public Sport PlayerSport { get; }
        public PlayerPosition PlayerPosition { get; set; }
        
        public Player (string firstName, string lastName, int age, Sport sport, PlayerPosition pos)
        {
            this.FirstName = firstName;
            this.Age = age;
            this.LastName = lastName;
            
            if (sport.Positions.Contains(pos))
            {
                PlayerPosition = pos;
                PlayerSport = sport;
            }
            else throw new ArgumentException("Player position is not part of that sport.");
        }

        public void Deconstruct (out string fname, out string lname, out int age)
        {
            fname = FirstName;
            lname = LastName;
            age = Age;
        }
    }
}