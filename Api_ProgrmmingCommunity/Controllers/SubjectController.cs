using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
    private readonly ProgrammingCommunityContext _context;

    public SubjectController(ProgrammingCommunityContext context)
    {
        _context = context;
    }

    // GET: api/Subject
    [HttpGet]
    public IActionResult GetSubjects()
    {
        var subjects = _context.Subjects
            .Select(s => new SubjectDTO
            {
                Title = s.Title,
                Code = s.Code,
            }).ToList();

        return Ok(subjects);
    }

    // GET: api/Subject/{Code}
    [HttpGet("{Code}")]
    public IActionResult GetSubjectByCode(int Code)
    {
        var subject = _context.Subjects
            .Where(s => s.Code == Code)
            .Select(s => new SubjectDTO
            {
                Title = s.Title,
                Code = s.Code,
            })
            .FirstOrDefault();

        if (subject == null)
        {
            return NotFound($"Subject with Code {Code} not found.");
        }

        return Ok(subject);
    }

    // POST: api/Subject
    [HttpPost]
    public IActionResult CreateSubject([FromBody] SubjectDTO subjectDTO)
    {
        if (subjectDTO == null)
        {
            return BadRequest("Subject data is null.");
        }

        var subject = new Subject
        {
            Title = subjectDTO.Title,
            Code = subjectDTO.Code,
        };

        _context.Subjects.Add(subject);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetSubjectByCode), new { Code = subject.Code }, new { Message = "Subject added successfully.", Subject = subjectDTO });
    }

    // PUT: api/Subject/{Code}
    [HttpPut("{Code}")]
    public IActionResult UpdateSubject(int Code, [FromBody] SubjectDTO subjectDTO)
    {
        if (subjectDTO == null)
        {
            return BadRequest("Subject data is null.");
        }

        var subject = _context.Subjects.FirstOrDefault(s => s.Code == Code);
        if (subject == null)
        {
            return NotFound($"Subject with Code {Code} not found.");
        }

        subject.Title = subjectDTO.Title;
        subject.Code = subjectDTO.Code;

        _context.Subjects.Update(subject);
        _context.SaveChanges();

        return Ok(new { Message = "Subject updated successfully.", Subject = subjectDTO });
    }

    // DELETE: api/Subject/{Code}
    [HttpDelete("{Code}")]
    public IActionResult DeleteSubject(int Code)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Code == Code);
        if (subject == null)
        {
            return NotFound($"Subject with Code {Code} not found.");
        }

        _context.Subjects.Remove(subject);
        _context.SaveChanges();

        return Ok(new { Message = "Subject deleted successfully." });
    }
}
