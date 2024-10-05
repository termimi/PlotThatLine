using ScottPlot.Colormaps;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using ScottPlot.Plottables;

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
        public void createGraph(Form form, List<Team> teams)
        {
            // Création du graph
            ScottPlot.WinForms.FormsPlot graph = new ScottPlot.WinForms.FormsPlot();
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
                createScatter(graph, t.teamScores.Select(ts => (double)ts.numberOfDay).ToArray(),t.teamScores.Select(ts => (double)ts.score).ToArray(),t.nameOfTeam);
            });
     
            // Rafraîchissement et ajout au form
            graph.Refresh();
            form.Controls.Add(graph);
        }
        public void createScatter(ScottPlot.WinForms.FormsPlot graph,double[] dayOfWeek, double[] nbpointArray, string nameOfTeam)
        {
            string[] days = { "lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche" };
            var scatt = graph.Plot.Add.Scatter(dayOfWeek, nbpointArray);
            //scatt.LegendText = nameOfTeam;
            graph.Plot.Axes.Bottom.SetTicks(dayOfWeek, days);
        }


    }
}
