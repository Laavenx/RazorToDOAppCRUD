using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using RazorToDoApp.Data;
using RazorToDoApp.Models;
using RazorToDoApp.Pages;

namespace RazorToDoApp.Tests
{
    public class RegisterControllerTests
    {
        MockFunctions mock = new MockFunctions();

        [Fact]
        public async void RegisterModel_OnGet_IsAuthenticated()
        {
            //Arrange
            ClaimsPrincipal user;
            var dbContext = await mock.GetDatabaseContext();
            user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new RegisterModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = controller.OnGet();

            //Assert
            result.Should().BeOfType<RedirectToPageResult>();
        }

        [Fact]
        public async void RegisterModel_OnGet_IsNotAuthenticated()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new RegisterModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = controller.OnGet();

            //Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async void RegisterModel_OnPostAsync_NotPostedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new RegisterModel(dbContext);
            controller.PageContext = pageContext;
            controller.Credentials = new RegisterCredentials();
            controller.Credentials.UserName = "12345";
            controller.Credentials.Password = "12345";

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<PageResult>(); ;
        }

        [Fact]
        public async void RegisterModel_OnPostAsync_PostedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new RegisterModel(dbContext);
            controller.PageContext = pageContext;
            controller.Credentials = new RegisterCredentials();
            controller.Credentials.UserName = "newuser";
            controller.Credentials.Password = "newuser";

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<RedirectToPageResult>(); ;
        }

        [Fact]
        public async void RegisterModel_OnPostAsync_ModelStateIsNotValid()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new RegisterModel(dbContext);
            controller.PageContext = pageContext;
            controller.ModelState.AddModelError("Mock", "Mock is required");

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<PageResult>();
        }
    }
}
