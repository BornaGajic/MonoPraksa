using System;
using System.Collections;
using System.Collections.Generic;

namespace SportContest
{
    public class Team
    {
        public string TeamName { get; }
        public Coach TeamCoach { get; }
        public Sport TeamSport { get; }
        public readonly List<Player> PlayerList;
        private (int win, int draw, int lose) wdlStats;

        public Team (string teamName, Coach coach, Sport teamSport)
        {
            this.TeamName = teamName;
            this.TeamCoach = coach;

            TeamSport = new Sport(teamSport);
            PlayerList = new List<Player>();
        }

        public Team (string teamName, Coach coach, Sport teamSport, int win, int draw, int lose)
        {
            this.TeamName = teamName;
            this.TeamCoach = coach;
            
            wdlStats = (win, draw, lose);

            TeamSport = new Sport(teamSport);
            PlayerList = new List<Player>();
        }

        public void AddPlayer (Player Player)
        {
            if (PlayerList.Count < TeamSport.LineupCount) 
            {
                bool isPositionTaken = PlayerList.Exists(x => x.PlayerPosition.Name == Player.PlayerPosition.Name);

                if (!isPositionTaken)
                    PlayerList.Add(Player);
                else 
                    throw new ArgumentException("Player position already taken");
            }   
        }

        public (int, int, int) TeamStats () => wdlStats;
    }
}