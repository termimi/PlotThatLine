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
        private string filePath = @"";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainGraph_Load(object sender, EventArgs e)
        {

        }

        private void openFileControlButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Fichiers JSON (*.json)|*.json";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Récupérer le chemin du fichier sélectionné
                filePath = openFileDialog.FileName;
                loadGraphic();
            }
        }
        private void loadGraphic()
        {
            Graph graph1 = new Graph(this, listOfTeams, this.mainGraph, this.mainLayoutPanel, this.daysFlowLayoutPanel);
            DataSelection data = new DataSelection("", "", "", 0, "", 0);
            data.loadFile(filePath);
            listOfTeams = data.GetTeamStructure();
            listOfTeams = data.GetAverageOfAllTeamScore(listOfTeams);
            graph1.createGraph(listOfTeams);
            graph1.CreateDaysCheckBox();
            this.mainGraph.Plot.Axes.AutoScale();
            this.mainGraph.Refresh();
        }
    }
}
