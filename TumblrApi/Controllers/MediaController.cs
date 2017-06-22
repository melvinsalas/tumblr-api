using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
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
        string _secretKey = "JmN2PvzZwJ7BkPrT28QmmVimiKT63B89N5tRiamc";
        string _accessKey = "AKIAJ762GGDL6IP6HVBA";
        string _bucketName = "melvinsalas.tumblr";

        public MediaController()
        {
            _context = new MongoDBContext();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MediaFile file)
        {
            // verify null request
            if (file == null) return BadRequest();

            // set states
            var client = new AmazonS3Client(_accessKey, _secretKey, Amazon.RegionEndpoint.USEast2);
            var memory = new MemoryStream(Convert.FromBase64String(file.Bytes));
            var name = Guid.NewGuid().ToString() + "-" + file.Name.Replace(' ', '_');

            // prepare request
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = name,
                InputStream = memory,
                CannedACL = S3CannedACL.PublicRead
            };

            // try put object
            try { await client.PutObjectAsync(request); }
            catch (Exception e) { Console.Write(e.Message); }

            // return file name
            return Json(name);
        }
    }
}