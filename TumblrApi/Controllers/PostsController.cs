using System;
using System.Collections.Generic;
using System.Linq;
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
			var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Find(m => m.Email == username).FirstOrDefault();
            if (user.Following == null) user.Following = new string[0];

			return _context.Posts
                           .Find(m => (m.UserId == user.Id || user.Following.Contains(m.UserId)))
                           .ToList().OrderByDescending(m => m.Publish);
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
            post.Publish = DateTime.Now;
			_context.Posts.InsertOne(post);
			return CreatedAtRoute("GetPost", new { id = post.Id }, post);
		}

        public string getUserId() 
        {
			var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var users = _context.Users.Find(m => m.Email == username);
            var user = users.FirstOrDefault();
            if(user == null) return "";
			return user.Id;
        }
    }
}