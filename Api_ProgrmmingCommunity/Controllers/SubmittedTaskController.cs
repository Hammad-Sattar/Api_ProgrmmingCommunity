using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class SubmittedTaskController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public SubmittedTaskController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddSubmittedTask")]
        
        public IActionResult CreateSubmittedTasks([FromBody] List<SubmittedTaskDTO> submittedTaskDtos)
            {
            if (submittedTaskDtos == null || !submittedTaskDtos.Any())
                {
                return BadRequest("No submitted tasks provided.");
                }

            var submittedTasks = submittedTaskDtos.Select(dto => new SubmittedTask
                {
                TaskId = dto.TaskId,
                QuestionId = dto.QuestionId,
                UserId = dto.UserId,
                Answer = dto.Answer,
                SubmissionDate = dto.SubmissionDate,
                SubmissionTime = dto.SubmissionTime,
                Score = dto.Score
                }).ToList();

            _context.SubmittedTasks.AddRange(submittedTasks);
            _context.SaveChanges();

            return Ok("All tasks submitted successfully.");
            }


        [HttpGet("GetSubmittedTask")]
        public IActionResult GetSubmittedTask([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] int? taskId, [FromQuery] int? questionId)
            {
            var submittedTasksQuery = _context.SubmittedTasks.AsQueryable();

            // Apply filters based on the provided query parameters
            if (id != null)
                submittedTasksQuery = submittedTasksQuery.Where(st => st.Id == id);
            if (userId != null)
                submittedTasksQuery = submittedTasksQuery.Where(st => st.UserId == userId);
            if (taskId != null)
                submittedTasksQuery = submittedTasksQuery.Where(st => st.TaskId == taskId);
            if (questionId != null)
                submittedTasksQuery = submittedTasksQuery.Where(st => st.QuestionId == questionId);

            var submittedTasks = submittedTasksQuery.ToList(); // Get the list of matching records

            if (submittedTasks.Count == 0)
                {
                return NotFound("No submitted tasks found.");
                }

            var submittedTaskDtos = submittedTasks.Select(st => new SubmittedTaskDTO
                {
                Id = st.Id,
                TaskId = st.TaskId,
                QuestionId = st.QuestionId,
                UserId = st.UserId,
                Answer = st.Answer,
                SubmissionDate = st.SubmissionDate,
                SubmissionTime = st.SubmissionTime,
                Score = st.Score
                }).ToList();

            return Ok(submittedTaskDtos);
            }

        [HttpPut("UpdateSubmittedTask")]
        public IActionResult UpdateSubmittedTask(int id, [FromBody] SubmittedTaskDTO submittedTaskDto)
            {
            var submittedTask = _context.SubmittedTasks.FirstOrDefault(st => st.Id == id);

            if (submittedTask == null)
                {
                return NotFound("Submitted task not found.");
                }

            submittedTask.TaskId = submittedTaskDto.TaskId;
            submittedTask.QuestionId = submittedTaskDto.QuestionId;
            submittedTask.UserId = submittedTaskDto.UserId;
            submittedTask.Answer = submittedTaskDto.Answer;
            submittedTask.SubmissionDate = submittedTaskDto.SubmissionDate;
            submittedTask.SubmissionTime = submittedTaskDto.SubmissionTime;
            submittedTask.Score = submittedTaskDto.Score;

            _context.SubmittedTasks.Update(submittedTask);
            _context.SaveChanges();

            return NoContent();
            }

        [HttpDelete("DeleteSubmittedTask")]
        public IActionResult DeleteSubmittedTask([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] int? taskId, [FromQuery] int? questionId)
            {
            var submittedTask = _context.SubmittedTasks
                .FirstOrDefault(st =>
                    (id != null && st.Id == id) ||
                    (userId != null && st.UserId == userId) ||
                    (taskId != null && st.TaskId == taskId) ||
                    (questionId != null && st.QuestionId == questionId));

            if (submittedTask == null)
                {
                return NotFound("Submitted task not found.");
                }

            _context.SubmittedTasks.Remove(submittedTask);
            _context.SaveChanges();

            return Ok("Submitted task deleted.");
            }
        }
    }
