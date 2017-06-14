using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class User
	{
		String _id;
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public String Id
		{
			get { return _id ?? ObjectId.GenerateNewId().ToString(); }
			set { _id = value; }
		}
		[BsonElement("Name")]
		public string Name { get; set; }
		[BsonElement("UserName")]
		public string UserName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<String> BlogId { get; set; }
	}
}
