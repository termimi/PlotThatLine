using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBAStatsCalculator;
using System.Windows.Forms;

namespace NBAStatsCalculator.Tests
{
    [TestClass()]
    public class GraphTests
    {
        [TestMethod()]
        public void createGraphTest()
        {
            ///Arange
            Form form = new Form();
            ScottPlot.WinForms.FormsPlot graphPlot = new ScottPlot.WinForms.FormsPlot();
            FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
            FlowLayoutPanel daysLayoutPanel = new FlowLayoutPanel();

            Team team1 = new Team("Equipe1");
            Team team2 = new Team("Equipe2");
            team1.teamScores = new List<(double score, double numberOfDay)>
            {
                (89,1),(100,2)
            };
            team2.teamScores = new List<(double score, double numberOfDay)>
            {
                (110,1),(95,2)
            };
            List<Team> teams = new List<Team>();
            Graph graph = new Graph(form, teams, graphPlot, layoutPanel, daysLayoutPanel);
            graph.daysToShow = new List<(double, string)>
            {
                (1, "lundi"), (2, "mardi")
            };
            ///Act
            teams.Add(team1);
            teams.Add(team2);
            graph.createGraph(teams);
            ///Assert
            Assert.IsTrue(graphPlot.Plot.GetPlottables().ToList().Count() > 0, "Le graphique est vide après l'appel de createGraph.");
        }

        [TestMethod()]
        public void createGraphTeamsFilterTest()
        {
            Form form = new Form();
            ScottPlot.WinForms.FormsPlot graphPlot = new ScottPlot.WinForms.FormsPlot();
            FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
            FlowLayoutPanel daysLayoutPanel = new FlowLayoutPanel();

            Team team1 = new Team("Equipe1");
            Team team2 = new Team("Equipe2");
            team1.teamScores = new List<(double score, double numberOfDay)>
            {
                (89,1),(100,2)
            };
            team2.teamScores = new List<(double score, double numberOfDay)>
            {
                (110,1),(95,2)
            };
            List<Team> teams = new List<Team>();
            Graph graph = new Graph(form, teams, graphPlot, layoutPanel, daysLayoutPanel);
            graph.daysToShow = new List<(double, string)>
            {
                (1, "lundi"), (2, "mardi")
            };
            ///Act
            teams.Add(team1);
            teams.Add(team2);
            graph.createGraph(teams);
            ///Assert
            Assert.IsTrue(graphPlot.Plot.GetPlottables().ToList().Count() == layoutPanel.Controls.OfType<CheckBox>()
                .ToList()
                .Where(c => c.Checked)
                .ToList()
                .Count(), "Le nombre de ligne du graph et le nombre de checkBox cochée n'est pas le même");
        }
    }
}