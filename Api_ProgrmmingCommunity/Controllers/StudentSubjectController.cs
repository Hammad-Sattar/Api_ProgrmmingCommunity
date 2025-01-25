using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public StudentSubjectController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddStudentSubject")]
        public IActionResult AddStudentSubject([FromBody] StudentSubjectDTO studentSubjectDto)
            {
            if (studentSubjectDto == null)
                {
                return BadRequest("StudentSubject data is null.");
                }

            var studentSubject = new StudentSubject
                {
                StudentId = studentSubjectDto.StudentId,
                SubjectCode = studentSubjectDto.SubjectCode,
                IsDeleted = false
                };

            _context.StudentSubjects.Add(studentSubject);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStudentSubject), new { id = studentSubject.Id }, studentSubject);
            }

        [HttpGet("GetStudentSubject")]
        public IActionResult GetStudentSubject([FromQuery] int? id, [FromQuery] int? studentId, [FromQuery] string? subjectCode)
            {
            var studentSubject = _context.StudentSubjects.FirstOrDefault(ss =>
                ss.IsDeleted == false &&
                ((id != null && ss.Id == id) ||
                 (studentId != null && ss.StudentId == studentId) ||
                 (subjectCode != null && ss.SubjectCode == subjectCode)));

            if (studentSubject == null)
                {
                return NotFound("StudentSubject not found or is marked as deleted.");
                }

            return Ok(new StudentSubjectDTO
                {
                Id = studentSubject.Id,
                StudentId = studentSubject.StudentId,
                SubjectCode = studentSubject.SubjectCode,
               
                });
            }

        [HttpGet("GetAllStudentSubjects")]
        public IActionResult GetAllStudentSubjects()
            {
            var studentSubjects = _context.StudentSubjects
                .Where(ss => ss.IsDeleted == false)
                .Select(ss => new StudentSubjectDTO
                    {
                    Id = ss.Id,
                    StudentId = ss.StudentId,
                    SubjectCode = ss.SubjectCode,
                   
                    })
                .ToList();

            return Ok(studentSubjects);
            }

        [HttpDelete("DeleteStudentSubject")]
        public IActionResult DeleteStudentSubject([FromQuery] int? id, [FromQuery] int? studentId, [FromQuery] string? subjectCode)
            {
            var studentSubject = _context.StudentSubjects.FirstOrDefault(ss =>
                (id != null && ss.Id == id) ||
                (studentId != null && ss.StudentId == studentId) ||
                (subjectCode != null && ss.SubjectCode == subjectCode));

            if (studentSubject == null)
                {
                return NotFound("StudentSubject not found.");
                }

            studentSubject.IsDeleted = true;
            _context.StudentSubjects.Update(studentSubject);
            _context.SaveChanges();

            return Ok("StudentSubject marked as deleted.");
            }
        }
    }
