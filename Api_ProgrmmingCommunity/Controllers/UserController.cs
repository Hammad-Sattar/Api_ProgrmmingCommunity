using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProgrmmingCommunity.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
        {
        private readonly ProgrammingCommunityContext _context;

        public UserController(ProgrammingCommunityContext context)
            {
            _context = context;
            }


        [HttpPost("Login")]
       
        public IActionResult Login([FromBody] LoginDTO loginDto)
            {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                {
                return BadRequest("Email or password cannot be empty.");
                }

            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password && u.IsDeleted == false);

            if (user == null)
                {
                return Unauthorized("Invalid credentials or user is deleted.");
                }

            var userDto = new UserDTO
                {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Phonenum = user.Phonenum,
               
                Role = user.Role
                };

            if (user.Role == 1)
                {
                userDto.Password = user.Password;
                return Ok(userDto);
                }
            else if (user.Role == 2)
                {
                userDto.Empid = user.Empid;
                return Ok(userDto);
                }
            else if (user.Role == 3 || user.Role == 4)
                {
                userDto.Section = user.Section;
                userDto.RegNum = user.RegNum;
                return Ok(userDto);
                }

            return Ok(userDto);
            }



        [HttpPost("RegisterUser")]
        public IActionResult CreateUser([FromBody] UserDTO userDto)
            {
            if (userDto == null)
                {
                return BadRequest("User data is null.");
                }

            
            if (_context.Users.Any(u => u.Email == userDto.Email && u.IsDeleted == false))
                {
                return Conflict("A user with the same Email already exists.");
                }

          
            if (!string.IsNullOrEmpty(userDto.Empid) &&
                _context.Users.Any(u => u.Empid == userDto.Empid && u.IsDeleted == false))
                {
                return Conflict("A user with the same Employee ID already exists.");
                }

           
            if (!string.IsNullOrEmpty(userDto.RegNum) &&
                _context.Users.Any(u => u.RegNum == userDto.RegNum && u.IsDeleted == false))
                {
                return Conflict("A user with the same Registration Number already exists.");
                }

           
            if (!string.IsNullOrEmpty(userDto.Phonenum) &&
                _context.Users.Any(u => u.Phonenum == userDto.Phonenum && u.IsDeleted == false))
                {
                return Conflict("A user with the same Phone Number already exists.");
                }

         
            var user = new User
                {
                Password = userDto.Password,
               
                Role = userDto.Role,
                RegNum = userDto.RegNum,
                Section = userDto.Section,
                Semester = userDto.Semester,
                Email = userDto.Email,
                Phonenum = userDto.Phonenum,
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,
                Empid = userDto.Empid
               
                };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
            }



        [HttpGet("GetUser")]
        public IActionResult GetUser([FromQuery] string? email, [FromQuery] string? empId, [FromQuery] string? regNum)
            {
           
            var user = _context.Users.FirstOrDefault(u =>
                u.IsDeleted == false && 
                ((email != null && u.Email == email) ||
                 (empId != null && u.Empid == empId) ||
                 (regNum != null && u.RegNum == regNum)));

            if (user == null)
                {
                return NotFound("User not found or user is marked as deleted.");
                }

            var userDto = new UserDTO
                {
                Id = user.Id,
                Password = user.Password,
               
                Role = user.Role,
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Empid = user.Empid,
              
                };

            return Ok(userDto);
            }

      
        [HttpGet("GetAllUser")]
        public IActionResult GetAllUsers()
            {
           
            var users = _context.Users
                .Where(user => user.IsDeleted == false) 
                .Select(user => new UserDTO
                    {
                    Id = user.Id,
                    Password = user.Password,
                   
                    Role = user.Role,
                    RegNum = user.RegNum,
                    Section = user.Section,
                    Semester = user.Semester,
                    Email = user.Email,
                    Phonenum = user.Phonenum,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Empid = user.Empid,
                  
                    }).ToList();

            return Ok(users);
            }

      
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser([FromQuery] string? email, [FromQuery] string? empId, [FromQuery] string? regNum)
            {
          
            var user = _context.Users.FirstOrDefault(u =>
                (email != null && u.Email == email) ||
                (empId != null && u.Empid == empId) ||
                (regNum != null && u.RegNum == regNum));

            if (user == null)
                {
                return NotFound("User not found.");
                }

            
            user.IsDeleted = true;
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok("User marked as deleted.");
            }
        }
    }
