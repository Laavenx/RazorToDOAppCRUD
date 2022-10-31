using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Data;
using RazorToDoApp.Entities;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RazorToDoApp.Tests
{
    internal class MockFunctions
    {
        public async Task<AppDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() <= 0)
            {
                var user = new AppUser()
                {
                    Id = 1,
                    UserName = "12345",
                    Password = "$2a$12$kRKCACHHQfkm7puDe9OgI.9E2opXQWBA7iq6JTzR/BNIEEwviwP/m"
                };
                databaseContext.Users.Add(user);
                databaseContext.Tasks.Add(
                new AppTask()
                {
                    Id = 1,
                    Name = "MockTask1",
                    User = user
                });
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        public PageContext GetPageContext(ClaimsPrincipal? claim)
        {
            var httpContext = new DefaultHttpContext() { User = claim };
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new EmptyModelMetadataProvider();
            var pageContext = new PageContext(actionContext)
            {
                ViewData = new ViewDataDictionary(modelMetadataProvider, modelState),
            };

            return pageContext;
        }

        public ClaimsPrincipal GetPrincipal(string id = "1")
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));
            return user;
        }
    }
}
