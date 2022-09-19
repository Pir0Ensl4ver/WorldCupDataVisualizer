using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer
{
    public interface IMatchRepository
    {
        public Task<List<MatchModel>> GetMatchesForFifaCode(string fifaCode);
        public Task<HashSet<TeamModel>> GetOpponentsForFifaCode(string fifaCode);
        public Task<List<PlayerModel>> GetPlayersFor(string fifaCode);
        public Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByGoals(string fifaCode);
        public Task<Dictionary<PlayerModel, int>> GetPlayersOrderedByYellowCards(string fifaCode);
        public Task<List<MatchModel>> GetMatchesOrderedByVisitors(string fifaCode);
    }
}