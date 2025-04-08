using Microsoft.AspNetCore.Mvc;
using Api_ProgrmmingCommunity.Models;
using System.Linq;
using Api_ProgrmmingCommunity.Dto;
using MapProjectApi.Models.DTOs;

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
        [HttpGet("GetOptionsByQuestionId")]
        public IActionResult GetOptionsByQuestionId([FromQuery] int questionId)
            {
           
            var question = _context.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
                {
                return NotFound("The question does not exist.");
                }

            if (question.Type != 2)
                {
                return BadRequest("The question does not have options as its type is not Mcq.");
                }

           
            var options = _context.QuestionOptions
                .Where(o => o.IsDeleted == false && o.QuestionId == questionId)
                .ToList();

            if (!options.Any())
                {
                return NotFound("No options found for the given question ID or they are marked as deleted.");
                }

            var optionDtos = options.Select(o => new QuestionOptionDTO
                {
               Id = o.Id,
                QuestionId = o.QuestionId,
                Option = o.Option,
                IsCorrect = o.IsCorrect,
               
                }).ToList();

            return Ok(optionDtos);
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
            
                QuestionId = option.QuestionId,
                Option = option.Option,
                IsCorrect = option.IsCorrect,
            
                });
            }

        [HttpGet("GetAllOptions")]
        public IActionResult GetAllOptions()
            {
            var options = _context.QuestionOptions
                .Where(o => o.IsDeleted == false)
                .Select(o => new QuestionOptionDTO
                    {
                  
                    QuestionId = o.QuestionId,
                    Option = o.Option,
                    IsCorrect = o.IsCorrect,
                   
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
