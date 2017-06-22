using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers
{
	[Route("api/[controller]")]
	public class BlogController : Controller
	{
		MongoDBContext _context;
		public BlogsController()
		{
			_context = new MongoDBContext();
		}

		[HttpGet]
		public IEnumerable<Blog> GetAll()
		{
			return _context.Blogs.Find(m => true).ToList();
		}

		[HttpGet("{id}", Name = "GetBlog")]
		public IActionResult GetById(String id)
		{
			var blog = _context.Blogs.Find(m => m.Id == id).FirstOrDefault();
			if (blog == null) return NotFound();
			return new ObjectResult(blog);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Blog blog)
		{
			if (blog == null) return BadRequest();
			_context.Blogs.InsertOne(blog);
			return CreatedAtRoute("GetBlog", new { id = blog.Id }, blog);
		}
	}
}