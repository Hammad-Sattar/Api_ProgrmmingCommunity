using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertSubjectController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public ExpertSubjectController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddExpertSubject")]
        public IActionResult AddExpertSubject([FromBody] ExpertSubjectDTO expertSubjectDto)
            {
            if (expertSubjectDto == null)
                {
                return BadRequest("ExpertSubject data is null.");
                }

            var expertSubject = new ExpertSubject
                {
                ExpertId = expertSubjectDto.ExpertId,
                SubjectCode = expertSubjectDto.SubjectCode,
                IsDeleted = false
                };

            _context.ExpertSubjects.Add(expertSubject);
            _context.SaveChanges();

            return Ok("Subject Added");
            }

        [HttpGet("GetExpertSubject")]
        public IActionResult GetExpertSubject([FromQuery] int? id, [FromQuery] int? expertId, [FromQuery] string? subjectCode)
            {
            var query = _context.ExpertSubjects
                .Where(es => es.IsDeleted == false)  
                .Join(_context.Subjects.Where(s => s.IsDeleted == false),
                      es => es.SubjectCode,
                      s => s.Code,
                      (es, s) => new
                          {
                          Id = es.Id,
                          ExpertId = es.ExpertId,
                          SubjectCode = es.SubjectCode,
                          Title = s.Title
                          });

            if (id != null)
                query = query.Where(es => es.Id == id);

            if (expertId != null)
                query = query.Where(es => es.ExpertId == expertId);

            if (subjectCode != null)
                query = query.Where(es => es.SubjectCode == subjectCode);

            var result = query.ToList();

            if (!result.Any())
                return NotFound("No matching ExpertSubject found or it is marked as deleted.");

            return Ok(result);
            }
       



        [HttpGet("GetAllExpertSubjects")]
        public IActionResult GetAllExpertSubjects()
            {
            var expertSubjects = _context.ExpertSubjects
                .Where(es => es.IsDeleted == false)
                .Select(es => new ExpertSubjectDTO
                    {
                    Id = es.Id,
                    ExpertId = es.ExpertId,
                    SubjectCode = es.SubjectCode,
                   
                    })
                .ToList();

            return Ok(expertSubjects);
            }

        [HttpDelete("DeleteExpertSubject")]
        public IActionResult DeleteExpertSubject([FromQuery] int? id, [FromQuery] int? expertId, [FromQuery] string? subjectCode)
            {
            var expertSubject = _context.ExpertSubjects.FirstOrDefault(es =>
                (id != null && es.Id == id) ||
                (expertId != null && es.ExpertId == expertId) ||
                (subjectCode != null && es.SubjectCode == subjectCode));

            if (expertSubject == null)
                {
                return NotFound("ExpertSubject not found.");
                }

            expertSubject.IsDeleted = true;
            _context.ExpertSubjects.Update(expertSubject);
            _context.SaveChanges();

            return Ok("ExpertSubject marked as deleted.");
            }
        }
    }
