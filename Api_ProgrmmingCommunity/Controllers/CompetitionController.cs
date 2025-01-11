using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        // POST: Add a new competition
        [HttpPost("MakeCompetition")]
        public IActionResult AddCompetition(CompetitionDTO competitionDTO)
        {
            if (competitionDTO == null)
            {
                return BadRequest("Competition data is null.");
            }

           
            var competition = new Competition
            {
                Title = competitionDTO.Title,
                Year = competitionDTO.Year,
                MinLevel = competitionDTO.MinLevel,
                MaxLevel = competitionDTO.MaxLevel,
              
            };

            _context.Competitions.Add(competition);
            _context.SaveChanges();

            
            return CreatedAtAction(nameof(GetCompetitionById), new { id = competition.Id }, competitionDTO);
        }

       
        [HttpGet("GetAllCompetitions")]
        public IActionResult GetAllCompetitions()
        {
            var competitions = _context.Competitions
                .Select(c => new CompetitionDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Year = c.Year,
                    MinLevel = c.MinLevel,
                    MaxLevel = c.MaxLevel,
                    
                })
                .ToList();

            if (!competitions.Any())
            {
                return NotFound("No competitions found.");
            }

            return Ok(competitions);
        }

       
        [HttpGet("GetCompetitionById/{id}")]
        public IActionResult GetCompetitionById( int id)
        {
            var competition = _context.Competitions
                .Where(c => c.Id == id)
                .Select(c => new CompetitionDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Year = c.Year,
                    MinLevel = c.MinLevel,
                    MaxLevel = c.MaxLevel,
                    
                })
                .FirstOrDefault();

            if (competition == null)
            {
                return NotFound($"Competition with ID {id} not found.");
            }

            return Ok(competition);
        }
    }
}
