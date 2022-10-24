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
using RazorToDoApp.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RazorToDoApp.Tests
{
    internal class MockFunctions
    {
        public async Task<ApplicationDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDBContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.User.CountAsync() <= 0)
            {
                var user = new DbUser()
                {
                    Id = 1,
                    UserName = "12345",
                    Password = "$2a$12$kRKCACHHQfkm7puDe9OgI.9E2opXQWBA7iq6JTzR/BNIEEwviwP/m"
                };
                databaseContext.User.Add(user);
                databaseContext.ToDoTask.Add(
                new DbToDoTask()
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
