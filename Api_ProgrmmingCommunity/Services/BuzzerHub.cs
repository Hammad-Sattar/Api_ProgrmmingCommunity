using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent; // Add your DbContext namespace
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Models;

namespace Api_ProgrammingCommunity.Services
    {
    public class BuzzerHub : Hub
        {
        private static readonly ConcurrentDictionary<string, (int teamId, int questionId)>
            _firstPress = new ConcurrentDictionary<string, (int, int)>();

        private readonly ProgrammingCommunityContext _dbContext;

        public BuzzerHub(ProgrammingCommunityContext dbContext)
            {
            _dbContext = dbContext;
            }

        public async System.Threading.Tasks.Task ResetBuzzer()
            {
            _firstPress.Clear();
            await Clients.All.SendAsync("ResetBuzzer");
            }

        public async System.Threading.Tasks.Task PressBuzzer(int teamId, int questionId)
            {
            // Get team name from database
            var teamName = await _dbContext.Teams
                .Where(t => t.TeamId == teamId)
                .Select(t => t.TeamName)
                .FirstOrDefaultAsync() ?? "Unknown Team";

            if (_firstPress.IsEmpty)
                {
                _firstPress.TryAdd("current", (teamId, questionId));
                await Clients.All.SendAsync("BuzzerPressed", teamId, teamName, questionId);
                }
            else
                {
                var (pressedTeamId, _) = _firstPress["current"];
                var pressedTeamName = await _dbContext.Teams
                    .Where(t => t.TeamId == pressedTeamId)
                    .Select(t => t.TeamName)
                    .FirstOrDefaultAsync() ?? "Unknown Team";

                await Clients.Caller.SendAsync("BuzzerAlreadyPressed", pressedTeamId, pressedTeamName);
                }
            }

        public async System.Threading.Tasks.Task MoveToNextQuestion()
            {
            await Clients.All.SendAsync("MoveToNextQuestion");
            }

        public async Task<(int teamId, string teamName, int questionId)?> GetCurrentPress()
            {
            if (_firstPress.TryGetValue("current", out var press))
                {
                var teamName = await _dbContext.Teams
                    .Where(t => t.TeamId == press.teamId)
                    .Select(t => t.TeamName)
                    .FirstOrDefaultAsync() ?? "Unknown Team";

                return (press.teamId, teamName, press.questionId);
                }
            return null;
            }
        }
    }