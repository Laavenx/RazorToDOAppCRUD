using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public void OnGet()
        {
            var taskList = _context.ToDoTask.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList();
            ViewData["taskList"] = taskList;
        }
        public async Task<IActionResult> OnPost()
        {
            var contextUser = _context.User
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var newTask = new DbToDoTask();
            newTask.Name = Task.Name;
            newTask.User = contextUser;
            _context.Add(newTask);

            await _context.SaveChangesAsync();

            var taskList = _context.ToDoTask.Where(t => t.User == contextUser).ToList();
            ViewData["taskList"] = taskList;

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
                var taskList = _context.ToDoTask.Where(t => t.User == contextUser).ToList();
                ViewData["taskList"] = taskList;
                return Page();
            }

            _context.Remove(contextTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }
        public async Task<IActionResult> OnPostEdit(string name, int id)
        {
            var contextUser = _context.User
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var contextTask = _context.ToDoTask.Where(t => t.Id == id).FirstOrDefault();

            if (contextTask?.User.Id != contextUser.Id)
            {
                var taskList = _context.ToDoTask.Where(t => t.User == contextUser).ToList();
                ViewData["taskList"] = taskList;
                return Page();
            }

            contextTask.Name = name;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }
    }
}
