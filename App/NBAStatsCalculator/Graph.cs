using System.Collections.Generic;
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
        private FlowLayoutPanel globalDaysFlowLayoutPanel = new FlowLayoutPanel();
        private string[] daysNameArray = { "lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche" };
        private List<(double dayOfWeekNumber, string dayName)> days = new List<(double dayOfWeekNumber, string dayName)>();
        private List<(double dayOfWeekNumber, string dayName)> daysToShow = new List<(double dayOfWeekNumber, string dayName)>();

        public Graph(Form form, List<Team> teams, ScottPlot.WinForms.FormsPlot graph, FlowLayoutPanel layoutPanel, FlowLayoutPanel daysLayoutPanel)
        {
            this.globalForm = form;
            this.globalTeams = teams;
            this.globalGraph = graph;
            //Création du flowLayoutPanel
            this.globalFlowLayoutPanel = layoutPanel;
            this.globalDaysFlowLayoutPanel = daysLayoutPanel;
        }
        /// <summary>
        /// Crée un graph à partir d'une liste d'équipe
        /// </summary>
        /// <param name="teams">liste d'équipe</param>
        public void createGraph(List<Team> teams)
        {
            // Label du graph
            globalGraph.Plot.XLabel("Jour de la semaine");
            globalGraph.Plot.YLabel("Nombre de points");
            //Crée une ligne pour chaque équipe
            teams.ForEach(t =>
            {
                t.teamScores = t.teamScores.OrderBy(ts => ts.numberOfDay).ToList();
                createScatter(t.teamScores.Select(ts => (double)ts.numberOfDay).ToArray(), t.teamScores.Select(ts => (double)ts.score).ToArray(), t.nameOfTeam);
            });
            // Crée la check box permettant de désafficher toutes les équipes
            CreateDisableAllTeamsCheckBox();
            // Rafraîchissement et ajout au form
            globalGraph.Refresh();
            globalForm.Controls.Add(globalGraph);
        }
        /// <summary>
        /// Crée une ligne dans le graph pour avec les information d'une équipe
        /// </summary>
        /// <param name="dayOfWeek">Numéros du jour de la semaine</param>
        /// <param name="nbpointArray">Moyenne de point de l'équipe en fonction du jour</param>
        /// <param name="nameOfTeam">Nom de l'équipe</param>
        private void createScatter( double[] dayOfWeek, double[] nbpointArray, string nameOfTeam)
        {
            dayOfWeek.ToList().ForEach(d =>
            {
                days.Add((d, daysNameArray[ (int)d - 1]));
            });
            List<double> pointsOfTeam = new List<double>();
            for(int i = 0; i< days.Count; i++)
            {
                pointsOfTeam.Add(nbpointArray[(int)days[i].dayOfWeekNumber -1]);
            }
            var scatt = globalGraph.Plot.Add.Scatter(days.Select(d => d.dayOfWeekNumber).ToArray(), pointsOfTeam.ToArray());
            scatt.LegendText = nameOfTeam;
            CreateTeamCheckBoxes(nameOfTeam);
            globalGraph.Plot.Axes.Bottom.SetTicks(days.Select(d => d.dayOfWeekNumber).ToArray(), days.Select(d => d.dayName).ToArray());
        }
        /// <summary>
        /// Crée une checkBox pour une équipe
        /// </summary>
        /// <param name="nameOfScatter">Nom de la ligne du graph</param>
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
        /// <summary>
        /// Crée le bouton permettant de tout supprimer
        /// </summary>
        private void CreateDisableAllTeamsCheckBox()
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = "ToutSupprimer";
            checkBox.Text = "Tout Supprimer";
            checkBox.AutoSize = true;

            checkBox.CheckedChanged += new EventHandler(Disable_All_Teams);

            globalFlowLayoutPanel.Controls.Add(checkBox);
        }
        /// <summary>
        /// Affiche/Désaffiche une équipe
        /// </summary>
        /// <param name="sender">CheckBox</param>
        /// <param name="e">Evenement (Changement d'états de la checkBox)</param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {   
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
        /// <summary>
        /// Désaffiche toutes les équipes du graph 
        /// </summary>
        /// <param name="sender">CheckBox</param>
        /// <param name="e">Evenement (Changement d'états de la checkBox)</param>
        private void Disable_All_Teams(object sender, EventArgs e)
        {
            // Cast les objets control du flow layout panel en IEnnumerable CheckBox afin de pouvoir utiliser LinQ  (merci ChatGpt)
            globalFlowLayoutPanel.Controls.Cast<CheckBox>().ToList().ForEach(c =>
            {
                if (c.Name != "ToutSupprimer")
                {
                    c.Checked = ! c.Checked;
                } 
            });
        }
        public void CreateDaysCheckBox()
        {
            daysNameArray.ToList().ForEach(d =>
            {
                CheckBox dayCheckBox = new CheckBox();
                dayCheckBox.Name = d;
                dayCheckBox.Text = d;
                dayCheckBox.AutoSize = true;
                dayCheckBox.Checked = true;
                dayCheckBox.CheckedChanged += new EventHandler(DaysCheckBox_CheckedChanged);

                globalDaysFlowLayoutPanel.Controls.Add(dayCheckBox);
            });
        }
        private void DaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            daysToShow.Clear();
            CheckBox changedCheckBox = sender as CheckBox;
            globalGraph.Plot.GetPlottables().ToList().ForEach(s =>
            {
                if(s is ScottPlot.Plottables.Scatter scatter)
                {
                    List<ScottPlot.Coordinates> scatterPoints = scatter.Data.GetScatterPoints().ToList();
                    scatterPoints.ForEach(sp =>
                    {
                       if(sp.X == days.Select(d => d.dayOfWeekNumber).SingleOrDefault() && !changedCheckBox.Checked)
                       {
                            globalGraph.Plot.Clear();
                            days.ForEach(d =>
                            {
                                if (sp.X != d.dayOfWeekNumber)
                                {
                                    daysToShow.Add(d);
                                }
                            });
                       }
                        else
                        {
                            globalGraph.Plot.Clear();
                            days.ForEach(d =>
                            {
                                if(d.dayName == changedCheckBox.Text)
                                    daysToShow.Add(d);
                            });
                        }
                    });
                }
            });
            
        }



    }
}
