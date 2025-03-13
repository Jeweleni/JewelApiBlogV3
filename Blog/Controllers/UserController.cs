using AutoMapper;
using BusinessLogicLayer.IServices;
using DomainLayer.Model;
using DomainLayer.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _imapper;

        public UserController(IUserService userService, IMapper imapper)
        {
            _userService = userService;
            _imapper = imapper;
        }

        // ✅ Login endpoint (returns JWT token)
     
        // ✅ Get all users
        [Authorize]
        [HttpGet]


        public IActionResult GetUsers()
        {
            return Ok(_imapper.Map<List<UserDto>>(_userService.GetAllUsers()));
        }

        // ✅ Get a single user by ID
        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_imapper.Map<UserDto>(user));
        }

        // ✅ Create a user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var createdUser = await _userService.CreateUser(userDto);
            if (createdUser == null)
            {
                return BadRequest("User creation failed.");
            }

            return Ok(_imapper.Map<UserDto>(createdUser));
        }

        // ✅ Update user
        [Authorize]
        [HttpPut]

        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            var existingUser = _imapper.Map<User>(userDto);
            var updatedUser = await _userService.UpdateUser(existingUser);

            if (updatedUser == null)
            {
                return BadRequest();
            }

            return Ok(_imapper.Map<UserDto>(updatedUser));
        }

        // ✅ Delete user
        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(string id)
        {
            bool isDeleted = await _userService.DeleteUser(id);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(new { Message = "Deleted successfully" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var token = await _userService.LoginUser(loginDto.Email, loginDto.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { Token = token });
        }



    }


}
