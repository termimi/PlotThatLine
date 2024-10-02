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
        public List<DataSelection> list = new List<DataSelection>();

        public DataSelection(string _dateOfGame, string _startOfGame, string _visitorTeam, int _visitorPoints, string _homeTeam, int _homePoints)
        {
            dateOfGame = _dateOfGame;
            startOfGame = _startOfGame;
            visitorTeam = _visitorTeam;
            homeTeam = _homeTeam;
            homePoints = _homePoints;
            visitorPoints = _visitorPoints;
        }
        public void loadFile()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(appPath, "TeamData", "TeamData.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
                
                foreach (var item in data)
                {
                    DataSelection dataInList = new DataSelection((string)item.Date, (string)item.Start, (string)item.Visitor, (int)item.VPTS, (string)item.Home, (int)item.HPTS);
                    list.Add(dataInList);
                }
            }
        }

    }

}
