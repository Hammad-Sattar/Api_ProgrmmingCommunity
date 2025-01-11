using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public TopicController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddTopic")]
        public IActionResult AddTopic([FromBody] Topic topic)
            {
            if (topic == null)
                {
                return BadRequest("Topic data is null.");
                }

            topic.IsDeleted = false;

            _context.Topics.Add(topic);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topic);
            }

        [HttpGet("GetTopic")]
        public IActionResult GetTopic([FromQuery] int? id, [FromQuery] string? subjectCode, [FromQuery] string? title)
            {
            var topic = _context.Topics.FirstOrDefault(t =>
                t.IsDeleted == false &&
                ((id != null && t.Id == id) ||
                 (subjectCode != null && t.SubjectCode == subjectCode) ||
                 (title != null && t.Title == title)));

            if (topic == null)
                {
                return NotFound("Topic not found or is marked as deleted.");
                }

            return Ok(topic);
            }

        [HttpGet("GetAllTopics")]
        public IActionResult GetAllTopics()
            {
            var topics = _context.Topics
                .Where(t => t.IsDeleted == false)
                .ToList();

            return Ok(topics);
            }

        [HttpDelete("DeleteTopic")]
        public IActionResult DeleteTopic([FromQuery] int? id, [FromQuery] string? subjectCode, [FromQuery] string? title)
            {
            var topic = _context.Topics.FirstOrDefault(t =>
                (id != null && t.Id == id) ||
                (subjectCode != null && t.SubjectCode == subjectCode) ||
                (title != null && t.Title == title));

            if (topic == null)
                {
                return NotFound("Topic not found.");
                }

            topic.IsDeleted = true;
            _context.Topics.Update(topic);
            _context.SaveChanges();

            return Ok("Topic marked as deleted.");
            }
        }
    }
