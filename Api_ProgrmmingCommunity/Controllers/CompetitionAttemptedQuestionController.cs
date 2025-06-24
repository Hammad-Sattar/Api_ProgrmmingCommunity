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
        [HttpGet("GetAttemptedQuestionsByRound/{roundId}")]
        
        public async Task<IActionResult> GetAttemptedQuestionsByRound(int roundId, int? teamId = null)
            {
            var query = _context.CompetitionAttemptedQuestions
                .Where(a => a.CompetitionRoundId == roundId && a.IsDeleted==false);

            if (teamId.HasValue)
                {
                query = query.Where(a => a.TeamId == teamId.Value);
                }

#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8629 // Nullable value type may be null.
            var result = await query
                .Select(a => new AttemptedQuestionDto
                    {
                    Id = a.Id,
                    QuestionId = (int)a.QuestionId,
                    QuestionText = _context.Questions
                       .Where(q => q.Id == a.QuestionId)
                       .Select(q => q.Text)
                       .FirstOrDefault(),
                    Marks = (int)_context.Questions
                .Where(q => q.Id == a.QuestionId)
                .Select(q => q.Marks)
                .FirstOrDefault(),

                    TeamId = (int)a.TeamId,
                    TeamName = _context.Teams
                                       .Where(t => t.TeamId == a.TeamId)
                                       .Select(t => t.TeamName)
                                       .FirstOrDefault(),
                    Answer = a.Answer,
                    Score = a.Score,
                    SubmissionTime = (DateTime)a.SubmissionTime
                    })
                .ToListAsync();
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8601 // Possible null reference assignment.

            return Ok(result);
            }


        [HttpPut("UpdateScore")]
        public async Task<IActionResult> UpdateScore(int id, int score)
            {
            var attemptedQuestion = await _context.CompetitionAttemptedQuestions.FindAsync(id);

            if (attemptedQuestion == null || attemptedQuestion.IsDeleted==true)
                {
                return NotFound("Attempted question not found or has been deleted.");
                }

            attemptedQuestion.Score = score;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Score updated successfully." });
            }





        }
    }
