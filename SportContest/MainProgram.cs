using System;
using System.Collections;
using System.Collections.Generic;

namespace SportContest
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            PlayerPosition[] football_positions = 
            {
                new PlayerPosition("Centre-back"),
                new PlayerPosition("Sweeper"),
                new PlayerPosition("Full-back"),
                new PlayerPosition("Wing-back"),
                new PlayerPosition("Centre midfield"),
                new PlayerPosition("Defensive midfield"),
                new PlayerPosition("Attacking midfield"),
                new PlayerPosition("Wide midfield"),
                new PlayerPosition("Centre forward"),
                new PlayerPosition("Second strike"),
                new PlayerPosition("Goalkeeper"),
            };

            Sport football = new Sport("Football", 11, new List<PlayerPosition>(football_positions));
            
            foreach (PlayerPosition pos in football.Positions)
            {
                Console.WriteLine(pos.Name);
            }

            Team Osijek = new Team("NK Osijek", new Coach("xxx", "yyy", 40), football);

            Player Borna = new Player("Borna", "Gajić", 20, football, football_positions[0]);
            
            Osijek.AddPlayer(Borna);

            //Player TEST = new Player("B", "G", 20, football, football_positions[0]);
            //Osijek.AddPlayer(TEST);

            foreach (var x in Osijek.PlayerList)
            {
                Console.WriteLine(x.FirstName + " " + x.LastName);
            }
        }
    }
}
