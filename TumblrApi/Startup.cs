﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TumblrApi.Models;

namespace TumblrApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddCors();
			services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
			services.AddAWSService<IAmazonS3>();
			services.AddAWSService<IAmazonDynamoDB>();
            MongoDBContext.ConnectionString = "mongodb://user:pass@ds133378.mlab.com:33378/tumblr";
            MongoDBContext.DatabaseName = "tumblr";
            MongoDBContext.IsSSL = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseCors(builder => builder
			   .AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .AllowCredentials());
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			app.UseMiddleware<AuthenticationMiddleware>();

			app.UseMvc();
        }
    }
}
