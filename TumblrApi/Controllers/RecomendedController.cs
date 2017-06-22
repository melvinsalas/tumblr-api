using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers
{
	[Route("api/[controller]")]
	public class RecommendedController : Controller
	{
		MongoDBContext _context;
		//private readonly UserManager<ApplicationUser> _userManager;

		public RecommendedController()
		{
			_context = new MongoDBContext();
		}

		[HttpGet]
		public List<User> GetRadar()
		{
            var rnd = new Random();
			String username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = _context.Users.Find(m => m.UserName != username).ToList();

			if (users.Count() > 4)
			{
                users = users.OrderBy(_ => Guid.NewGuid()).Take(4).ToList();
            }
		
			return users;
		}
	}
}