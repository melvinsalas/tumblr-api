using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		MongoDBContext _context;
		public UsersController()
		{
			_context = new MongoDBContext();
		}

		[HttpGet]
		public IEnumerable<User> GetAll()
		{
			return _context.Users.Find(m => true).ToList();
		}

		[HttpGet("{id}", Name = "GetUser")]
		public IActionResult GetById(String id)
		{
			var user = _context.Users.Find(m => m.Id == id).FirstOrDefault();
			if (user == null) return NotFound();
			return new ObjectResult(user);
		}

		[HttpPost]
		public IActionResult Create([FromBody] User user)
		{
			if (user == null) return BadRequest();
            var search = _context.Users.Find(m => m.UserName == user.UserName).FirstOrDefault();
            if (search != null) return StatusCode(409); // Conflict
			_context.Users.InsertOne(user);
			return CreatedAtRoute("GetUser", new { id = user.Id }, user);
		}
	}
}