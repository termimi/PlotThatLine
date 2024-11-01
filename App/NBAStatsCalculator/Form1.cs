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
        public void loadGraphic()
        {
            //TODO: Refactor afin de ne plus avoir besoin de clear avant de créer le graph
            this.mainLayoutPanel.Controls.Clear();
            this.mainGraph.Plot.Clear();
            Graph graph1 = new Graph(this, listOfTeams, this.mainGraph, this.mainLayoutPanel, this.daysFlowLayoutPanel);
            DataSelection data = new DataSelection("", "", "", 0, "", 0);
            try
            {
                data.loadFile(filePath);
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Les données du fichier chargé ne sont pas au bon format, veuillez voir le fichier exemple sur répertoire GitHub (https://github.com/termimi/PlotThatLine/tree/main/DataSets) erreur: " + ex.Message, "Erreur de ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listOfTeams = data.GetTeamStructure();
            listOfTeams = data.GetAverageOfAllTeamScore(listOfTeams);
            graph1.createGraph(listOfTeams);
            if (daysFlowLayoutPanel.Controls.OfType<CheckBox>().ToList().Count() == 0)
                graph1.CreateDaysCheckBox();
            this.mainGraph.Refresh();
           

        }
    }
}
