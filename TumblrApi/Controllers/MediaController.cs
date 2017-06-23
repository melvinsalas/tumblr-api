using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TumblrApi.Models;

namespace TumblrApi.Controllers
{
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        MongoDBContext _context;
        string _secretKey = "";
        string _accessKey = "";
        string _bucketName = "melvinsalas.tumblr";
        RegionEndpoint _region = RegionEndpoint.USEast2;

        public MediaController()
        {
            _context = new MongoDBContext();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MediaFile file)
        {
            // Verify Bad Request
            if (file == null) return BadRequest();

            // Set varibles & new states
            var client = new AmazonS3Client(_accessKey, _secretKey, _region);
            var memory = new MemoryStream(Convert.FromBase64String(file.Bytes));
            var name = Guid.NewGuid().ToString() + "-" + file.Name.Replace(' ', '_');

            // Prepare aws request
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = name,
                InputStream = memory,
                CannedACL = S3CannedACL.PublicRead
            };

            // Try to publish
            try { await client.PutObjectAsync(request); }
            catch (Exception e) { Console.Write(e.Message); }

            // Return filename
            return Json(name);
        }
    }
}