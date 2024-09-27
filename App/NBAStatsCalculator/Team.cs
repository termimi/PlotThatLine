using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NBAStatsCalculator
{
    public class Team
    {
        public string nameOfTeam;
        public List<GamesResult> teamScores;

        public Team(string teamName,List<GamesResult> scoresOfTeam)
        {
            nameOfTeam = teamName;
            teamScores = scoresOfTeam;
        }
        
    }
    
}
