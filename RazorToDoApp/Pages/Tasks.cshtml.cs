using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RazorToDoApp.Data;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages
{
    [Authorize(Policy = "MustBeLoggedIn")]
    public class TasksModel : PageModel
    {
        [BindProperty]
        public ToDoTask Task { get; set; }
        private readonly ApplicationDBContext _context;
        public TasksModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public void GetTasks()
        {
            var taskList = _context.ToDoTask.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
            ViewData["taskList"] = taskList;
        }
        public void OnGet()
        {
            GetTasks();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) 
            {
                GetTasks();
                return Page();
            }

            var contextUser = _context.User
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var newTask = new DbToDoTask();
            newTask.Name = Task.Name;
            newTask.User = contextUser;
            _context.Add(newTask);

            await _context.SaveChangesAsync();

            GetTasks();

            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var contextUser = _context.User
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var contextTask = _context.ToDoTask.Where(t => t.Id == id).FirstOrDefault();

            if (contextTask?.User.Id != contextUser.Id)
            {
                GetTasks();
                return Page();
            }

            _context.Remove(contextTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }
        public async Task<IActionResult> OnPostEdit(string name, int id)
        {
            if (name.IsNullOrEmpty())
            {
                GetTasks();
                ViewData["taskEditError"] = "Edited task name is empty";
                return Page();
            }

            if (name.Length > 30)
            {
                GetTasks();
                ViewData["taskEditError"] = "Edited task name is too long";
                return Page();
            }

            var contextUser = _context.User
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var contextTask = _context.ToDoTask.Where(t => t.Id == id).FirstOrDefault();

            if (contextTask?.User.Id != contextUser.Id)
            {
                GetTasks();
                return Page();
            }

            contextTask.Name = name;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }
    }
}
