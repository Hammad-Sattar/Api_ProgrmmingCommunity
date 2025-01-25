using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertTopicController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public ExpertTopicController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddExpertTopic")]
        public IActionResult AddExpertTopic([FromBody] ExpertTopicDTO expertTopicDto)
            {
            if (expertTopicDto == null)
                {
                return BadRequest("ExpertTopic data is null.");
                }

            var expertTopic = new ExpertTopic
                {
                ExpertId = expertTopicDto.ExpertId,
                TopicId = expertTopicDto.TopicId,
                IsDeleted = false
                };

            _context.ExpertTopics.Add(expertTopic);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExpertTopic), new { id = expertTopic.Id }, expertTopic);
            }

        [HttpGet("GetExpertTopic")]
        public IActionResult GetExpertTopic([FromQuery] int? id, [FromQuery] int? expertId, [FromQuery] int? topicId)
            {
            var expertTopic = _context.ExpertTopics.FirstOrDefault(et =>
                et.IsDeleted == false &&
                ((id != null && et.Id == id) ||
                 (expertId != null && et.ExpertId == expertId) ||
                 (topicId != null && et.TopicId == topicId)));

            if (expertTopic == null)
                {
                return NotFound("ExpertTopic not found or is marked as deleted.");
                }

            return Ok(new ExpertTopicDTO
                {
                Id = expertTopic.Id,
                ExpertId = expertTopic.ExpertId,
                TopicId = expertTopic.TopicId,
               
                });
            }

        [HttpGet("GetAllExpertTopics")]
        public IActionResult GetAllExpertTopics()
            {
            var expertTopics = _context.ExpertTopics
                .Where(et => et.IsDeleted == false)
                .Select(et => new ExpertTopicDTO
                    {
                    Id = et.Id,
                    ExpertId = et.ExpertId,
                    TopicId = et.TopicId,
                  
                    })
                .ToList();

            return Ok(expertTopics);
            }

        [HttpDelete("DeleteExpertTopic")]
        public IActionResult DeleteExpertTopic([FromQuery] int? id, [FromQuery] int? expertId, [FromQuery] int? topicId)
            {
            var expertTopic = _context.ExpertTopics.FirstOrDefault(et =>
                (id != null && et.Id == id) ||
                (expertId != null && et.ExpertId == expertId) ||
                (topicId != null && et.TopicId == topicId));

            if (expertTopic == null)
                {
                return NotFound("ExpertTopic not found.");
                }

            expertTopic.IsDeleted = true;
            _context.ExpertTopics.Update(expertTopic);
            _context.SaveChanges();

            return Ok("ExpertTopic marked as deleted.");
            }
        }
    }
