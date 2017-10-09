using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootHubDb;

namespace FootHubDbTest
{
    [TestClass]
    public class FootWebClientTest
    {
        [TestMethod]
        public void TestGetLeagueTable()
        {
            FootWebClient client = new FootWebClient();
            Standing[] table = client.GetLeagueTable(445);
            Assert.AreEqual("Manchester City FC", table[0].TeamName);
        }
    }
}
