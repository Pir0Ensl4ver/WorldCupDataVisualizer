using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Services;
using RestSharp;

namespace DataLayer.Repository.Api
{
    public class ApiMatchRepository : IMatchRepository
    {
        private readonly EndpointPrefixDeterminer _prefixDeterminer = EndpointPrefixDeterminer.Instance;
        private const string FifaCodeEndpoint = "/matches/country?fifa_code=";
        private const string AllMatchesEndpoint = "/matches";

        public async Task<List<MatchModel>> GetMatchesForFifaCode(string fifaCode)
        {
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + FifaCodeEndpoint + fifaCode);
            var response = await client.ExecuteGetAsync<List<MatchModel>>(new RestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong during api call... Falling back to empty list of matches");
                System.Diagnostics.Debug.WriteLine($"API returned {response.StatusCode}: {response.Content}");
                return new List<MatchModel>();
            }

            return response.Data;
        }

        public async Task<HashSet<TeamModel>> GetOpponentsForFifaCode(string fifaCode)
        {
            List<MatchModel> allMatches = await GetMatchesForFifaCode(fifaCode);
            return allMatches.Aggregate(new HashSet<TeamModel>(), (accumulator, current) =>
            {
                accumulator.Add(current.AwayTeam.Code == fifaCode ? current.HomeTeam : current.AwayTeam);
                return accumulator;
            });
        }

        public async Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByGoals(string fifaCode)
        {
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + FifaCodeEndpoint + fifaCode);
            var response = await client.ExecuteAsync<List<MatchModel>>(new RestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in ApiMatchRepository::GetHighestScoringPlayersForFifaCode() during API call... returning empty dictionary of players");
                System.Diagnostics.Debug.WriteLine($"API returned {response.StatusCode}: {response.Content}");
                return new Dictionary<PlayerModel, int>();
            }

            var playerToEventType = TieEventCountToPlayer(response.Data, fifaCode, "goal");
            return playerToEventType.OrderBy(x => -x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByYellowCards(string fifaCode)
        {
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + FifaCodeEndpoint + fifaCode);
            var response = await client.ExecuteAsync<List<MatchModel>>(new RestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in ApiMatchRepository::GetPlayersOrderedByYellowCards() during API call... returning empty dictionary of players");
                System.Diagnostics.Debug.WriteLine($"API returned {response.StatusCode}: {response.Content}");
                return new Dictionary<PlayerModel, int>();
            }

            var playerToEventCount = TieEventCountToPlayer(response.Data, fifaCode, "yellow-card");
            return playerToEventCount.OrderBy(x => -x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<List<MatchModel>> GetMatchesOrderedByVisitors(string fifaCode)
        {
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + FifaCodeEndpoint + fifaCode);
            var response = await client.ExecuteAsync<List<MatchModel>>(new RestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in ApiMatchRepository::GetMatchesOrderedByVisitors() during API call... returning empty list of matches");
                System.Diagnostics.Debug.WriteLine($"API returned {response.StatusCode}: {response.Content}");
                return new List<MatchModel>();
            }

            response.Data.Sort((match1, match2) =>
                Int32.Parse(match2.Attendance).CompareTo(Int32.Parse(match1.Attendance)));
            return response.Data;
        }


        public async Task<List<PlayerModel>> GetPlayersFor(string fifaCode)
        {
            var client = new RestClient(_prefixDeterminer.GetEndpointPrefix() + FifaCodeEndpoint + fifaCode);
            var response = await client.ExecuteAsync<List<MatchModel>>(new RestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in ApiMatchRepository::GetPlayersFor() during API call... returning empty list of players");
                System.Diagnostics.Debug.WriteLine($"API returned {response.StatusCode}: {response.Content}");
                return new List<PlayerModel>();
            }

            var match = response.Data.First();
            return GetPlayersForTeam(fifaCode, match);
        }

        //-----------------------
        //   PRIVATE FUNCTIONS 
        //-----------------------

        private Dictionary<PlayerModel, int> TieEventCountToPlayer(List<MatchModel> matches, string fifaCode,
            string eventType)
        {
            return matches.Aggregate(new Dictionary<PlayerModel, int>(), (acc, match) =>
            {
                var currentEvents = TieEventCountToPlayer(match, fifaCode, eventType);
                foreach (var pair in currentEvents)
                {
                    var currentMatchEvent = pair.Value;
                    acc.TryGetValue(pair.Key, out var previousMatchEvent);
                    acc[pair.Key] = previousMatchEvent + currentMatchEvent;
                }

                return acc;
            });
        }

        private Dictionary<PlayerModel, int> TieEventCountToPlayer(MatchModel match, string fifaCode, string eventType)
        {
            var teamPlayers = GetPlayersForTeam(fifaCode, match);
            var matchEvents = match.HomeTeamEvents.Union(match.AwayTeamEvents);
            return teamPlayers.ToDictionary(
                player => player,
                player => { return matchEvents.Count(e => e.TypeOfEvent == eventType && e.Player == player.Name); }
            );
        }

        private List<PlayerModel> GetPlayersForTeam(string fifaCode, MatchModel match)
        {
            return match.HomeTeam.Code == fifaCode
                ? match.HomeTeamStatistics.StartingEleven.Union(match.HomeTeamStatistics.Substitutes).ToList()
                : match.AwayTeamStatistics.StartingEleven.Union(match.AwayTeamStatistics.Substitutes).ToList();
        }
    }
}