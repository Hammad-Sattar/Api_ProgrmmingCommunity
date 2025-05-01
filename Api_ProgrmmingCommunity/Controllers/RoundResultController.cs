using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class RoundResultController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public RoundResultController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateRoundResult")]
        public IActionResult CreateRoundResult([FromBody] RoundResultDTO roundResultDto)
            {
            if (roundResultDto == null)
                {
                return BadRequest("Round result data is null.");
                }

            var roundResult = new RoundResult
                {
                CompetitionRoundId = roundResultDto.CompetitionRoundId,
                TeamId = roundResultDto.TeamId,
                Score = roundResultDto.Score,
                CompetitionId = roundResultDto.CompetitionId,
                IsQualified = roundResultDto.IsQualified,
               
                };

            _context.RoundResults.Add(roundResult);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoundResult), new { id = roundResult.Id }, roundResult);
            }

        [HttpGet("GetRoundResult")]
        public IActionResult GetRoundResult([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? teamId, [FromQuery] int? competitionId)
            {
            var roundResults = _context.RoundResults
                .Where(rr =>
                    (id != null && rr.Id == id) ||
                    (competitionRoundId != null && rr.CompetitionRoundId == competitionRoundId) ||
                    (teamId != null && rr.TeamId == teamId) ||
                    (competitionId != null && rr.CompetitionId == competitionId))
                .ToList();

            if (roundResults == null || !roundResults.Any())
                {
                return NotFound("Round results not found.");
                }

            var roundResultsDto = roundResults.Select(rr => new RoundResultDTO
                {
                Id = rr.Id,
                CompetitionRoundId = rr.CompetitionRoundId,
                TeamId = rr.TeamId,
                Score = rr.Score,
                CompetitionId = rr.CompetitionId,
                IsQualified = rr.IsQualified
                }).ToList();

            return Ok(roundResultsDto);
            }


        [HttpPut("UpdateRoundResult")]
        public IActionResult UpdateRoundResult(int id, [FromBody] RoundResultDTO roundResultDto)
            {
            var roundResult = _context.RoundResults.FirstOrDefault(rr => rr.Id == id);

            if (roundResult == null)
                {
                return NotFound("Round result not found.");
                }

            roundResult.CompetitionRoundId = roundResultDto.CompetitionRoundId;
            roundResult.TeamId = roundResultDto.TeamId;
            roundResult.Score = roundResultDto.Score;
            roundResult.CompetitionId = roundResultDto.CompetitionId;
            roundResult.IsQualified = roundResultDto.IsQualified;
           

            _context.RoundResults.Update(roundResult);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteRoundResult")]
        public IActionResult DeleteRoundResult([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? teamId, [FromQuery] int? competitionId)
            {
            var roundResult = _context.RoundResults
                .FirstOrDefault(rr =>
                    (id != null && rr.Id == id) ||
                    (competitionRoundId != null && rr.CompetitionRoundId == competitionRoundId) ||
                    (teamId != null && rr.TeamId == teamId) ||
                    (competitionId != null && rr.CompetitionId == competitionId));

            if (roundResult == null)
                {
                return NotFound("Round result not found.");
                }

            _context.RoundResults.Remove(roundResult);
            _context.SaveChanges();

            return Ok("Round result deleted.");
            }

        [HttpPost("insert-round-results")]
        public async Task<IActionResult> InsertRoundResults()
            {
            try
                {
                
                var attemptedQuestions = await _context.CompetitionAttemptedQuestions
                    .Where(x => x.IsDeleted == false) 
                    .ToListAsync();

                var roundResults = new List<RoundResult>();

                
                var groupedByRoundAndTeam = attemptedQuestions
                    .GroupBy(x => new { x.CompetitionRoundId, x.TeamId })
                    .ToList();

                foreach (var group in groupedByRoundAndTeam)
                    {
                    int totalScoreForRound = 0;
                    int teamScore = 0;

                    foreach (var attemptedQuestion in group)
                        {
                        // Assume each question is worth 10 points, score is already provided in the table
                        int score = attemptedQuestion.Score ?? 0; // If no score is assigned, it defaults to 0

                       
                        teamScore += score;

                        // Accumulate the total available score for the round
                        totalScoreForRound += 10; // Each question gives 10 points
                        }

                    // Calculate 50% of the total available score
                    int qualifyingScore = totalScoreForRound / 2;

                    // Check if the team qualifies (score >= 50% of the total available score)
                    bool isQualified = teamScore >= qualifyingScore;

                    // Create a new round result entry for this team
                    var roundResult = new RoundResult
                        {
                        CompetitionRoundId = group.Key.CompetitionRoundId,
                        TeamId = group.Key.TeamId,
                        Score = teamScore,
                        CompetitionId = group.FirstOrDefault().CompetitionId,
                        IsQualified = isQualified,
                        IsDeleted = false
                        };

                    roundResults.Add(roundResult);
                    }

                // Insert all results into the RoundResults table
                await _context.RoundResults.AddRangeAsync(roundResults);
                await _context.SaveChangesAsync();

                return Ok("Round results inserted successfully.");
                }
            catch (Exception ex)
                {
                return BadRequest($"Error: {ex.Message}");
                }
            }

        [HttpGet("GetRoundResults/{roundId}")]
        public async Task<IActionResult> GetRoundResults(int roundId)
            {
            var results = await (from rr in _context.RoundResults
                                 join t in _context.Teams on rr.TeamId equals t.TeamId
                                 where rr.CompetitionRoundId == roundId
                                       && (rr.IsDeleted == false || rr.IsDeleted == null)
                                       && (t.IsDeleted == false || t.IsDeleted == null)
                                 select new
                                     {
                                     t.TeamId,
                                     t.TeamName,
                                     Score = rr.Score ?? 0,
                                     IsQualified = rr.IsQualified ?? false
                                     })
                                 .Distinct()
                                 .ToListAsync();

            var distinctResults = results
                .GroupBy(r => r.TeamId)
                .Select(g => new CompetitionRoundResultDTO
                    {
                    TeamId = g.Key,
                    TeamName = g.First().TeamName,
                    Score = g.First().Score,
                    IsQualified = g.First().IsQualified
                    })
                .ToList();

            if (!distinctResults.Any())
                {
                return NotFound("No results found for the specified round.");
                }

            return Ok(distinctResults);
            }


        }
    }
