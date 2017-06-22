using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers
{
	[Route("api/[controller]")]
	public class MediaController : Controller
	{
		MongoDBContext _context;

		public MediaController()
		{
			_context = new MongoDBContext();
		}

		[HttpPost]
		public IActionResult Create([FromBody] MediaFile file)
		{
			if (file == null) return BadRequest();
            return Json("{ok}");
		}
	}
}