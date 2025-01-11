using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionAttemptedQuestionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public CompetitionAttemptedQuestionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateCompetitionAttemptedQuestion")]
        public IActionResult CreateCompetitionAttemptedQuestion([FromBody] CompetitionAttemptedQuestionDTO competitionAttemptedQuestionDto)
            {
            if (competitionAttemptedQuestionDto == null)
                {
                return BadRequest("Competition attempted question data is null.");
                }

            var competitionAttemptedQuestion = new CompetitionAttemptedQuestion
                {
                CompetitionRoundQuestionId = competitionAttemptedQuestionDto.CompetitionRoundQuestionId,
                TeamId = competitionAttemptedQuestionDto.TeamId,
                Answer = competitionAttemptedQuestionDto.Answer,
                Score = competitionAttemptedQuestionDto.Score,
                SubmissionTime = competitionAttemptedQuestionDto.SubmissionTime,
                IsDeleted = competitionAttemptedQuestionDto.IsDeleted
                };

            _context.CompetitionAttemptedQuestions.Add(competitionAttemptedQuestion);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetitionAttemptedQuestion), new { id = competitionAttemptedQuestion.Id }, competitionAttemptedQuestion);
            }

        [HttpGet("GetCompetitionAttemptedQuestion")]
        public IActionResult GetCompetitionAttemptedQuestion([FromQuery] int? id, [FromQuery] int? competitionRoundQuestionId, [FromQuery] int? teamId)
            {
            var competitionAttemptedQuestion = _context.CompetitionAttemptedQuestions
                .FirstOrDefault(caq =>
                    (id != null && caq.Id == id) ||
                    (competitionRoundQuestionId != null && caq.CompetitionRoundQuestionId == competitionRoundQuestionId) ||
                    (teamId != null && caq.TeamId == teamId));

            if (competitionAttemptedQuestion == null)
                {
                return NotFound("Competition attempted question not found.");
                }

            var competitionAttemptedQuestionDto = new CompetitionAttemptedQuestionDTO
                {
                Id = competitionAttemptedQuestion.Id,
                CompetitionRoundQuestionId = competitionAttemptedQuestion.CompetitionRoundQuestionId,
                TeamId = competitionAttemptedQuestion.TeamId,
                Answer = competitionAttemptedQuestion.Answer,
                Score = competitionAttemptedQuestion.Score,
                SubmissionTime = competitionAttemptedQuestion.SubmissionTime,
                IsDeleted = competitionAttemptedQuestion.IsDeleted
                };

            return Ok(competitionAttemptedQuestionDto);
            }

        [HttpPut("UpdateCompetitionAttemptedQuestion")]
        public IActionResult UpdateCompetitionAttemptedQuestion(int id, [FromBody] CompetitionAttemptedQuestionDTO competitionAttemptedQuestionDto)
            {
            var competitionAttemptedQuestion = _context.CompetitionAttemptedQuestions.FirstOrDefault(caq => caq.Id == id);

            if (competitionAttemptedQuestion == null)
                {
                return NotFound("Competition attempted question not found.");
                }

            competitionAttemptedQuestion.CompetitionRoundQuestionId = competitionAttemptedQuestionDto.CompetitionRoundQuestionId;
            competitionAttemptedQuestion.TeamId = competitionAttemptedQuestionDto.TeamId;
            competitionAttemptedQuestion.Answer = competitionAttemptedQuestionDto.Answer;
            competitionAttemptedQuestion.Score = competitionAttemptedQuestionDto.Score;
            competitionAttemptedQuestion.SubmissionTime = competitionAttemptedQuestionDto.SubmissionTime;
            competitionAttemptedQuestion.IsDeleted = competitionAttemptedQuestionDto.IsDeleted;

            _context.CompetitionAttemptedQuestions.Update(competitionAttemptedQuestion);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteCompetitionAttemptedQuestion")]
        public IActionResult DeleteCompetitionAttemptedQuestion([FromQuery] int? id, [FromQuery] int? competitionRoundQuestionId, [FromQuery] int? teamId)
            {
            var competitionAttemptedQuestion = _context.CompetitionAttemptedQuestions
                .FirstOrDefault(caq =>
                    (id != null && caq.Id == id) ||
                    (competitionRoundQuestionId != null && caq.CompetitionRoundQuestionId == competitionRoundQuestionId) ||
                    (teamId != null && caq.TeamId == teamId));

            if (competitionAttemptedQuestion == null)
                {
                return NotFound("Competition attempted question not found.");
                }

            _context.CompetitionAttemptedQuestions.Remove(competitionAttemptedQuestion);
            _context.SaveChanges();

            return Ok("Competition attempted question deleted.");
            }
        }
    }
