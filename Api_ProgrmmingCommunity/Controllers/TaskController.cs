using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;
using Microsoft.EntityFrameworkCore;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public TaskController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] TaskDTO taskDto)
            {
            if (taskDto == null)
                {
                return BadRequest("Task data is null.");
                }

            var task = new Models.Task
                {
                MinLevel = taskDto.MinLevel,
                MaxLevel = taskDto.MaxLevel,
                StartDate = taskDto.StartDate,
                EndDate = taskDto.EndDate,
                UserId = taskDto.UserId,
                IsDeleted = false
                };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(new { id = task.Id });
            }

        [HttpGet("GetTasksByUser")]
        public IActionResult GetTasksByUser([FromQuery] int userId)
            {
            var tasks = _context.Tasks
                .Where(t => t.UserId == userId && t.IsDeleted!=true)
                .Select(t => new TaskDTO
                    {
                    Id = t.Id,
                    MinLevel = t.MinLevel,
                    MaxLevel = t.MaxLevel,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    UserId = t.UserId
                    })
                .ToList();

            if (!tasks.Any())
                {
                return NotFound("No tasks found for the given user or all tasks are marked as deleted.");
                }

            return Ok(tasks);
            }


        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
            {
            var tasks = _context.Tasks
                .Where(t => t.IsDeleted == false  && t.Attempt ==false)
                .Select(t => new TaskDTO
                    {
                    Id = t.Id,
                    MinLevel = t.MinLevel,
                    MaxLevel = t.MaxLevel,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    UserId=t.UserId
                   
                    })
                .ToList();

            return Ok(tasks);
            }

        [HttpPut("Attempt/{taskId}")]
        public async Task<IActionResult> MarkTaskAsAttempted(int taskId)
            {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.IsDeleted == false);

            if (task == null)
                {
                return NotFound("Task not found or task is deleted.");
                }

            task.Attempt = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Task marked as attempted.", taskId = taskId });
            }

        [HttpGet("GetTaskQuestionCount/{taskId}")]
        public IActionResult GetTaskQuestionCount(int taskId)
            {
            var questionCount = _context.TaskQuestions
                .Where(tq => tq.TaskId == taskId && tq.IsDeleted == false)
                .Count();

            return Ok(new { TaskId = taskId, QuestionCount = questionCount });
            }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask([FromQuery] int id)
            {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                {
                return NotFound("Task not found.");
                }

            task.IsDeleted = true;
            _context.Tasks.Update(task);
            _context.SaveChanges();

            return Ok("Task marked as deleted.");
            }
        }
    }
