﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TumblrApi.Models
{
	public class Auth
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Photo { get; set; }
	}
}
