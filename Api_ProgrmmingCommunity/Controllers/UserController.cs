using Api_ProgrmmingCommunity.Dto;
using Api_ProgrmmingCommunity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProgrmmingCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ProgrammingCommunityContext _context;

        public UserController(ProgrammingCommunityContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return BadRequest("Invalid login details.");
            }

            // Find the user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDTO.Email);

            if (user == null)
            {
                return Unauthorized("Email not found.");
            }

            // Check if the password matches
            if (user.Password != loginDTO.Password)
            {
                return Unauthorized("Incorrect password.");
            }

            // Create a response DTO
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum
            };

            return Ok(userDTO);
        }

        // POST: api/User
        [HttpPost("Signup")]
        public IActionResult CreateUser([FromBody] UserDTO userCreateDTO)
        {
            if (userCreateDTO == null)
            {
                return BadRequest("User data is null.");
            }

            var user = new User
            {
                Id=userCreateDTO.Id,
                Firstname=userCreateDTO.Firstname,
                Lastname=userCreateDTO.Lastname,
                Password = userCreateDTO.Password,
                Profimage = userCreateDTO.Profimage,
                Role = userCreateDTO.Role,
                RegNum = userCreateDTO.RegNum,
                Section = userCreateDTO.Section,
                Semester = userCreateDTO.Semester,
                Email = userCreateDTO.Email,
                Phonenum = userCreateDTO.Phonenum,
              

            };

        
            _context.Users.Add(user);
            _context.SaveChanges();

          
            var userDTO = new UserDTO
            {   Firstname=user.Firstname, 
                Lastname=user.Lastname,
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum,
              
            };

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDTO);
        }

        // GET: api/User
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();

            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            // Map the User entities to UserDTOs
            var userDTOs = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Firstname=user.Firstname,
                Lastname=user.Lastname,
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum,
             
            }).ToList();

            return Ok(userDTOs); // Return a 200 OK response with the list of users
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

        
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum,
               
            };

            return Ok(userDTO);
        }

   
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Firstname = userDTO.Firstname;
            user.Lastname = userDTO.Lastname;
            user.Password = userDTO.Password;
            user.Profimage = userDTO.Profimage;
            user.Role = userDTO.Role;

            user.RegNum = userDTO.RegNum;
            user.Section = userDTO.Section;
            user.Semester = userDTO.Semester;
            user.Email = userDTO.Email;
            user.Phonenum = userDTO.Phonenum;

            _context.Users.Update(user);
            _context.SaveChanges();

            var updatedUserDTO = new UserDTO
            {
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,

                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum
            };

            return Ok(new { Message = "User updated successfully.", User = updatedUserDTO });
        }

        // DELETE: api/User/{id}
        [HttpDelete("(\"DeleteUser\"){id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
