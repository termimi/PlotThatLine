using System.Diagnostics;

namespace NBAStatsCalculator
{
    public partial class Form1 : Form
    {
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
            graph1.initializeGraphBasics(this, listOfTeams);
            graph1.createGraph(listOfTeams);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        
    }
}
