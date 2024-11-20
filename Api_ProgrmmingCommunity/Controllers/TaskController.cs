using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ProgrammingCommunityContext _context;

    public TaskController(ProgrammingCommunityContext context)
    {
        _context = context;
    }

    // Add a new task
    [HttpPost("AddTask")]
    public IActionResult AddTask(TaskDTO taskDTO)
    {
        if (taskDTO == null)
        {
            return BadRequest("Task data is null.");
        }

        // Map received TaskDTO to Task model
        var task = new Api_ProgrmmingCommunity.Models.Task
        {
            MinLevel = taskDTO.MinLevel,
            MaxLevel = taskDTO.MaxLevel,
            StartDate = taskDTO.StartDate,
            EndDate = taskDTO.EndDate,

        };

        // Save to database
        _context.Tasks.Add(task);
        _context.SaveChanges();

        // Return the created task as TaskDTO with a 201 status
        var taskResponse = new TaskDTO
        {
            Id = task.Id,
            MinLevel = task.MinLevel ?? 0,
            MaxLevel = task.MaxLevel ?? 0,
            StartDate = taskDTO.StartDate,
            EndDate = taskDTO.EndDate,
        };

        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, taskResponse);
    }

    // Get a task by ID
    [HttpGet("GetTaskById/{id}")]
    public IActionResult GetTaskById(int id)
    {
        var task = _context.Tasks
            .Where(t => t.Id == id)
            .FirstOrDefault();

        if (task == null)
        {
            return NotFound($"Task with ID {id} not found.");
        }

        // Map Task model to TaskDTO for response
        var taskDTO = new TaskDTO
        {
            Id = task.Id,
            MinLevel = task.MinLevel,
            MaxLevel = task.MaxLevel ?? 0,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
        };

        return Ok(taskDTO);
    }

    [HttpGet("GetAllTasks")]
    public IActionResult GetAllTasks()
    {
      
        var tasks = _context.Tasks
            .Select(t => new TaskDTO
            {
                Id = t.Id,
                MinLevel = t.MinLevel ?? 0,
                MaxLevel = t.MaxLevel ?? 0,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
            })
            .ToList();

   
        if (tasks.Count == 0)
        {
            return NotFound("No tasks found.");
        }

      
        return Ok(tasks);
    }

}
