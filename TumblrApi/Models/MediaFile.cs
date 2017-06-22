using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class MediaFile
	{
		public string Name { get; set; }
		public string Bytes { get; set; }
	}
}
