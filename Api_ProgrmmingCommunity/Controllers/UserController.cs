﻿using Api_ProgrmmingCommunity.Dto;
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

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userCreateDTO)
        {
            if (userCreateDTO == null)
            {
                return BadRequest("User data is null.");
            }

            // Map the DTO to the User model
            var user = new User
            {
                Password = userCreateDTO.Password,
                Profimage = userCreateDTO.Profimage,
                Role = userCreateDTO.Role,
               
                RegNum = userCreateDTO.RegNum,
                Section = userCreateDTO.Section,
                Semester = userCreateDTO.Semester,
                Email = userCreateDTO.Email,
                Phonenum = userCreateDTO.Phonenum
            };

            // Add the user to the database
            _context.Users.Add(user);
            _context.SaveChanges();

            // Return a 201 Created response with the created UserDTO
            var userDTO = new UserDTO
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

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDTO);
        }

        // GET: api/User
        [HttpGet]
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
                Id=user.Id,
                Password = user.Password,
                Profimage = user.Profimage,
                Role = user.Role,
               
                RegNum = user.RegNum,
                Section = user.Section,
                Semester = user.Semester,
                Email = user.Email,
                Phonenum = user.Phonenum
            }).ToList();

            return Ok(userDTOs); // Return a 200 OK response with the list of users
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // Map the User entity to a UserDTO for returning the data
            var userDTO = new UserDTO
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

            return Ok(userDTO);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
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

            // Update user properties
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

            // Return a message with the updated user details
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
        [HttpDelete("{id}")]
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