using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.EntityFrameworkCore;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("MakeCompetition")]
        public IActionResult AddCompetition([FromBody] CompetitionDTO competitionDto)
            {
            if (competitionDto == null)
                {
                return BadRequest("Competition data is null.");
                }

            var competition = new Competition
                {
                CompetitionId = competitionDto.CompetitionId,
                Title = competitionDto.Title,
                Year = competitionDto.Year,
                MinLevel = competitionDto.MinLevel,
                MaxLevel = competitionDto.MaxLevel,
                Password = competitionDto.Password,
                UserId = competitionDto.UserId,
                Rounds = competitionDto.Rounds,
              
                };

            _context.Competitions.Add(competition);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetition), new { id = competition.CompetitionId });
            }

        [HttpGet("GetUnregisterdCompetition/{userId}")]
        public async Task<IActionResult> GetUnregisterdCompetition(int userId)
            {
            var competitions = await _context.Competitions
                .Where(c => c.IsDeleted == false)
                .Where(c => !_context.CompetitionTeams
                    .Join(_context.Teams, ct => ct.TeamId, t => t.TeamId, (ct, t) => new { ct, t })
                    .Join(_context.TeamMembers, joined => joined.t.TeamId, tm => tm.TeamId, (joined, tm) => new { joined, tm })
                    .Any(x => x.tm.UserId == userId && x.joined.ct.CompetitionId == c.CompetitionId))
                .Select(c => new
                    {
                    c.CompetitionId,
                    c.Title,
                    c.Year,
                    c.MinLevel,
                    c.MaxLevel
                    })
                .ToListAsync(); // Use ToListAsync() instead of ToList()

            // If no competitions found
            if (competitions == null || !competitions.Any())
                {
                return Ok(new { message = "User has no unregistered competitions.", data = competitions });
                }

            return Ok(new { message = "Competitions retrieved successfully.", data = competitions });
            }


        [HttpGet("GetRegisteredCompetitions/{userId}")]
        public async Task<IActionResult> GetRegisteredCompetitions(int userId)
            {
            var competitions = await _context.Competitions
                .Where(c => c.IsDeleted == false)
                .Join(_context.CompetitionTeams, c => c.CompetitionId, ct => ct.CompetitionId, (c, ct) => new { c, ct })
                .Join(_context.Teams, joined => joined.ct.TeamId, t => t.TeamId, (joined, t) => new { joined, t })
                .Join(_context.TeamMembers, joined2 => joined2.t.TeamId, tm => tm.TeamId, (joined2, tm) => new { joined2, tm })
                .Where(x => x.tm.UserId == userId)
                .Select(x => new
                    {
                    x.joined2.joined.c.CompetitionId,
                    x.joined2.joined.c.Title,
                    x.joined2.joined.c.Year,
                    x.joined2.joined.c.MinLevel,
                    x.joined2.joined.c.MaxLevel
                    })
                .Distinct()
                .ToListAsync();

            if (!competitions.Any())
                {
                return Ok(new { message = "User is not registered in any competition.", data = competitions });
                }

            return Ok(new { message = "Registered competitions retrieved successfully.", data = competitions });
            }


        [HttpGet("GetCompetition")]
        public IActionResult GetCompetition([FromQuery] int competitionId)
            {
            var competition = _context.Competitions
                .FirstOrDefault(c => c.CompetitionId == competitionId && c.IsDeleted == false);

            if (competition == null)
                {
                return NotFound("Competition not found or is marked as deleted.");
                }

            return Ok(new CompetitionDTO
                {
                CompetitionId = competition.CompetitionId,
                Title = competition.Title,
                Year = competition.Year,
                MinLevel = competition.MinLevel,
                MaxLevel = competition.MaxLevel,
                Password = competition.Password,
                UserId = competition.UserId,
                Rounds = competition.Rounds,
            
                });
            }

        [HttpGet("GetAllCompetitions")]
        public IActionResult GetAllCompetitions()
            {
            var competitions = _context.Competitions
                .Where(c => c.IsDeleted == false)
                .Select(c => new CompetitionDTO
                    {
                    CompetitionId = c.CompetitionId,
                    Title = c.Title,
                    Year = c.Year,
                    MinLevel = c.MinLevel,
                    MaxLevel = c.MaxLevel,
                    Password = c.Password,
                    UserId = c.UserId,
                    Rounds = c.Rounds,
                   
                    })
                .ToList();

            return Ok(competitions);
            }

        [HttpDelete("DeleteCompetition")]
        public IActionResult DeleteCompetition([FromQuery] int competitionId)
            {
            var competition = _context.Competitions
                .FirstOrDefault(c => c.CompetitionId == competitionId);

            if (competition == null)
                {
                return NotFound("Competition not found.");
                }

            competition.IsDeleted = true;
            _context.Competitions.Update(competition);
            _context.SaveChanges();

            return Ok("Competition marked as deleted.");
            }
        }
    }
