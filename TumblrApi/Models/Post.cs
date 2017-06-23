using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{   
    public class Post
	{
        String _id;
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id {
            get { return _id ?? ObjectId.GenerateNewId().ToString(); }
            set { _id = value; }
        }
		[BsonElement("UserId")]
		[BsonRepresentation(BsonType.ObjectId)]
        public String UserId { get; set; }
        [BsonElement("Publish")]
        public DateTime Publish { get; set; }
		[BsonElement("Media")]
        public Media Media { get; set; }
    }
}
