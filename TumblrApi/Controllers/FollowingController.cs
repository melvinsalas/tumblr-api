using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers 
{
    [Route("api/[controller]")]
    public class FollowingController : Controller
    {
        MongoDBContext _context;
        public FollowingController() 
        {
            _context = new MongoDBContext();
        }

        [HttpPost]
		public async Task<UpdateResult> AddFollowing([FromBody] Following following)
		{
            User user = getUser();

			var followings = new List<string>(user.Following);
            if (!followings.Contains(following.Id)) followings.Add(following.Id);

			var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            var update = Builders<User>.Update
                                       .Set(s => s.Following, followings.ToArray());
			return await _context.Users.UpdateOneAsync(filter, update);
		}

		[HttpDelete]
		public async Task<UpdateResult> RemoveFollowing([FromBody] Following following)
		{
			User user = getUser();

			var followings = new List<string>(user.Following);
			followings.Remove(following.Id);

			var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
			var update = Builders<User>.Update
									   .Set(s => s.Following, followings.ToArray());
			return await _context.Users.UpdateOneAsync(filter, update);
		}

        public User getUser() 
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = _context.Users.Find(m => m.Email == username);
			var user = users.FirstOrDefault();
			if (user.Following == null) user.Following = new string[0];
            return user;
        }
    }
}