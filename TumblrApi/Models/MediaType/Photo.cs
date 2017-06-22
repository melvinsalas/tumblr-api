using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models.MediaType
{
	public class Photo : Media
	{
		[BsonElement("URL")]
		public string URL { get; set; }
	}
}
