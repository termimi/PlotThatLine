using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAStatsCalculator
{
    public class Graph
    {
        public string name;
        public int graWidth = 500;
        public int graHeight = 250;
        public int graLeft = 100;
        public int graTop = 100;
        public int graPosX;
        public int graPosY;
        public Graph(string _name)
        {
            this.name = _name;
        }
        public void createGraph(Form form, int[] nbPoints, double[] dayOfWeek)
        {
            // création du graph
            ScottPlot.WinForms.FormsPlot graph = new ScottPlot.WinForms.FormsPlot();
            // taille du graph
            graph.Width = this.graWidth;
            graph.Height = this.graHeight;

            // position du graph
            graph.Left = this.graLeft;
            graph.Top = this.graTop;
            graPosX = graph.Left;
            graPosY = graph.Top;
            // label du graph
            graph.Plot.XLabel("jour de la semaine");
            graph.Plot.YLabel("Nombre de points");

            // ajout de la ligne
            graph.Plot.Add.Scatter(dayOfWeek, nbPoints);
            graph.Refresh();
            form.Controls.Add(graph);
        }

    }
}
