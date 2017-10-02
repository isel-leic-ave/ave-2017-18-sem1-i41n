using System;
using System.Net;
using  Newtonsoft.Json;

    class LeagueTable {
        public string matchday;
        public Standing[] standing;
        public string leagueCaption;
    }

    class Standing {
        public string teamName;
        public int goals;
        public int playedGames;
    }

class App{
   static void Main() {
        WebClient client = new WebClient();
        string body = client.DownloadString("http://api.football-data.org/v1/soccerseasons/445/leagueTable");
        LeagueTable table = (LeagueTable) JsonConvert.DeserializeObject(body, typeof(LeagueTable));
        foreach(Standing l in table.standing) {
            Console.WriteLine(l.teamName);
        }
   }
}
