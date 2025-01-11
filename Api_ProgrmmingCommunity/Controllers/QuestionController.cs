using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public QuestionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] QuestionDTO questionDto)
            {
            if (questionDto == null)
                {
                return BadRequest("Question data is null.");
                }

            var question = new Question
                {
                SubjectCode = questionDto.SubjectCode,
                TopicId = questionDto.TopicId,
                UserId = questionDto.UserId,
                Difficulty = questionDto.Difficulty,
                Text = questionDto.Text,
                Type = questionDto.Type,
                IsDeleted = false
                };

            _context.Questions.Add(question);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
            }

        [HttpGet("GetQuestion")]
        public IActionResult GetQuestion([FromQuery] int? id, [FromQuery] string? subjectCode, [FromQuery] int? topicId, [FromQuery] int? userId)
            {
            var question = _context.Questions.FirstOrDefault(q =>
                q.IsDeleted == false &&
                ((id != null && q.Id == id) ||
                 (subjectCode != null && q.SubjectCode == subjectCode) ||
                 (topicId != null && q.TopicId == topicId) ||
                 (userId != null && q.UserId == userId)));

            if (question == null)
                {
                return NotFound("Question not found or is marked as deleted.");
                }

            return Ok(new QuestionDTO
                {
                Id = question.Id,
                SubjectCode = question.SubjectCode,
                TopicId = question.TopicId,
                UserId = question.UserId,
                Difficulty = question.Difficulty,
                Text = question.Text,
                Type = question.Type,
                IsDeleted = question.IsDeleted
                });
            }

        [HttpGet("GetAllQuestions")]
        public IActionResult GetAllQuestions()
            {
            var questions = _context.Questions
                .Where(q => q.IsDeleted == false)
                .Select(q => new QuestionDTO
                    {
                    Id = q.Id,
                    SubjectCode = q.SubjectCode,
                    TopicId = q.TopicId,
                    UserId = q.UserId,
                    Difficulty = q.Difficulty,
                    Text = q.Text,
                    Type = q.Type,
                    IsDeleted = q.IsDeleted
                    })
                .ToList();

            return Ok(questions);
            }

        [HttpDelete("DeleteQuestion")]
        public IActionResult DeleteQuestion([FromQuery] int? id, [FromQuery] string? subjectCode, [FromQuery] int? topicId, [FromQuery] int? userId)
            {
            var question = _context.Questions.FirstOrDefault(q =>
                (id != null && q.Id == id) ||
                (subjectCode != null && q.SubjectCode == subjectCode) ||
                (topicId != null && q.TopicId == topicId) ||
                (userId != null && q.UserId == userId));

            if (question == null)
                {
                return NotFound("Question not found.");
                }

            question.IsDeleted = true;
            _context.Questions.Update(question);
            _context.SaveChanges();

            return Ok("Question marked as deleted.");
            }
        }
    }
