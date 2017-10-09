using System;
using System.Net;
using  Newtonsoft.Json;

class Href {
    public string href;
}

class Links{
    public Href self;
    public Href soccerseason;
}

class LeagueTable {
    public Links _links;
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
        Console.WriteLine(table._links.self.href);
        foreach(Standing l in table.standing) {
            Console.WriteLine(l.teamName);
        }
   }
}
