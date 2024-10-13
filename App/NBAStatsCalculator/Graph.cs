﻿namespace NBAStatsCalculator
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

            // Rafraîchissement et ajout au form
            globalGraph.Refresh();
            globalForm.Controls.Add(globalGraph);
        }
        private void createScatter(double[] dayOfWeek, double[] nbpointArray, string nameOfTeam)
        {
            string[] days = { "lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche" };
            var scatt = globalGraph.Plot.Add.Scatter(dayOfWeek, nbpointArray);
            scatt.LegendText = nameOfTeam;
            CreateButton(nameOfTeam);
            globalGraph.Plot.Axes.Bottom.SetTicks(dayOfWeek, days);
        }
        private void CreateButton(string nameOfScatter)
        {
            Button button = new Button();
            button.Text = nameOfScatter;
            button.Size = new Size(50, 50);
            button.Click += new EventHandler(Button_Click);

            globalFlowLayoutPanel.Controls.Add(button);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Convertir le sender en Button
            Button clickedButton = sender as Button;
            globalGraph.Plot.GetPlottables().ToList().ForEach(scatter =>
            {
                scatter.IsVisible = false;
                if (scatter.LegendItems.Select(s => s.LabelText).FirstOrDefault().ToString() == clickedButton.Text)
                {
                    scatter.IsVisible = !scatter.IsVisible;
                }
                globalGraph.Refresh();
            });
        }



    }
}
