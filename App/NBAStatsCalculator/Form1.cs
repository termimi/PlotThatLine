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
            
            Graph graph1 = new Graph(this);
            DataSelection data = new DataSelection("", "", "", 0, "", 0);
            data.loadFile();
            listOfTeams = data.GetTeamStructure();
            listOfTeams = data.GetAverageOfAllTeamScore(listOfTeams);
            AddListOfScore();
            graph1.createGraph(this,listOfTeams);
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
            listeScore.Add(nbPoints);
            listeScore.Add(nbPoints);
        }
        

    }
}
