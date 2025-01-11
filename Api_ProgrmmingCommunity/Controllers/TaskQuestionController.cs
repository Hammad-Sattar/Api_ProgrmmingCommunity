using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TaskQuestionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public TaskQuestionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("CreateTaskQuestion")]
        public IActionResult CreateTaskQuestion([FromBody] TaskQuestionDTO taskQuestionDto)
            {
            if (taskQuestionDto == null)
                {
                return BadRequest("Task question data is null.");
                }

            var taskQuestion = new TaskQuestion
                {
                TaskId = taskQuestionDto.TaskId,
                QuestionId = taskQuestionDto.QuestionId,
                IsDeleted = taskQuestionDto.IsDeleted
                };

            _context.TaskQuestions.Add(taskQuestion);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskQuestion), new { id = taskQuestion.Id }, taskQuestion);
            }

        [HttpGet("GetTaskQuestion")]
        public IActionResult GetTaskQuestion([FromQuery] int? id, [FromQuery] int? taskId, [FromQuery] int? questionId)
            {
            var taskQuestion = _context.TaskQuestions
                .FirstOrDefault(tq =>
                    (id != null && tq.Id == id) ||
                    (taskId != null && tq.TaskId == taskId) ||
                    (questionId != null && tq.QuestionId == questionId));

            if (taskQuestion == null)
                {
                return NotFound("Task question not found.");
                }

            var taskQuestionDto = new TaskQuestionDTO
                {
                Id = taskQuestion.Id,
                TaskId = taskQuestion.TaskId,
                QuestionId = taskQuestion.QuestionId,
                IsDeleted = taskQuestion.IsDeleted
                };

            return Ok(taskQuestionDto);
            }

        [HttpPut("UpdateTaskQuestion")]
        public IActionResult UpdateTaskQuestion(int id, [FromBody] TaskQuestionDTO taskQuestionDto)
            {
            var taskQuestion = _context.TaskQuestions.FirstOrDefault(tq => tq.Id == id);

            if (taskQuestion == null)
                {
                return NotFound("Task question not found.");
                }

            taskQuestion.TaskId = taskQuestionDto.TaskId;
            taskQuestion.QuestionId = taskQuestionDto.QuestionId;
            taskQuestion.IsDeleted = taskQuestionDto.IsDeleted;

            _context.TaskQuestions.Update(taskQuestion);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteTaskQuestion")]
        public IActionResult DeleteTaskQuestion([FromQuery] int? id, [FromQuery] int? taskId, [FromQuery] int? questionId)
            {
            var taskQuestion = _context.TaskQuestions
                .FirstOrDefault(tq =>
                    (id != null && tq.Id == id) ||
                    (taskId != null && tq.TaskId == taskId) ||
                    (questionId != null && tq.QuestionId == questionId));

            if (taskQuestion == null)
                {
                return NotFound("Task question not found.");
                }

            _context.TaskQuestions.Remove(taskQuestion);
            _context.SaveChanges();

            return Ok("Task question deleted.");
            }
        }
    }
