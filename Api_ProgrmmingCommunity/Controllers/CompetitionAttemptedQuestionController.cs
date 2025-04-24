using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetAllAttemptedCompetitionQuestions")]
        public async Task<ActionResult<IEnumerable<CompetitionAttemptedQuestionDTO>>> GetAll()
            {
            var items = await _context.CompetitionAttemptedQuestions
                .Where(x => x.IsDeleted != true)
                .Select(x => new CompetitionAttemptedQuestionDTO
                    {
                    Id = x.Id,
                    CompetitionId = x.CompetitionId,
                    CompetitionRoundId = x.CompetitionRoundId,
                    QuestionId = x.QuestionId,
                    TeamId = x.TeamId,
                    Answer = x.Answer,
                    Score = x.Score,
                    SubmissionTime = x.SubmissionTime
                    })
                .ToListAsync();

            return Ok(items);
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitionAttemptedQuestionDTO>> GetById(int id)
            {
            var x = await _context.CompetitionAttemptedQuestions.FindAsync(id);

            if (x == null || x.IsDeleted == true)
                return NotFound();

            var dto = new CompetitionAttemptedQuestionDTO
                {
                Id = x.Id,
                CompetitionId = x.CompetitionId,
                CompetitionRoundId = x.CompetitionRoundId,
                QuestionId = x.QuestionId,
                TeamId = x.TeamId,
                Answer = x.Answer,
                Score = x.Score,
                SubmissionTime = x.SubmissionTime
                };

            return Ok(dto);
            }

        [HttpPost("AddCompetitionAttemptedQuestion")]
       
        public async Task<ActionResult> AddCompetitionAttemptedQuestions(List<CompetitionAttemptedQuestionDTO> dtoList)
            {
           
            var entities = dtoList.Select(dto => new CompetitionAttemptedQuestion
                {
                CompetitionId = dto.CompetitionId,
                CompetitionRoundId = dto.CompetitionRoundId,
                QuestionId = dto.QuestionId,
                TeamId = dto.TeamId,
                Answer = dto.Answer,
                Score = dto.Score,
                SubmissionTime = dto.SubmissionTime
                }).ToList();

            
            _context.CompetitionAttemptedQuestions.AddRange(entities);
            await _context.SaveChangesAsync();

            return Ok("Round Questions Attempted successfully");
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CompetitionAttemptedQuestionDTO dto)
            {
            var x = await _context.CompetitionAttemptedQuestions.FindAsync(id);

            if (x == null || x.IsDeleted == true)
                return NotFound();

            x.CompetitionId = dto.CompetitionId;
            x.CompetitionRoundId = dto.CompetitionRoundId;
            x.QuestionId = dto.QuestionId;
            x.TeamId = dto.TeamId;
            x.Answer = dto.Answer;
            x.Score = dto.Score;
            x.SubmissionTime = dto.SubmissionTime;

            _context.Entry(x).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            {
            var x = await _context.CompetitionAttemptedQuestions.FindAsync(id);

            if (x == null || x.IsDeleted == true)
                return NotFound();

            x.IsDeleted = true;
            _context.Entry(x).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            }
        }
    }
