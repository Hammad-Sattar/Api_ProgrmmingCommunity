using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [ApiController]
    [Route("api/[controller]")]
    public class BuzzerController : ControllerBase
        {
        private static readonly ConcurrentDictionary<int, BuzzerPress> _buzzerPresses = new ConcurrentDictionary<int, BuzzerPress>();
        private readonly ProgrammingCommunityContext _dbContext;

        public BuzzerController(ProgrammingCommunityContext dbContext)
            {
            _dbContext = dbContext;
            }

        [HttpPost("press")]
        public async Task<IActionResult> PressBuzzer([FromBody] BuzzerPressRequest request)
            {
            var team = await _dbContext.Teams.FindAsync(request.TeamId);
            if (team == null) return NotFound("Team not found");

            var question = await _dbContext.Questions.FindAsync(request.QuestionId);
            if (question == null) return NotFound("Question not found");

            var pressTime = DateTime.UtcNow;

            // ✅ Save each press attempt
            var log = new BuzzerPressLog
                {
                QuestionId = request.QuestionId,
                TeamId = request.TeamId,
                TeamName = team.TeamName,
                PressTime = pressTime
                };
            _dbContext.BuzzerPressLogs.Add(log);
            await _dbContext.SaveChangesAsync();

            // ✅ Check if any global press exists
            var existingGlobalPress = _buzzerPresses.Values.FirstOrDefault();
            if (existingGlobalPress != null)
                {
                return Ok(new BuzzerPressResponse
                    {
                    Success = false,
                    FirstPressTeamId = existingGlobalPress.TeamId,
                    FirstPressTeamName = existingGlobalPress.TeamName,
                    PressTime = existingGlobalPress.PressTime,
                    QuestionId = existingGlobalPress.QuestionId
                    });
                }

            // ✅ Register global first press
            var newPress = new BuzzerPress
                {
                QuestionId = request.QuestionId,
                TeamId = request.TeamId,
                TeamName = team.TeamName,
                PressTime = pressTime
                };
            _buzzerPresses.TryAdd(request.QuestionId, newPress);

            return Ok(new BuzzerPressResponse
                {
                Success = true,
                FirstPressTeamId = request.TeamId,
                FirstPressTeamName = team.TeamName,
                PressTime = pressTime,
                QuestionId = request.QuestionId
                });
            }

        [HttpPost("reset")]
        public IActionResult ResetBuzzer()
            {
            _buzzerPresses.Clear();
            return Ok();
            }

       /* [HttpPost("reset/{questionId}")]
        public IActionResult ResetBuzzer(int questionId)
            {
            _buzzerPresses.TryRemove(questionId, out _);
            return Ok();
            }*/

        [HttpGet("status")]
        public IActionResult GetCurrentPress()
            {
            var press = _buzzerPresses.Values.FirstOrDefault();
            if (press != null)
                {
                return Ok(press);
                }
            return NotFound("No active buzzer press");
            }

       /* [HttpGet("status/{questionId}")]
        public IActionResult GetBuzzerStatus(int questionId)
            {
            if (_buzzerPresses.TryGetValue(questionId, out var press))
                {
                return Ok(press);
                }
            return NotFound("No press recorded for this question");
            }*/

        [HttpPost("nextquestion")]
        public IActionResult MoveToNextQuestion()
            {
            _buzzerPresses.Clear();
            return Ok();
            }
        }

    // Model classes
    public class BuzzerPressRequest
        {
        public int TeamId { get; set; }
        public int QuestionId { get; set; }
        }

    public class BuzzerPressResponse
        {
        public bool Success { get; set; }
        public int FirstPressTeamId { get; set; }
        public string FirstPressTeamName { get; set; }
        public DateTime PressTime { get; set; }
        public int QuestionId { get; set; }
        }

    public class BuzzerPress
        {
        public int QuestionId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime PressTime { get; set; }
        }
    }