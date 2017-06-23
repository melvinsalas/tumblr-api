using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class Radar
	{
		public User User { get; set; }
		public Post Post { get; set; }
	}
}
