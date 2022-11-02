using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Entities;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages
{
    [Authorize(Policy = "MustBeLoggedIn")]
    public class TasksModel : PageModel
    {
        [BindProperty]
        public ToDoTask Task { get; set; }
        private readonly Data.AppDbContext _context;
        public TasksModel(Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) 
            {
                ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
                return Page();
            }

            var contextUser = _context.Users
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var newTask = new AppTask();
            newTask.Name = Task.Name;
            newTask.User = contextUser;
            _context.Add(newTask);

            await _context.SaveChangesAsync();

            ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var contextUser = _context.Users
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var contextTask = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();

            if (contextTask?.User.Id != contextUser.Id)
            {
                ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
                return Page();
            }

            _context.Remove(contextTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }

        public async Task<IActionResult> OnPostEdit(string? name, int id)
        {
            if (string.IsNullOrEmpty(name))
            {
                ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
                ViewData["taskEditError"] = "Edited task name is empty";
                return Page();
            }

            if (name.Length > 30)
            {
                ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
                ViewData["taskEditError"] = "Edited task name is too long";
                return Page();
            }

            var contextUser = _context.Users
                .Where(u => u.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).FirstOrDefault();
            var contextTask = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();

            if (contextTask?.User.Id != contextUser.Id)
            {
                ViewData["taskList"] = await _context.Tasks.Where(t => t.User.Id == Int32
                .Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
                return Page();
            }

            contextTask.Name = name;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks");
        }
    }
}
