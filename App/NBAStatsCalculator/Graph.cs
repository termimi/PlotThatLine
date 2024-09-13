﻿using ScottPlot.Colormaps;
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
        public int graWidth = 500;
        public int graHeight = 250;
        public int graLeft = 100;
        public int graTop = 100;
        public int graPosX;
        public int graPosY;
        public string[] days = {"lundi", "mardi", "mercredi", "jeudi", "Vendredi", "Samedi", "Dimanche"};
        public Graph(string _name)
        {
            this.name = _name;
        }
        public void createGraph(Form form, double[] nbPoints, double[] dayOfWeek)
        {
            // Création du graph
            ScottPlot.WinForms.FormsPlot graph = new ScottPlot.WinForms.FormsPlot();

            // Taille du graph
            graph.Width = this.graWidth;
            graph.Height = this.graHeight;

            // Position du graph
            graph.Left = this.graLeft;
            graph.Top = this.graTop;
            graPosX = graph.Left;
            graPosY = graph.Top;

            // Label du graph
            graph.Plot.XLabel("Jour de la semaine");
            graph.Plot.YLabel("Nombre de points");

            // Ajout de la ligne (en utilisant des index pour dayOfWeek)
            graph.Plot.Add.Scatter(dayOfWeek, nbPoints);
            graph.Plot.Axes.Bottom.SetTicks(dayOfWeek, days);

            // Rafraîchissement et ajout au form
            graph.Refresh();
            form.Controls.Add(graph);
        }


    }
}
