using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public SubjectController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddSubject")]
        public IActionResult AddSubject([FromBody] SubjectDTO subjectDto)
            {
            if (subjectDto == null)
                {
                return BadRequest("Subject data is null.");
                }

            var subject = new Subject
                {
                Code = subjectDto.Code,
                Title = subjectDto.Title,
                IsDeleted = false
                };

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSubject), new { code = subject.Code }, subject);
            }

        [HttpGet("GetSubject")]
        public IActionResult GetSubject([FromQuery] string? code, [FromQuery] string? title)
            {
            var subject = _context.Subjects.FirstOrDefault(s =>
                s.IsDeleted == false &&
                ((code != null && s.Code == code) ||
                 (title != null && s.Title == title)));

            if (subject == null)
                {
                return NotFound("Subject not found or is marked as deleted.");
                }

            var subjectDto = new SubjectDTO
                {
                Code = subject.Code,
                Title = subject.Title,
               
                };

            return Ok(subjectDto);
            }

        [HttpGet("GetAvailableSubjects")]
        public IActionResult GetAllSubjects(int userId)
            {
           
            var registeredSubjects = _context.ExpertSubjects
                .Where(es => es.ExpertId == userId && es.IsDeleted == false)
                .Select(es => es.SubjectCode)
                .ToList();

           
            var subjects = _context.Subjects
                .Where(s => s.IsDeleted == false && !registeredSubjects.Contains(s.Code))
                .Select(s => new SubjectDTO
                    {
                    Code = s.Code,
                    Title = s.Title,
                    }).ToList();

            return Ok(subjects);
            }


        [HttpGet("GetAllSubjects")]
        public IActionResult GetAllSubjects()
            {
            var subjects = _context.Subjects
                .Where(s => s.IsDeleted == false)
                .Select(s => new SubjectDTO
                    {
                    Code = s.Code,
                    Title = s.Title,
                   
                    }).ToList();

            return Ok(subjects);
            }

        [HttpDelete("DeleteSubject")]
        public IActionResult DeleteSubject([FromQuery] string? code, [FromQuery] string? title)
            {
            var subject = _context.Subjects.FirstOrDefault(s =>
                (code != null && s.Code == code) ||
                (title != null && s.Title == title));

            if (subject == null)
                {
                return NotFound("Subject not found.");
                }

            subject.IsDeleted = true;
            _context.Subjects.Update(subject);
            _context.SaveChanges();

            return Ok("Subject marked as deleted.");
            }
        }
    }
