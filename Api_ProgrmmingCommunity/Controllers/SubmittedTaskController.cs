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
        public IActionResult CreateSubmittedTask([FromBody] SubmittedTaskDTO submittedTaskDto)
            {
            if (submittedTaskDto == null)
                {
                return BadRequest("Submitted task data is null.");
                }

            var submittedTask = new SubmittedTask
                {
                TaskquestionId = submittedTaskDto.TaskquestionId,
                UserId = submittedTaskDto.UserId,
                Answer = submittedTaskDto.Answer
                };

            _context.SubmittedTasks.Add(submittedTask);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSubmittedTask), new { id = submittedTask.Id }, submittedTask);
            }

        [HttpGet("GetSubmittedTask")]
        public IActionResult GetSubmittedTask([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] int? taskquestionId)
            {
            var submittedTask = _context.SubmittedTasks
                .FirstOrDefault(st =>
                    (id != null && st.Id == id) ||
                    (userId != null && st.UserId == userId) ||
                    (taskquestionId != null && st.TaskquestionId == taskquestionId));

            if (submittedTask == null)
                {
                return NotFound("Submitted task not found.");
                }

            var submittedTaskDto = new SubmittedTaskDTO
                {
                Id = submittedTask.Id,
                TaskquestionId = submittedTask.TaskquestionId,
                UserId = submittedTask.UserId,
                Answer = submittedTask.Answer
                };

            return Ok(submittedTaskDto);
            }

        //[HttpPut("UpdateSubmittedTask")]
        //public IActionResult UpdateSubmittedTask(int id, [FromBody] SubmittedTaskDTO submittedTaskDto)
        //    {
        //    var submittedTask = _context.SubmittedTasks.FirstOrDefault(st => st.Id == id);

        //    if (submittedTask == null)
        //        {
        //        return NotFound("Submitted task not found.");
        //        }

        //    submittedTask.TaskquestionId = submittedTaskDto.TaskquestionId;
        //    submittedTask.UserId = submittedTaskDto.UserId;
        //    submittedTask.Answer = submittedTaskDto.Answer;

        //    _context.SubmittedTasks.Update(submittedTask);
        //    _context.SaveChanges();

        //    return NoContent();
        //    }

        [HttpDelete("DeleteSubmittedTask")]
        public IActionResult DeleteSubmittedTask([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] int? taskquestionId)
            {
            var submittedTask = _context.SubmittedTasks
                .FirstOrDefault(st =>
                    (id != null && st.Id == id) ||
                    (userId != null && st.UserId == userId) ||
                    (taskquestionId != null && st.TaskquestionId == taskquestionId));

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
