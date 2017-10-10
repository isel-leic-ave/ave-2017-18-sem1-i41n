using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace FootHubDb
{
    public class FootWebClient
    {
        private readonly WebClient client = new WebClient() { Encoding = Encoding.UTF8 };

        public League[] GetLeagues() {
            string body = client.DownloadString("http://api.football-data.org/v1/soccerseasons/");
            League[] leagues = (League[])JsonConvert.DeserializeObject(body, typeof(League[]));
            return leagues;
        }

        public Standing[] GetLeagueTable(int leagueId)
        {
            string path = String.Format(
                "http://api.football-data.org/v1/soccerseasons/{0}/leagueTable",
                leagueId);
            string body = client.DownloadString(path);
            LeagueTable table= (LeagueTable)JsonConvert.DeserializeObject(body, typeof(LeagueTable));
            return table.Standing;
        }

        public Team GetTeam(int teamId) {
            // TO DO !!!!!
            throw new NotImplementedException();
        }
    }
}
