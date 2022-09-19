using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Services;

namespace DataLayer.File
{
    public class FileMatchRepository : IMatchRepository
    {
        private const string FileSuffix = "matches.json";
        private readonly EndpointPrefixDeterminer _prefixDeterminer = EndpointPrefixDeterminer.Instance;

        public async Task<List<MatchModel>> GetMatchesForFifaCode(string fifaCode)
        {
            using var stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
            var matches = await JsonSerializer.DeserializeAsync<List<MatchModel>>(stream);
            if (matches == null)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong while reading from file... falling back to empty list of matches.");
                return new List<MatchModel>();
            }
            return matches.Where(match => match.HomeTeam.Code == fifaCode || match.AwayTeam.Code == fifaCode).ToList();
        }

        public async Task<HashSet<TeamModel>> GetOpponentsForFifaCode(string fifaCode)
        {
            List<MatchModel> allMatches = await GetMatchesForFifaCode(fifaCode);
            return allMatches.Aggregate(new HashSet<TeamModel>(), (accumulator, current) =>
            {
                if (current.AwayTeam.Code == fifaCode)
                {
                    accumulator.Add(current.HomeTeam);
                }
                else
                {
                    accumulator.Add(current.AwayTeam);
                }

                return accumulator;
            });
        }

        public async Task<List<PlayerModel>> GetPlayersFor(string fifaCode)
        {
            using var stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
            var matches = await JsonSerializer.DeserializeAsync<List<MatchModel>>(stream);
            if (matches == null || !matches.Any())
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in FileMatchRepository::GetPlayersFor() during reading from file... returning empty list of players");
                return new List<PlayerModel>();
            }
            var match = matches.FirstOrDefault(match => match.HomeTeam.Code == fifaCode || match.AwayTeam.Code == fifaCode);
            if (match == null)
            {
                System.Diagnostics.Debug.WriteLine("No match found for current league... Returning empty list of players");
                return new List<PlayerModel>();
            }
            return GetPlayersForTeam(fifaCode, match);
        }

        public async Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByGoals(string fifaCode)
        {
            using var stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
            var matches = await JsonSerializer.DeserializeAsync<List<MatchModel>>(stream);
            if (matches == null || !matches.Any())
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in FileMatchRepository::GetHighestScoringPlayersForFifaCode() during reading from file... returning empty dictionary of players");
                return new Dictionary<PlayerModel, int>();
            }
            var filteredMatches = FilterMatchesForCurrentlySelectedTeam(matches, fifaCode);
            var playerToEventType = TieEventCountToPlayer(filteredMatches, fifaCode, "goal");
            return playerToEventType.OrderBy(x => -x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByYellowCards(string fifaCode)
        {
            using var stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
            var matches = await JsonSerializer.DeserializeAsync<List<MatchModel>>(stream);
            if (matches == null || !matches.Any())
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in FileMatchRepository::GetHighestScoringPlayersForFifaCode() during reading from file... returning empty dictionary of players");
                return new Dictionary<PlayerModel, int>();
            }
            var filteredMatches = FilterMatchesForCurrentlySelectedTeam(matches, fifaCode);
            var playerToEventType = TieEventCountToPlayer(filteredMatches, fifaCode, "yellow-card");
            return playerToEventType.OrderBy(x => -x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<List<MatchModel>> GetMatchesOrderedByVisitors(string fifaCode)
        {
            using var stream = System.IO.File.OpenRead(_prefixDeterminer.GetFilePrefix() + FileSuffix);
            var matches = await JsonSerializer.DeserializeAsync<List<MatchModel>>(stream);
            if (matches == null || !matches.Any())
            {
                System.Diagnostics.Debug.WriteLine(
                    "Something went wrong in FileMatchRepository::GetMatchesOrderedByVisitors() during reading from file... returning empty list of matches");
                return new List<MatchModel>();
            }
            var filteredMatches = FilterMatchesForCurrentlySelectedTeam(matches, fifaCode);
            filteredMatches.Sort((match1, match2) => Int32.Parse(match2.Attendance).CompareTo(Int32.Parse(match1.Attendance)));
            return filteredMatches;
        }
        
        //-----------------------
        //   PRIVATE FUNCTIONS 
        //-----------------------

        private List<MatchModel> FilterMatchesForCurrentlySelectedTeam(List<MatchModel> matches, string fifaCode)
        {
            return matches.FindAll(match => match.HomeTeam.Code == fifaCode || match.AwayTeam.Code == fifaCode);
        }

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