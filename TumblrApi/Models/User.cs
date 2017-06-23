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
		[BsonElement("Email")]
		public string Email { get; set; }
		[BsonElement("Password")]
		public string Password { get; set; }
		[BsonElement("Name")]
		public string Name { get; set; }
		[BsonElement("Photo")]
		public string Photo { get; set; }
		[BsonElement("Blogname")]
		public string Blogname { get; set; }
		[BsonElement("Following")]
        public string[] Following { get; set; }
	}
}
