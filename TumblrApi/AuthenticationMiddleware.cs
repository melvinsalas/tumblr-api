using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using TumblrApi.Models;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];
        if (authHeader != null && authHeader.StartsWith("Basic", StringComparison.Ordinal))
        {
            //Extract credentials
            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            var username = usernamePassword.Substring(0, seperatorIndex);
            var password = usernamePassword.Substring(seperatorIndex + 1);

            MongoDBContext dbContext = new MongoDBContext();
            User loggedUser = dbContext.Users.Find(m => m.Email == username && m.Password == password).FirstOrDefault();

            if (loggedUser == null)
            {
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, loggedUser.Email) };
            var identity = new ClaimsIdentity(claims, "Basic");
            context.User = new ClaimsPrincipal(identity);
            await _next.Invoke(context);
        }
        else
        {
            // no authorization header
            context.Response.StatusCode = 401; //Unauthorized
            return;
        }
    }
}