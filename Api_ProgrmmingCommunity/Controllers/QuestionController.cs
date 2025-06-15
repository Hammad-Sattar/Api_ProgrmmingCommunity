using Microsoft.AspNetCore.Mvc;
using MapProjectApi.Models;
using MapProjectApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;


namespace MapProjectApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public QuestionsController(ProgrammingCommunityContext context)
            {
            _context = context;
            }

        [HttpGet("GetAllQuestionsWithOption")]
        public async Task<ActionResult<IEnumerable<QuestionDtoList>>> GetAllQuestionss()
            {
            var questions = await _context.Questions
                .Select(q => new
                    {
                    q.Id,
                    q.SubjectCode,
                    q.TopicId,
                    q.UserId,
                    q.Difficulty,
                    q.Text,
                    q.Marks,
                    q.Type,
                    Options = q.Type == 2
                        ? _context.QuestionOptions.Where(opt => opt.QuestionId == q.Id)
                                                  .Select(opt => new
                                                      {
                                                      opt.Id,
                                                      opt.Option,
                                                      opt.IsCorrect
                                                      }).ToList()
                        : null
                    })
                .ToListAsync();

            var questionDtos = questions.Select(q => new QuestionDtoList
                {
                Id = q.Id,
                SubjectCode = q.SubjectCode,
                TopicId = q.TopicId,
                UserId = q.UserId,
                Difficulty = q.Difficulty,
                Text = q.Text,
                Type = q.Type,
                Marks = q.Marks,
                Options = q.Options != null ? q.Options.Select(opt => new QuestionOptionDTO
                    {
                    Id = opt.Id,
                    Option = opt.Option,
                    IsCorrect = opt.IsCorrect
                    }).ToList() : null
                }).ToList();

            return Ok(questionDtos);
        }
        [HttpPost("AddQuestionWithOptions")]
       public async Task<IActionResult> AddQuestionWithOptions([FromBody] QuestionDtoListVM model)
{
    if (model == null)
    {
        return BadRequest("Invalid data.");
    }

    // Create and save the question
    var question = new Question
    {
        SubjectCode = model.SubjectCode,
        TopicId = model.TopicId,
        UserId = model.UserId,
        Difficulty = model.Difficulty,
        Text = model.Text,
        Type = model.Type,
        Marks = model.Marks
    };

    _context.Questions.Add(question);
    await _context.SaveChangesAsync();  // Save to get the generated ID

    // If it's a multiple-choice question (type 2), save options
    if (model.Type == 2 && model.Options != null && model.Options.Any())
    {
        var options = model.Options.Select(opt => new QuestionOption
        {
            QuestionId = question.Id,
            Option = opt.Option,
            IsCorrect = opt.IsCorrect
        }).ToList();

        _context.QuestionOptions.AddRange(options);
        await _context.SaveChangesAsync();
    }

    // If it's a code-output question (type 3), save expected output
    if (model.Type == 3 && !string.IsNullOrWhiteSpace(model.Output))
    {
        var output = new QuestionOutput
        {
            QuestionId = question.Id,
            Output = model.Output
        };

        _context.QuestionOutputs.Add(output);
        await _context.SaveChangesAsync();
    }

    return Ok(new { message = "Question added successfully", QuestionId = question.Id });
}


        [HttpGet("GetAllQuestions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions()
            {
            var questions = await _context.Questions
                .Select(q => new QuestionDto
                    {
                    Id = q.Id,
                    SubjectCode = q.SubjectCode,
                    TopicId = q.TopicId,
                    UserId = q.UserId,
                    Difficulty = q.Difficulty,
                    Text = q.Text,
                    Type = q.Type,
                    Marks = q.Marks

                    })
                .ToListAsync();

            return Ok(questions);
        }

        [HttpGet("GetQuestionById/{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int id)
            {
            var question = await _context.Questions
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (question == null)
                {
                return NotFound();
                }

            return Ok(new QuestionDto
                {
                Id = question.Id,
                SubjectCode = question.SubjectCode,
                TopicId = question.TopicId,
                UserId = question.UserId,
                Difficulty = question.Difficulty,
                Text = question.Text,
                Type = question.Type,
                Marks = question.Marks
                });
        }
        [HttpPost("PostQuestion")]
        public async Task<ActionResult<QuestionDto>> PostQuestion(QuestionDto questionDto)
            {
            var question = new Question
                {
                SubjectCode = questionDto.SubjectCode,
                TopicId = questionDto.TopicId,
                UserId = questionDto.UserId,
                Difficulty = questionDto.Difficulty,
                Text = questionDto.Text,
                Type = questionDto.Type,
                Marks = questionDto.Marks
                };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            questionDto.Id = question.Id;

            return Ok(question.Id);
            }

        [HttpDelete("DeleteQuestionWithOptions/{id}")]
        public async Task<ActionResult> DeleteQuestionWithOptions(int id)
            {
            var options = await _context.QuestionOptions
                .Where(opt => opt.QuestionId == id)
                .ToListAsync();

            string responseMessage = string.Empty;

            if (options.Any())
                {
                _context.QuestionOptions.RemoveRange(options);
                responseMessage += "Options deleted. ";
                }
            else
                {
                responseMessage += "Question Have No Option ";
                }

            var question = await _context.Questions
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (question == null)
                {
                return NotFound(new { message = "Question not found" });
                }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            responseMessage += "Question deleted.";

            return Ok(new { message = responseMessage });
            }

        [HttpDelete("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
            {
            var question = await _context.Questions
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (question == null)
                {
                return NotFound();
                }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("UpdateQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionDto questionDto)
            {
            var question = await _context.Questions
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (question == null)
                {
                return NotFound();
                }

            question.SubjectCode = questionDto.SubjectCode ?? question.SubjectCode;
            question.TopicId = questionDto.TopicId ?? question.TopicId;
            question.UserId = questionDto.UserId ?? question.UserId;
            question.Difficulty = questionDto.Difficulty ?? question.Difficulty;
            question.Text = questionDto.Text ?? question.Text;
            question.Type = questionDto.Type ?? question.Type;
            question.Marks = questionDto.Marks ?? question.Marks;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            return NoContent();
            }

        [HttpGet("GetAllShuffleQuestion")]
        public IActionResult GetAllShuffleQuestion()
            {
            var random = new Random();

            var questions = _context.Questions
                .Where(q => q.Type == 3 && q.IsDeleted == false)
                .AsEnumerable()  
                .Select(q => new
                    {
                    q.Id,
                    q.SubjectCode,
                    q.TopicId,
                    q.UserId,
                    q.Difficulty,
                    q.Type,
                    q.Marks,
                    Lines = q.Text != null
                        ? q.Text.Split(new[] { "\\n" }, StringSplitOptions.None)
                                .OrderBy(_ => random.Next())
                                .ToList()
                        : new List<string>()
                    
                    })
                .ToList();

            return Ok(questions);
            }

        [HttpGet("GetQuestionOutput/{questionId}")]
        public async Task<IActionResult> GetQuestionOutput(int questionId)
            {
            var output = await _context.QuestionOutputs
                .Where(q => q.QuestionId == questionId)
                .Select(q => new { q.Output })
                .FirstOrDefaultAsync();

            if (output == null)
                {
                return NotFound(new { Message = "Output not found for this question ID." });
                }

            return Ok(output);
            }
        }

    }
    
    
