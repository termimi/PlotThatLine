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
        
        public static List<string> getAllTeams()
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
    }
   
}
