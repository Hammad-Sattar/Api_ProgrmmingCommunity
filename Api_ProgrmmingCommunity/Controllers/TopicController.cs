using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using Api_ProgrmmingCommunity.Dto;
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
        public IActionResult AddTopic([FromBody] TopicDTO topicDto)
            {
            if (topicDto == null)
                {
                return BadRequest("Topic data is null.");
                }

            var topic = new Topic
                {
                SubjectCode = topicDto.SubjectCode,
                Title = topicDto.Title,
                IsDeleted = false
                };

            _context.Topics.Add(topic);
            _context.SaveChanges();

            topicDto.Id = topic.Id; // Set the Id from the database after insert

            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topicDto);
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

            var topicDto = new TopicDTO
                {
                Id = topic.Id,
                SubjectCode = topic.SubjectCode,
                Title = topic.Title,
             
                };

            return Ok(topicDto);
            }

        [HttpGet("GetAllTopics")]
        public IActionResult GetAllTopics()
            {
            var topics = _context.Topics
                .Where(t => t.IsDeleted == false)
                .ToList();

            var topicDtos = topics.Select(t => new TopicDTO
                {
                Id = t.Id,
                SubjectCode = t.SubjectCode,
                Title = t.Title,
             
                }).ToList();

            return Ok(topicDtos);
            }

        [HttpGet("GetTopicsBySubject")]
        public IActionResult GetTopicsBySubject(string subjectCode)
            {
           
            if (string.IsNullOrEmpty(subjectCode))
                {
                return BadRequest("Subject code is required.");
                }

          
            var topics = _context.Topics
                .Where(t => t.SubjectCode == subjectCode && t.IsDeleted == false)
                .ToList();

            // Map topics to DTOs
            var topicDtos = topics.Select(t => new TopicDTO
                {
                Id = t.Id,
                SubjectCode = t.SubjectCode,
                Title = t.Title,
                }).ToList();

            return Ok(topicDtos);
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
