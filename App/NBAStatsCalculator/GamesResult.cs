using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAStatsCalculator
{
    public class GamesResult
    {
        public double dayOfGame;
        public List<double> gamesScores = new List<double>();
        public GamesResult(List<double> scores, double dayNumber) 
        { 
            dayOfGame = dayNumber;
            gamesScores = scores;
        }

    }
}
