using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionRoundQuestionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionRoundQuestionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateCompetitionRoundQuestion")]
        public IActionResult CreateCompetitionRoundQuestion([FromBody] CompetitionRoundQuestionDTO competitionRoundQuestionDto)
            {
            if (competitionRoundQuestionDto == null)
                {
                return BadRequest("Competition round question data is null.");
                }

            var competitionRoundQuestion = new CompetitionRoundQuestion
                {
                CompetitionRoundId = competitionRoundQuestionDto.CompetitionRoundId,
                QuestionId = competitionRoundQuestionDto.QuestionId,
  
                };

            _context.CompetitionRoundQuestions.Add(competitionRoundQuestion);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetitionRoundQuestion), new { id = competitionRoundQuestion.Id }, competitionRoundQuestion);
            }

        [HttpGet("GetCompetitionRoundQuestion")]
        public IActionResult GetCompetitionRoundQuestion([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? questionId)
            {
            var competitionRoundQuestions = _context.CompetitionRoundQuestions
                .Where(crq =>
                    (id != null && crq.Id == id) ||
                    (competitionRoundId != null && crq.CompetitionRoundId == competitionRoundId) ||
                    (questionId != null && crq.QuestionId == questionId))
                .ToList();

            if (competitionRoundQuestions == null || !competitionRoundQuestions.Any())
                {
                return NotFound("Competition round questions not found.");
                }

            var competitionRoundQuestionDtos = competitionRoundQuestions
                .Select(crq => new CompetitionRoundQuestionDTO
                    {
                    Id = crq.Id,
                    CompetitionRoundId = crq.CompetitionRoundId,
                    QuestionId = crq.QuestionId,
                    })
                .ToList();

            return Ok(competitionRoundQuestionDtos);
            }


        [HttpPut("UpdateCompetitionRoundQuestion")]
        public IActionResult UpdateCompetitionRoundQuestion(int id, [FromBody] CompetitionRoundQuestionDTO competitionRoundQuestionDto)
            {
            var competitionRoundQuestion = _context.CompetitionRoundQuestions.FirstOrDefault(crq => crq.Id == id);

            if (competitionRoundQuestion == null)
                {
                return NotFound("Competition round question not found.");
                }

            competitionRoundQuestion.CompetitionRoundId = competitionRoundQuestionDto.CompetitionRoundId;
            competitionRoundQuestion.QuestionId = competitionRoundQuestionDto.QuestionId;


            _context.CompetitionRoundQuestions.Update(competitionRoundQuestion);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteCompetitionRoundQuestion")]
        public IActionResult DeleteCompetitionRoundQuestion([FromQuery] int? id, [FromQuery] int? competitionRoundId, [FromQuery] int? questionId)
            {
            var competitionRoundQuestion = _context.CompetitionRoundQuestions
                .FirstOrDefault(crq =>
                    (id != null && crq.Id == id) ||
                    (competitionRoundId != null && crq.CompetitionRoundId == competitionRoundId) ||
                    (questionId != null && crq.QuestionId == questionId));

            if (competitionRoundQuestion == null)
                {
                return NotFound("Competition round question not found.");
                }

            _context.CompetitionRoundQuestions.Remove(competitionRoundQuestion);
            _context.SaveChanges();

            return Ok("Competition round question deleted.");
            }
        }
    }
