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
    public class LoginControllerTests
    {
        MockFunctions mock = new MockFunctions();

        [Fact]
        public async void LoginModel_OnGet_IsAuthenticated()
        {
            //Arrange
            ClaimsPrincipal user;
            var dbContext = await mock.GetDatabaseContext();
            user = mock.GetPrincipal();          
            var pageContext = mock.GetPageContext(user);

            var controller = new LoginModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = controller.OnGet();

            //Assert
            result.Should().BeOfType<RedirectToPageResult>();
        }


        [Fact]
        public async void LoginModel_OnGet_IsNotAuthenticated()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new LoginModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = controller.OnGet();

            //Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async void LoginModel_OnPostAsync_PostedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);

            var controller = new LoginModel(dbContext);
            controller.PageContext = pageContext;
            controller.Credentials = new LoginCredentials();
            controller.PageContext.HttpContext = new DefaultHttpContext()
            {
                RequestServices = serviceProviderMock.Object,
            };
            controller.Credentials.UserName = "12345";
            controller.Credentials.Password = "12345";

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<RedirectToPageResult>(); ;
        }

        [Fact]
        public async void LoginModel_OnPostAsync_NotPostedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new LoginModel(dbContext);
            controller.PageContext = pageContext;
            controller.Credentials = new LoginCredentials();

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<PageResult>(); ;
        }

        [Fact]
        public async void LoginModel_OnPostAsync_ModelStateIsNotValid()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var pageContext = mock.GetPageContext(null);

            var controller = new LoginModel(dbContext);
            controller.PageContext = pageContext;
            controller.ModelState.AddModelError("Mock", "Mock is required");

            //Act
            var result = await controller.OnPostAsync();

            //Assert
            result.Should().BeOfType<PageResult>();
        }
    }
}