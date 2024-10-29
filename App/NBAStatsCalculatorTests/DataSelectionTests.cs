using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBAStatsCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAStatsCalculator.Tests
{
    [TestClass()]
    public class DataSelectionTests
    {
        [TestMethod()]
        public void loadFileTest()
        {
            ///Arrange
            DataSelection data = new DataSelection("", "", "", 0, "", 0);
            string filePath = @"C:\Users\BILEL\Documents\PlotThatLine\App\NBAStatsCalculator\teamData.json";
            ///Act
            data.loadFile(filePath);
            ///Assert
            Assert.IsInstanceOfType(data.homeTeam,typeof(string));
            Assert.IsInstanceOfType(data.visitorTeam, typeof(string));
            Assert.IsInstanceOfType(data.homePoints, typeof(int));
            Assert.IsInstanceOfType(data.visitorPoints, typeof(int));
            Assert.IsInstanceOfType(data.dateOfGame, typeof(string));
            Assert.IsTrue(data.list.Count > 0);
        }
    }
}