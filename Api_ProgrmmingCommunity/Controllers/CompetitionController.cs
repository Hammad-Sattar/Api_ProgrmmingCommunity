using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

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
