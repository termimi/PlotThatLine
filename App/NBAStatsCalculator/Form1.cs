using System.Diagnostics;

namespace NBAStatsCalculator
{
    public partial class Form1 : Form
    {
        public List<double> nbPoints = new List<double>
        {
            75, 
            85, 
            110,
            95, 
            98, 
            75, 
            83
        };
        public List<double> nbPoints2 = new List<double>
        {
            65,
            70,
            100,
            80,
            86,
            69,
            97
        };
        public List<Team> listOfTeams = new List<Team>();
        public List<List<double>> listeScore = new List<List<double>>();
        public List<string> teamNames = new List<string>();
        public List<Team> teams = new List<Team>();
        public DateTime dateTest = new DateTime(2024, 9, 6);
        public Form1()
        {
            InitializeComponent();
            Graph graph1 = new Graph();
            DataSelection data = new DataSelection("", "", "", 0, "", 0);
            data.loadFile();
            data.GetTeamStructure();
            AddListOfScore();
            graph1.createGraph(this,listeScore);
            int daysOfWeek = ConvertDateToDayOfWeekNumber(dateTest);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        public int ConvertDateToDayOfWeekNumber(DateTime dateOfDay)
        {
            int dayOfWeek = (int)dateOfDay.DayOfWeek;
            return dayOfWeek;

        }
        public void AddListOfScore()
        {
            List<(List<double>, double numberOfDay)> test = new List<(List<double>, double numberOfDay)> ();
            List<(List<double>, double numberOfDay)> test2 = new List<(List<double>, double numberOfDay)>();
            test.Add((nbPoints, 3));
            test2.Add((nbPoints2, 3));
            Team team1 = new Team("team1", test);
            Team team2 = new Team("team2", test2);
            listeScore.Add(nbPoints);
            listeScore.Add(nbPoints);
        }

    }
}
