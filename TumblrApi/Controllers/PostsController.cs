using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers 
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        MongoDBContext _context;

        public PostsController()
        {
            _context = new MongoDBContext();
        }

        [HttpGet]
		public IEnumerable<Post> GetAll()
		{
			return _context.Posts.Find(m => m.UserId == getUserId()).ToList();
		}

		[HttpGet("{id}", Name = "GetPost")]
		public IActionResult GetById(String id)
		{
            var posts = _context.Posts.Find(m => m.Id == id && m.UserId == getUserId());
            var post = posts.FirstOrDefault();
			if (post == null) return NotFound();
			return new ObjectResult(post);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Post post)
		{
			if (post == null) return BadRequest();
			if (getUserId() == null) return StatusCode(409);
            post.UserId = getUserId();
			_context.Posts.InsertOne(post);
			return CreatedAtRoute("GetPost", new { id = post.Id }, post);
		}

        public string getUserId() 
        {
			var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var users = _context.Users.Find(m => m.UserName == username);
            var user = users.FirstOrDefault();
            if(user == null) return "";
			return user.Id;
        }
    }
}