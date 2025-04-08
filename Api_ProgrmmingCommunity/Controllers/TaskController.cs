using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

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

        [HttpGet("GetTask")]
        public IActionResult GetTask([FromQuery] int? id)
            {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.IsDeleted == false);

            if (task == null)
                {
                return NotFound("Task not found or is marked as deleted.");
                }

            return Ok(new TaskDTO
                {
                Id = task.Id,
                MinLevel = task.MinLevel,
                MaxLevel = task.MaxLevel,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                UserId=task.UserId
               
                });
            }

        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
            {
            var tasks = _context.Tasks
                .Where(t => t.IsDeleted == false)
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
