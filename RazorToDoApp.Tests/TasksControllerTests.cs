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
    public class TasksControllerTests
    {
        MockFunctions mock = new MockFunctions();

        [Fact]
        public async void TasksModel_OnGet_ViewModelContainsTasks()
        {
            //Arrange
            ClaimsPrincipal user;
            var dbContext = await mock.GetDatabaseContext();
            user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            controller.OnGet();
            controller.ViewData.TryGetValue("taskList", out object? view);
            Type type = view.GetType();

            //Assert
            type.IsGenericType.Should().Be(true);
        }

        [Fact]
        public async void RegisterModel_OnPost_ModelStateIsNotValid()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;
            controller.ModelState.AddModelError("Mock", "Mock is required");

            //Act
            var result = await controller.OnPost();

            //Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async void RegisterModel_OnPost_ModelStateIsValid()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.Task = new ToDoTask();
            controller.Task.Name = "Mock";
            
            controller.PageContext = pageContext;


            //Act
            var result = await controller.OnPost();

            //Assert
            result.Should().BeOfType<PageResult>();
        }


        [Fact]
        public async void RegisterModel_OnPostDelete_DeletedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = await controller.OnPostDelete(1);

            //Assert
            result.Should().BeOfType<RedirectToPageResult>();
        }

        [Fact]
        public async void RegisterModel_OnPostDelete_WrongTaskId()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = await controller.OnPostDelete(2);

            //Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async void TasksModel_OnPostEdit_ViewModelStringNull()
        {
            //Arrange
            ClaimsPrincipal user;
            var dbContext = await mock.GetDatabaseContext();
            user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            await controller.OnPostEdit(null, 1);
            controller.ViewData.TryGetValue("taskEditError", out object? view);

            //Assert
            view.Should().Be("Edited task name is empty");
        }

        [Fact]
        public async void TasksModel_OnPostEdit_ViewModelStringTooLong()
        {
            //Arrange
            ClaimsPrincipal user;
            var dbContext = await mock.GetDatabaseContext();
            user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            await controller.OnPostEdit("f3ujv6vihfxAdrmk7hBqWAwlwVpWb7J", 1);
            controller.ViewData.TryGetValue("taskEditError", out object? view);

            //Assert
            view.Should().Be("Edited task name is too long");
        }

        [Fact]
        public async void RegisterModel_OnPostEdit_WrongTaskId()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = await controller.OnPostEdit("fpWb7J", 2);

            //Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async void RegisterModel_OnPostEdit_EditedSuccessfully()
        {
            //Arrange
            var dbContext = await mock.GetDatabaseContext();
            var user = mock.GetPrincipal();
            var pageContext = mock.GetPageContext(user);

            var controller = new TasksModel(dbContext);
            controller.PageContext = pageContext;

            //Act
            var result = await controller.OnPostEdit("fpWb7J", 1);

            //Assert
            result.Should().BeOfType<RedirectToPageResult>();
        }
    }
}
