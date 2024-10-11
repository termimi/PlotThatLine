using ScottPlot.Colormaps;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using ScottPlot.Plottables;
using System.Diagnostics;

namespace NBAStatsCalculator
{
    public class Graph
    {
        public string name;
        public double graWidth ; // 500
        public double graHeight; //250
        public int graLeft = 100;
        public int graTop = 100;
        public int graPosX;
        public int graPosY;
        private ScottPlot.WinForms.FormsPlot graph = new ScottPlot.WinForms.FormsPlot();
        private List<Team> globalTeams = new List<Team>();
        private Form globalForm = new Form();
        private FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        private bool hasGraphChanged = false;


        public Graph(Form form)
        {
            this.graHeight = form.Height/1.5;
            this.graWidth = form.Width/1.5;
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
        private FlowLayoutPanel CreateFlowLayoutPanel(Form form)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            //flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Top = this.graTop;
            flowLayoutPanel.Left = this.graLeft + (int)graWidth;
            //flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.AutoScroll = true;
            form.Controls.Add(flowLayoutPanel);
            return flowLayoutPanel;
        }
        public void initializeGraphBasics(Form form, List<Team> teams)
        {
            globalForm = form;
            globalTeams = teams;
            //Création du flowLayoutPanel
            flowLayoutPanel = CreateFlowLayoutPanel(globalForm);
        }
        public void createGraph(List<Team>teams)
        {
            
            // Création du graph

            // Taille du graph
            graph.Width =  (int)this.graWidth;
            graph.Height = (int)this.graHeight;

            // Position du graph
            graph.Left = this.graLeft;
            graph.Top = this.graTop;
            graPosX = graph.Left;
            graPosY = graph.Top;

            // Label du graph
            graph.Plot.XLabel("Jour de la semaine");
            graph.Plot.YLabel("Nombre de points");

            teams.ForEach(t =>
            {
                t.teamScores = t.teamScores.OrderBy(ts => ts.numberOfDay).ToList();
                createScatter(graph, t.teamScores.Select(ts => (double)ts.numberOfDay).ToArray(),t.teamScores.Select(ts => (double)ts.score).ToArray(),t.nameOfTeam,flowLayoutPanel);
            });
     
            // Rafraîchissement et ajout au form
            graph.Refresh();
            globalForm.Controls.Add(graph);
        }
        private void createScatter(ScottPlot.WinForms.FormsPlot graph,double[] dayOfWeek, double[] nbpointArray, string nameOfTeam, FlowLayoutPanel flowLayoutPanel)
        {
            string[] days = { "lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche" };
            var scatt = graph.Plot.Add.Scatter(dayOfWeek, nbpointArray);
            scatt.LegendText = nameOfTeam; 
            CreateButton(nameOfTeam, flowLayoutPanel);
            graph.Plot.Axes.Bottom.SetTicks(dayOfWeek, days);
        }
        private void CreateButton(string nameOfScatter, FlowLayoutPanel flowLayoutPanel)
        {
            Button button = new Button();
            button.Text = nameOfScatter;
            button.Size = new Size(50, 50);
            button.Click += new EventHandler(Button_Click);

            flowLayoutPanel.Controls.Add(button);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Convertir le sender en Button
            Button clickedButton = sender as Button;
            graph.Plot.GetPlottables().ToList().ForEach(scatter =>
            {
                scatter.IsVisible = false;
                if(scatter.LegendItems.Select(s => s.LabelText).FirstOrDefault().ToString() == clickedButton.Text)
                {
                    scatter.IsVisible = !scatter.IsVisible;
                }
                graph.Refresh();
            });
        }



    }
}
