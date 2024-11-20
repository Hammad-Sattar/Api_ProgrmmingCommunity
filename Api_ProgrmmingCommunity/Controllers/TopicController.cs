using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private readonly ProgrammingCommunityContext _context;

    public TopicController(ProgrammingCommunityContext context)
    {
        _context = context;
    }

  
    [HttpPost("AddTopic")]
    public IActionResult AddTopic(TopicDTO topicDTO)
    {
        if (topicDTO == null)
        {
            return BadRequest("Topic data is null.");
        }

   
        if (!string.IsNullOrEmpty(topicDTO.SubjectCode) &&
            !_context.Subjects.Any(s => s.Code == topicDTO.SubjectCode))
        {
            return BadRequest($"Subject with code {topicDTO.SubjectCode} does not exist.");
        }

      
        var topic = new Topic
        {
            Title = topicDTO.Title,
            SubjectCode = topicDTO.SubjectCode
        };

   
        _context.Topics.Add(topic);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTopicById), new { id = topic.Id }, topic);
    }

  
    [HttpGet("GetTopicById/{id}")]
    public IActionResult GetTopicById(int id)
    {
        var topic = _context.Topics
            .Where(t => t.Id == id)
            .Select(t => new TopicDTO
            {
                Id = t.Id,
                Title = t.Title,
                SubjectCode = t.SubjectCode
            })
            .FirstOrDefault();

        if (topic == null)
        {
            return NotFound($"Topic with ID {id} not found.");
        }

        return Ok(topic);
    }
}
