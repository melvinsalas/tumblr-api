using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models.MediaPost
{
    public class PhotoText
    {
		String _id;
		[BsonRepresentation(BsonType.ObjectId)]
		public String Id
		{
			get { return _id ?? ObjectId.GenerateNewId().ToString(); }
			set { _id = value; }
		}
		[BsonElement("UserId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public String UserId { get; set; }
		[BsonElement("Media")]
		public MediaType.Text Media { get; set; }

        public static explicit operator PhotoText(Post post)
        {
			var c = new PhotoText()
			{
				UserId = post.UserId,
				Media = (MediaType.Text)post.Media
			};
            return c;
        }
    }
}
