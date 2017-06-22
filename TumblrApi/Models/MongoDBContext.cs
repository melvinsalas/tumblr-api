using System;
using System.Security.Authentication;
using MongoDB.Driver;

namespace TumblrApi.Models 
{
	public class MongoDBContext
	{
		public static string ConnectionString { get; set; }
		public static string DatabaseName { get; set; }
		public static bool IsSSL { get; set; }

		IMongoDatabase _database { get; }

		public MongoDBContext()
		{
			try
			{
				MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL) settings.SslSettings = GetSslSettings();
				var mongoClient = new MongoClient(settings);
				_database = mongoClient.GetDatabase(DatabaseName);
			}
			catch (Exception ex)
			{
				throw new Exception("Can not access to db server.", ex);
			}
		}

        SslSettings GetSslSettings()
        {
            return new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
		}

		public IMongoCollection<Post> Posts
		{
			get { return _database.GetCollection<Post>("posts"); }
		}

		public IMongoCollection<User> Users
		{
			get { return _database.GetCollection<User>("users"); }
		}

		public IMongoCollection<Blog> Blogs
		{
			get { return _database.GetCollection<Blog>("blogs"); }
		}

		public IMongoCollection<Media> Media
		{
			get { return _database.GetCollection<Media>("media"); }
		}
	}
}