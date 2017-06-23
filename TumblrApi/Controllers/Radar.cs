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
    public class RadarController : Controller
    {
        MongoDBContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;

        public RadarController()
        {
            _context = new MongoDBContext();
        }

        [HttpGet]
        public Radar GetRadar()
        {
            var loggedUser = _context.Users
                                     .Find(m => m.Email == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                                     .FirstOrDefault();

            var post = _context.Posts
                               .Find(m => m.UserId != loggedUser.Id && m.Media.Type == "photo")
                               .ToList()
                               .OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
            if (post == null) return null;

            var user = _context.Users
                               .Find(m => m.Id == post.UserId).FirstOrDefault();
            if (user == null) return null;

            var radar = new Radar() {
                User = user,
                Post = post
            };

            return radar;
        }
    }
}