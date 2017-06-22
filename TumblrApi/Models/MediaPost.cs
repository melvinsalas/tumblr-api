using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class MediaPost
	{
		public Post Post { get; set; }
		public Media Media { get; set; }
		//[BsonElement("Content")]
		//public string Content { get; set; }
		//DateTime? _published;
		//[BsonElement("Published")]
		//public DateTime Published 
		//{
		//    get { return _published ?? DateTime.Now; }
		//    set { _published = value; }
		//}
	}
}
