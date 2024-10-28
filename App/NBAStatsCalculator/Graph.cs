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
        private string[] daysNameArray = { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };
        // Liste contenant les 7 jours de la semaine ainsi que leur identifiant
        private List<(double dayOfWeekNumber, string dayName)> days = new List<(double dayOfWeekNumber, string dayName)>();
        // Liste des jour a afficher dans le graph et leur identifiant
        private List<(double dayOfWeekNumber, string dayName)> daysToShow = new List<(double dayOfWeekNumber, string dayName)>();

        public Graph(Form form, List<Team> teams, ScottPlot.WinForms.FormsPlot graph, FlowLayoutPanel layoutPanel, FlowLayoutPanel daysLayoutPanel)
        {
            this.globalForm = form;
            this.globalTeams = teams;
            this.globalGraph = graph;
            //Création du flowLayoutPanel
            this.globalFlowLayoutPanel = layoutPanel;
            this.globalDaysFlowLayoutPanel = daysLayoutPanel;
            // Ajout des jours et leurs identifiant
            days.Add((1, "lundi"));
            days.Add((2, "mardi"));
            days.Add((3, "mercredi"));
            days.Add((4, "jeudi"));
            days.Add((5, "vendredi"));
            days.Add((6, "samedi"));
            days.Add((7, "dimanche"));
            daysToShow = days;
            // Crée la check box permettant de désafficher toutes les équipes
            CreateDisableAllTeamsCheckBox();
        }
        /// <summary>
        /// Crée un graph à partir d'une liste d'équipe
        /// </summary>
        /// <param name="teams">liste d'équipe</param>
        public void createGraph(List<Team> teams)
        {
            double i = 0;
            this.globalTeams = teams;
            // Label du graph
            globalGraph.Plot.XLabel("Jour de la semaine");
            globalGraph.Plot.YLabel("Nombre de points");
            //Crée une ligne pour chaque équipe
            if(daysToShow.Count >=1)
            {
                teams.ForEach(t =>
                {
                    t.teamScores = t.teamScores.OrderBy(ts => ts.numberOfDay).ToList();
                    createPlot(t.teamScores.Where(ts => daysToShow.Select(d => (double)d.dayOfWeekNumber).Contains(ts.numberOfDay)).Select(ts => (double)ts.numberOfDay).ToArray(), t.teamScores.Where(ts => daysToShow.Select(d => (double)d.dayOfWeekNumber).Contains(ts.numberOfDay)).Select(ts => (double)ts.score).ToArray(), t.nameOfTeam, i);
                    i++;
                });
            }
            else
            {
                return;
            }
            i = 0;
            // Rafraîchissement et ajout au form
            globalGraph.Refresh();
            globalForm.Controls.Add(globalGraph);
            //TODO: Ne faire appel a cette methode que si nécessaire (uniquement si les il n'y pas 7 jour à afficher et que certaines équipe sont déchoché)
            UpdatesTeamsVisibility();
        }
        /// <summary>
        /// Crée une ligne dans le graph pour avec les information d'une équipe
        /// </summary>
        /// <param name="dayOfWeek">Numéros du jour de la semaine</param>
        /// <param name="nbpointArray">Moyenne de point de l'équipe en fonction du jour</param>
        /// <param name="nameOfTeam">Nom de l'équipe</param>
        private void createPlot(double[] dayOfWeek, double[] nbpointArray, string nameOfTeam,double indexOfTeam)
        {
            if(dayOfWeek.Length > 1)
            {
                var scatt = globalGraph.Plot.Add.Scatter(dayOfWeek, nbpointArray);
                scatt.LegendText = nameOfTeam;
            }
            else
            {
                //TODO:Changer les labels de l'axe X pour inclure le nom des équipes
                var coloumn = globalGraph.Plot.Add.Bar(indexOfTeam, nbpointArray[0]);
                coloumn.LegendText = nameOfTeam;
            }
            // Evite la redondance des checkBox (31 = le nombre d'equipe + le bouton tout supprimer)
            if(globalFlowLayoutPanel.Controls.Count < 31)
                CreateTeamCheckBoxes(nameOfTeam);
            globalGraph.Plot.Axes.Bottom.SetTicks(dayOfWeek, daysToShow.Select(d => d.dayName).ToArray());
            globalGraph.Plot.Axes.AutoScale();
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
        /// TODO: Déplacer la methode afin de la créer dans la classe FORM
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
            CheckBox changedCheckBox = sender as CheckBox;
            globalGraph.Plot.GetPlottables().ToList().ForEach(scatter =>
            {
                if (scatter.LegendItems.Select(s => s.LabelText).FirstOrDefault().ToString() == changedCheckBox.Text)
                {
                    // Inverse la visibilité de la ligne 
                    scatter.IsVisible = !scatter.IsVisible;
                    globalGraph.Refresh();
                }
                
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
                    c.Checked = !c.Checked;
                }
            });
        }
        /// <summary>
        /// Crée les checkbox permettant de choisir les jours à afficher
        /// </summary>
        /// TODO: Déplacer la methode afin de la créer dans la classe FORM
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
            List<(double dayOfWeekNumber, string dayName)> daysChecked = new List<(double dayOfWeekNumber, string dayName)>();
            CheckBox changedCheckBox = sender as CheckBox;
            globalDaysFlowLayoutPanel.Controls.OfType<CheckBox>().ToList().ForEach(c =>
            {
                if (c.Checked)
                {
                    daysChecked.Add((days
                        .Where(d => d.dayName == c.Text)
                        .Select(d => d.dayOfWeekNumber).Single(), c.Text));
                }
            });
            // modifie la liste des jour à afficher
            daysToShow = daysChecked;
            // CRéation des nouvelles ligne
            globalGraph.Plot.Clear();
            createGraph(this.globalTeams);
        }
        /// <summary>
        /// Désaffiche les équipes décoché apprés avoir filtrer sur les jours
        /// </summary>
        private void UpdatesTeamsVisibility()
        {
            //TODO: Refactore pour que le code de cette methode et la methode CheckBox_CheckedChanged ne soit plus redondant
            //Liste des équipe à ne pas afficher
            globalFlowLayoutPanel.Controls.OfType<CheckBox>().ToList().Where(c => !c.Checked).ToList().ForEach(c =>
            {
                globalGraph.Plot.GetPlottables().ToList().ForEach(scatter =>
                {
                    if (scatter.LegendItems.Select(s => s.LabelText).FirstOrDefault().ToString() == c.Text)
                    {
                        scatter.IsVisible = !scatter.IsVisible;
                        globalGraph.Refresh();
                    }
                    
                });
            });
        }



    }
}
