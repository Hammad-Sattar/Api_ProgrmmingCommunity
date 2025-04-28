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


        [HttpPut("UpdateSubmittedTaskScore")]
        public IActionResult UpdateSubmittedTaskScore([FromQuery] int id, [FromQuery] int score)
            {
            var submittedTask = _context.SubmittedTasks.FirstOrDefault(st => st.Id == id);

            if (submittedTask == null)
                {
                return NotFound($"No submitted task found with Id = {id}.");
                }

            submittedTask.Score = score;
            _context.SaveChanges();

            return Ok("Score updated successfully.");
            }


        [HttpGet("GetSubmittedTask")]
        public IActionResult GetSubmittedTask([FromQuery] int? taskId )
            {
            var submittedTasksQuery = _context.SubmittedTasks.AsQueryable();

           
            if (taskId != null)
                submittedTasksQuery = submittedTasksQuery.Where(st => st.TaskId == taskId);
            

            var submittedTasks = submittedTasksQuery.ToList(); 

            if (submittedTasks.Count == 0)
                {
                return Ok("No submitted tasks found.");
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
