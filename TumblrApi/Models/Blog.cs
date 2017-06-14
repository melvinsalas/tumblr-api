using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class Blog
	{
		String _id;
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public String Id
		{
			get { return _id ?? ObjectId.GenerateNewId().ToString(); }
			set { _id = value; }
		}
	}
}
