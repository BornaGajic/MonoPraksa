using System;
using System.Collections;
using System.Collections.Generic;

namespace SportContest
{
    public class Sport
    {
        public string SportName { get; }
        public int LineupCount { get; }
        public List<PlayerPosition> Positions;

        public Sport (string sportName, int lineupCount, List<PlayerPosition> positionList)
        {
            this.SportName = sportName;
            this.LineupCount = lineupCount;

            if (lineupCount == positionList.Count)
                Positions = new List<PlayerPosition>(positionList);
            else
                throw new ArgumentException("lineup count and count of positionList must be the same.");
        }
        public Sport (Sport copySport)
        {
            this.SportName = copySport.SportName;
            this.LineupCount = copySport.LineupCount;

            Positions = new List<PlayerPosition>(copySport.Positions);
        }
    }
}