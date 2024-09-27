using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAStatsCalculator
{
    public static class DataSelection
    {
        public static List<string> getAllTeamsNames()
        {
            // list contenant toutes les entrée de home même les doublons
            List<string> allHomeRecords = new List<string>();
            //List d'équipe
            List<string> nbaTeams = new List<string>();

            string appPath = AppDomain.CurrentDomain.BaseDirectory;

            string filePath = Path.Combine(appPath, "TeamData", "TeamBasicData.csv");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                while (csv.Read()) // Lire la ligne suivante
                {
                    var record = csv.GetRecord<dynamic>(); // Récupérer l'enregistrement
                    allHomeRecords.Add(record.Home); // Ajouter l'équipe à la liste
                }
            }
            nbaTeams = allHomeRecords.Distinct().ToList();
            return nbaTeams;
        }
        public static List<Team> getTeamsStats(List<string> listOfTeams)
        {
            var cryptoName = new Dictionary<string, double>()
            {
                { "Mon",1},
                { "Tue",2},
                { "Wed",3},
                { "Thu",4},
                { "Fri",5},
                { "Sat",6},
                { "Sun",7}
                
            };
            List<GamesResult> results = new List<GamesResult>();
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            List<Team> teams = new List<Team>();
            string filePath = Path.Combine(appPath, "TeamData", "TeamBasicData.csv");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                listOfTeams.ForEach(teamOfList =>
                {
                    List<double> scoreOfTeam = new List<double>();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<dynamic>();
                        if (record.Home == teamOfList)
                        {
                            scoreOfTeam.Add(Convert.ToDouble(record.HPTS));
                        }
                        if (record.Visitor == teamOfList)
                        {
                            scoreOfTeam.Add(Convert.ToDouble(record.VPTS));
                        }
                    }
                    Team team = new Team(teamOfList, results);
                    teams.Add(team);
                });
            }
            return teams;
        }
    }

}
