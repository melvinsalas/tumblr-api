using System;
using System.Collections.Generic;
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
			return _context.Posts.Find(m => true).ToList();
		}

		[HttpGet("{id}", Name = "GetPost")]
		public IActionResult GetById(String id)
		{
            var post = _context.Posts.Find(m => m.Id == id).FirstOrDefault();
			if (post == null) return NotFound();
			return new ObjectResult(post);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Post post)
		{
			if (post == null) return BadRequest();
			var user = _context.Users.Find(m => m.Id == post.UserId).FirstOrDefault();
            if (user == null) return StatusCode(409);
			_context.Posts.InsertOne(post);
			return CreatedAtRoute("GetPost", new { id = post.Id }, post);
		}
    }
}