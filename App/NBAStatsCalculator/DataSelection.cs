using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAStatsCalculator
{
    public  class DataSelection
    {
        public string dateOfGame;
        public string startOfGame;
        public string visitorTeam;
        public int visitorPoints;
        public string homeTeam;
        public int homePoints;

        public DataSelection()
        {
            
        }
        public void loadFile()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(appPath, "TeamData", "TeamBasicData.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
                dateOfGame = data.Date;
                startOfGame = data.Start;
                visitorTeam = data.Visitor;
                visitorPoints = data.VPTS;
                homeTeam = data.Home;
                homePoints = data.HPTS;
            }
        }

    }

}
