using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models.MediaType
{   
    public class Text : Media
	{
		[BsonElement("Title")]
		public string Title { get; set; }
    }
}
