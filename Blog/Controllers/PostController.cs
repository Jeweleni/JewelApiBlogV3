﻿using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Service;
using DomainLayer.Model;
using DomainLayer.PostDto;
using DomainLayer.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostService _ipostService;
        IMapper _imapper;

        public PostController(IPostService ipostService, IMapper imapper)
        {
            _ipostService = ipostService;
            _imapper = imapper;
        }

        //endpoint to get all posts
        [Authorize]
        [HttpGet]
        public IActionResult GetPost()
        {
            return Ok(_imapper.Map<List<PostDto>>(_ipostService.GetAllPost()));

        }

        //endpoint to get one post
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Post? post = _ipostService.GetPost(id);

            if (post == null)
            {
                return NotFound();
            }

            PostDto existingPost = _imapper.Map<PostDto>(post);

            return Ok(existingPost);
        }

        //endpoint to create post
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostDto postdto)
        {
            Post newPost = _imapper.Map<Post>(postdto);

            Post? createPost = _ipostService.CreatePost(newPost, out string message);

            if (createPost == null)
            {
                return BadRequest(message);
            }

            PostDto existingPost = _imapper.Map<PostDto>(createPost);

            return Ok(existingPost);
        }

        //endpoint to update a post
        [Authorize]
        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDto postdto)
        {
            Post existingPost = _imapper.Map<Post>(postdto);

            Post? postUpdate = _ipostService.UpdatePost(existingPost, out string message);

            if (postUpdate is null)
            {
                return BadRequest(message);
            }

            PostDto newPost = _imapper.Map<PostDto>(postUpdate);

            return Ok(newPost);
        }
        // delete post
        [Authorize]
        [HttpDelete]
        public IActionResult DeletePost(int id)

        {
            bool isDeleted = _ipostService.DeletePost(id, out string message);

            if (!isDeleted)
            {
                return BadRequest(message);
            }

            return Ok(new { Message = message });
        }
    }
}
