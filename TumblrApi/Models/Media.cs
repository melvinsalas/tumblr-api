using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{   
    public class Media
    {
		[BsonElement("Type")]
		public string Type { get; set; }
		[BsonElement("Tags")]
		public string[] Tags { get; set; }
		[BsonElement("Title")]
		public string Title { get; set; }
		[BsonElement("Content")]
		public string Content { get; set; }
		[BsonElement("URL")]
		public string URL { get; set; }
    }
}
