using System.Diagnostics;

namespace NBAStatsCalculator
{
    public class Graph
    {
        private ScottPlot.WinForms.FormsPlot globalGraph = new ScottPlot.WinForms.FormsPlot();
        private List<Team> globalTeams = new List<Team>();
        private Form globalForm = new Form();
        private FlowLayoutPanel globalFlowLayoutPanel = new FlowLayoutPanel();
        private bool hasGraphChanged = false;


        public Graph(Form form, List<Team> teams, ScottPlot.WinForms.FormsPlot graph, FlowLayoutPanel layoutPanel)
        {
            this.globalForm = form;
            this.globalTeams = teams;
            this.globalGraph = graph;
            //Création du flowLayoutPanel
            this.globalFlowLayoutPanel = layoutPanel;
        }
        public double[] putListValueIntoArray(List<double> liste)
        {
            double[] doubleArray = new double[liste.Count];
            for (int i = 0; i < liste.Count; i++)
            {
                doubleArray[i] = liste[i];
            }
            return doubleArray;
        }
        public void createGraph(List<Team> teams)
        {
            // Label du graph
            globalGraph.Plot.XLabel("Jour de la semaine");
            globalGraph.Plot.YLabel("Nombre de points");

            teams.ForEach(t =>
            {
                t.teamScores = t.teamScores.OrderBy(ts => ts.numberOfDay).ToList();
                createScatter(t.teamScores.Select(ts => (double)ts.numberOfDay).ToArray(), t.teamScores.Select(ts => (double)ts.score).ToArray(), t.nameOfTeam);
            });
            CreateDisableAllTeamsCheckBox();
            // Rafraîchissement et ajout au form
            globalGraph.Refresh();
            globalForm.Controls.Add(globalGraph);
        }
        private void createScatter(double[] dayOfWeek, double[] nbpointArray, string nameOfTeam)
        {
            string[] days = { "lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche" };
            var scatt = globalGraph.Plot.Add.Scatter(dayOfWeek, nbpointArray);
            scatt.LegendText = nameOfTeam;
            CreateTeamCheckBoxes(nameOfTeam);
            globalGraph.Plot.Axes.Bottom.SetTicks(dayOfWeek, days);
        }
        private void CreateTeamCheckBoxes(string nameOfScatter)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = nameOfScatter;
            checkBox.Text = nameOfScatter;
            checkBox.AutoSize = true;
            checkBox.Checked = true;
            checkBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            
            globalFlowLayoutPanel.Controls.Add(checkBox);
        }
        private void CreateDisableAllTeamsCheckBox()
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = "ToutSupprimer";
            checkBox.Text = "Tout Supprimer";
            checkBox.AutoSize = true;

            checkBox.CheckedChanged += new EventHandler(Disable_All_Teams);

            globalFlowLayoutPanel.Controls.Add(checkBox);
        }
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Convertir le sender en Button
            CheckBox changedCheckBox = sender as CheckBox;
            globalGraph.Plot.GetPlottables().ToList().ForEach(scatter =>
            {
                if (scatter.LegendItems.Select(s => s.LabelText).FirstOrDefault().ToString() == changedCheckBox.Text)
                {
                    scatter.IsVisible = !scatter.IsVisible;
                }
                globalGraph.Refresh();
            });
        }
        //TODO: Trouver un moyen de rendre le programme moins lent
        private void Disable_All_Teams(object sender, EventArgs e)
        {
            globalFlowLayoutPanel.Controls.Cast<CheckBox>().ToList().ForEach(c =>
            {
                if (c.Name != "ToutSupprimer")
                {
                    c.Checked = false;
                } 
            });
        }



    }
}
