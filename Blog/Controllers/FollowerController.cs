using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Service;
using DomainLayer.CommentDto;
using DomainLayer.DTO;
using DomainLayer.FollowerDto;
using DomainLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        IFollowerService _followerService;
        IMapper _imapper;

        public FollowerController(IFollowerService followerService, IMapper imapper)
        {
            _followerService = followerService;
            _imapper = imapper;
        }

        //endpoint to get all Followers
        [Authorize]
        [HttpGet]
        public IActionResult GetFollower()
        {

            return Ok(_imapper.Map<List<FollowerDto>>(_followerService.GetAllFollower()));

        }

        //endpoint to create Follower
        [Authorize]
        [HttpPost]
        public IActionResult CreateFollower([FromBody] CreateFollowerDto Followerdto)
        {
            Follower newFollower = _imapper.Map<Follower>(Followerdto);

            Follower? creaeFollower = _followerService.CreateFollower(newFollower, out string message);

            if (creaeFollower == null)
            {
                return BadRequest(message);
            }

            FollowerDto existingFollower = _imapper.Map<FollowerDto>(creaeFollower);

            return Ok(existingFollower);
        }

        //endpoint to delete a Follower
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteFollower(int id) // Add the parameter type (int)
        {
            // Call the service to delete the Follower
            bool isDeleted = _followerService.DeleteFollower(id, out string message);

            // If deletion fails, return a BadRequest with the error message
            if (!isDeleted)
            {
                return BadRequest(message);
            }

            // Return a success response with the message
            return Ok(new { Message = message });
        }
    }
}
