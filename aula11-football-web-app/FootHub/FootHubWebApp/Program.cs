using FootHubDb;
using System;

namespace FootHubWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FootWebClient client = new FootWebClient();
            Standing[] table = client.GetLeagueTable(445);
            foreach (Standing l in table)
            {
                Console.WriteLine(l.TeamName);
            }
        }
    }
}
