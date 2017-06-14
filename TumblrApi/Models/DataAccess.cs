using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace TumblrApi.Models
{
	public class DataAccess
	{
		MongoClient _client;
        MongoDatabase _database;
        MongoCollection _collection;

        public DataAccess()
		{
			_client = new MongoClient("mongodb://localhost:27017");
			_database = _client.GetDatabase("Tumblr");
			_collection = _database.GetCollection<PostItem>("Posts");
		}

		public IEnumerable<PostItem> GetPost()
		{
			return _db.GetCollection<Product>("Products").FindAll();
		}


		public Product GetProduct(ObjectId id)
		{
			var res = Query<Product>.EQ(p => p.Id, id);
			return _db.GetCollection<Product>("Products").FindOne(res);
		}

		public Product Create(Product p)
		{
			_db.GetCollection<Product>("Products").Save(p);
			return p;
		}

		public void Update(ObjectId id, Product p)
		{
			p.Id = id;
			var res = Query<Product>.EQ(pd => pd.Id, id);
			var operation = Update<Product>.Replace(p);
			_db.GetCollection<Product>("Products").Update(res, operation);
		}
		public void Remove(ObjectId id)
		{
			var res = Query<Product>.EQ(e => e.Id, id);
			var operation = _db.GetCollection<Product>("Products").Remove(res);
		}
	}
}