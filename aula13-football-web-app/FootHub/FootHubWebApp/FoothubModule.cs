using FootHubDb;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootHubWebApp
{
    public class FoothubModule:NancyModule
    {
        FootWebClient client = new FootWebClient();

        public FoothubModule()
        {
            Get["/leagues"] = _ =>
            {
                League[] leagues = client.GetLeagues();
                FootHubViewModel model = new FootHubViewModel(
                    "Football Leagues", LeaguesToHtml(leagues));
                return View["ViewTable", model];
            };
            Get["/leagues/{leagueId}"] = args =>
            {
                Standing[] standings = client.GetLeagueTable(args.leagueId);
                FootHubViewModel model = new FootHubViewModel(
                    "League Table", StandingsToHtml(standings));
                return View["ViewTable", model];
            };
            Get["/teams/{teamId}"] = args =>
            {
                // TO DO !!!!!
                FootHubViewModel model = new FootHubViewModel(
                    "Team Details", "");
                return View["ViewDetails", model];
            };
        }

        private static string LeaguesToHtml(League[] leagues)
        {
            const string headers = "<thead><tr>"
                + "<td>Year</td>"
                + "<td>Caption</td>"
                + "</tr></thead>";
            string res = "";
            foreach ( League l in leagues)
            {
                res += String.Format("<tr><td>{1}</td><td><a href='/leagues/{0}'>{2}</a></td></tr>",
                    l.Id,
                    l.Year,
                    l.Caption);
            }
            return "<table class='table table-hover'>" + headers + "<tbody>" + res + "</tbody></table>";
        }

        private static string StandingsToHtml(Standing[] standings)
        {
            const string headers = "<thead><tr>"
            + "<td>Position</td>"
            + "<td>Team</td>"
            + "<td>Points</td>"
            + "</tr></thead>";
            string res = "";
            foreach (Standing s in standings)
            {
                res += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                    s.Position,
                    s.TeamName,
                    s.Points);
            }
            return "<table class='table table-hover'>" + headers + "<tbody>" + res + "</tbody></table>";
        }
    }
}
