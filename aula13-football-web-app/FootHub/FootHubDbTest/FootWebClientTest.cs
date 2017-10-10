using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootHubDb;

namespace FootHubDbTest
{
    [TestClass]
    public class FootWebClientTest
    {
        [TestMethod]
        public void TestGetLeagues()
        {
            FootWebClient client = new FootWebClient();
            League[] leagues = client.GetLeagues();
            Assert.AreEqual(17, leagues.Length);
            Assert.AreEqual("Campeonato Brasileiro da Série A", leagues[0].Caption);
            Assert.AreEqual("Premier League 2017/18", leagues[1].Caption);
        }

        [TestMethod]
        public void TestGetLeagueTable()
        {
            FootWebClient client = new FootWebClient();
            Standing[] table = client.GetLeagueTable(445);
            Assert.AreEqual(20, table.Length);
        }
    }
}
