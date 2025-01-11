using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public QuestionOptionController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpPost("AddOption")]
        public IActionResult AddOption([FromBody] QuestionOptionDTO optionDto)
            {
            if (optionDto == null)
                {
                return BadRequest("Option data is null.");
                }

            var option = new QuestionOption
                {
                QuestionId = optionDto.QuestionId,
                Option = optionDto.Option,
                IsCorrect = optionDto.IsCorrect,
                IsDeleted = false
                };

            _context.QuestionOptions.Add(option);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOption), new { id = option.Id }, option);
            }

        [HttpGet("GetOption")]
        public IActionResult GetOption([FromQuery] int? id, [FromQuery] int? questionId)
            {
            var option = _context.QuestionOptions.FirstOrDefault(o =>
                o.IsDeleted == false &&
                ((id != null && o.Id == id) || (questionId != null && o.QuestionId == questionId)));

            if (option == null)
                {
                return NotFound("Option not found or is marked as deleted.");
                }

            return Ok(new QuestionOptionDTO
                {
                Id = option.Id,
                QuestionId = option.QuestionId,
                Option = option.Option,
                IsCorrect = option.IsCorrect,
                IsDeleted = option.IsDeleted
                });
            }

        [HttpGet("GetAllOptions")]
        public IActionResult GetAllOptions()
            {
            var options = _context.QuestionOptions
                .Where(o => o.IsDeleted == false)
                .Select(o => new QuestionOptionDTO
                    {
                    Id = o.Id,
                    QuestionId = o.QuestionId,
                    Option = o.Option,
                    IsCorrect = o.IsCorrect,
                    IsDeleted = o.IsDeleted
                    })
                .ToList();

            return Ok(options);
            }

        [HttpDelete("DeleteOption")]
        public IActionResult DeleteOption([FromQuery] int? id, [FromQuery] int? questionId)
            {
            var option = _context.QuestionOptions.FirstOrDefault(o =>
                (id != null && o.Id == id) ||
                (questionId != null && o.QuestionId == questionId));

            if (option == null)
                {
                return NotFound("Option not found.");
                }

            option.IsDeleted = true;
            _context.QuestionOptions.Update(option);
            _context.SaveChanges();

            return Ok("Option marked as deleted.");
            }
        }
    }
