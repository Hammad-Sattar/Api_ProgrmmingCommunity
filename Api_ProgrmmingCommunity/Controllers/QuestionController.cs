using Microsoft.AspNetCore.Http;
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

        // DTO class

        [HttpGet("GetAllQuestions")]
        public IActionResult GetAllQuestions()
        {
            var questions = _context.Questions
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    SubjectCode = q.SubjectCode,
                    TopicId = q.TopicId,
                    UserId = q.UserId,
                    Difficulty = q.Difficulty,
                    Text = q.Text,
                    Type = q.Type
                }).ToList();

            if (!questions.Any())
            {
                return NotFound("No questions found.");
            }

            return Ok(questions);
        }

        [HttpPost("AddQuestion")]
        public IActionResult PostQuestion( QuestionDTO questionDTO)
        {
            if (questionDTO == null)
            {
                return BadRequest("Question data is null.");
            }

            
            if (questionDTO.TopicId.HasValue && !_context.Topics.Any(t => t.Id == questionDTO.TopicId))
            {
                return BadRequest($"Topic with ID {questionDTO.TopicId} does not exist.");
            }

            if (questionDTO.UserId.HasValue && !_context.Users.Any(u => u.Id == questionDTO.UserId))
            {
                return BadRequest($"User with ID {questionDTO.UserId} does not exist.");
            }

           
            var question = new Question
            {
                SubjectCode = questionDTO.SubjectCode,
                TopicId = questionDTO.TopicId,
                UserId = questionDTO.UserId,
                Difficulty = questionDTO.Difficulty,
                Text = questionDTO.Text,
                Type = questionDTO.Type
            };

           
            _context.Questions.Add(question);
            _context.SaveChanges();

           
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        }


        [HttpGet("GetQuestionById/{id}")]
        public IActionResult GetQuestionById( int id)
        {
            var question = _context.Questions
                .Where(q => q.Id == id)
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    SubjectCode = q.SubjectCode,
                    TopicId = q.TopicId,
                    UserId = q.UserId,
                    Difficulty = q.Difficulty,
                    Text = q.Text,
                    Type = q.Type
                }).FirstOrDefault();

            if (question == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }

            return Ok(question);
        }
    }
}
