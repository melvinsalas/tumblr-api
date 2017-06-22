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
    public class AuthController : Controller
    {
        MongoDBContext _context;

        public AuthController() 
        {
            _context = new MongoDBContext();
        }

        [HttpGet]
        public Auth GetAuth()
		{
			String username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _context.Users.Find(m => m.UserName == username).FirstOrDefault();

            Auth auth = new Auth()
            {
                Name = user.Name,
                UserName = user.UserName,
                Photo = user.Photo
            };

            return auth;
        }
    }
}