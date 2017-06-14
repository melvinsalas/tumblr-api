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
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Content")]
        public string Content { get; set; }
        DateTime? _published;
        [BsonElement("Published")]
        public DateTime Published 
        {
            get { return _published ?? DateTime.Now; }
            set { _published = value; }
        }
    }
}
