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
        
        public DateTime dateTest = new DateTime(2024, 9, 6);
        public Form1()
        {
            InitializeComponent();
            Graph graph1 = new Graph();
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
            Team team1 = new Team("team1", nbPoints);
            Team team2 = new Team("team2", nbPoints2);
            listeScore.Add(team1.teamScores);
            listeScore.Add(team2.teamScores);
        }

    }
}
