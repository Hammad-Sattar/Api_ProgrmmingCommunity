using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionMemberController : ControllerBase
    {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionMemberController(ProgrammingCommunityContext context)
        {
            _context = context;
        }

       
        [HttpPost("AddCompetitionMember")]
        public IActionResult AddCompetitionMember( CompetitionMemberDTO competitionMemberDTO)
        {
            if (competitionMemberDTO == null)
            {
                return BadRequest("CompetitionMemberDTO data is null.");
            }

          
            var competitionMember = new CompetitionMember
            {
                CompetitionId = competitionMemberDTO.CompetitionId,
                UserId = competitionMemberDTO.UserId,
              
            };

    
            _context.CompetitionMembers.Add(competitionMember);
            _context.SaveChanges();

          
            competitionMemberDTO.Id = competitionMember.Id;

            return CreatedAtAction(nameof(GetCompetitionMemberById), new { id = competitionMemberDTO.Id }, competitionMemberDTO);
        }

     
        [HttpGet("GetAllCompetitionMembers")]
        public IActionResult GetAllCompetitionMembers()
        {
            var competitionMembers = _context.CompetitionMembers
                .Select(cm => new CompetitionMemberDTO
                {
                    Id = cm.Id,
                    CompetitionId = cm.CompetitionId,
                    UserId = cm.UserId,
                    
                })
                .ToList();

            if (!competitionMembers.Any())
            {
                return NotFound("No competition members found.");
            }

            return Ok(competitionMembers);
        }

 
        [HttpGet("GetCompetitionMemberById/{id}")]
        public IActionResult GetCompetitionMemberById( int id)
        {
            var competitionMember = _context.CompetitionMembers
                .Where(cm => cm.Id == id)
                .Select(cm => new CompetitionMemberDTO
                {
                    Id = cm.Id,
                    CompetitionId = cm.CompetitionId,
                    UserId = cm.UserId,
                   
                })
                .FirstOrDefault();

            if (competitionMember == null)
            {
                return NotFound($"CompetitionMember with ID {id} not found.");
            }

            return Ok(competitionMember);
        }
    }
}
