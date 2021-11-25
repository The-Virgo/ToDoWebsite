using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoWebsite.Data;

namespace ToDoWebsite.Controllers
{
    public class TaskController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskController
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ToDoWebsite.Models.Task> tasks = await ApplicationDb.GetTasksAsync(_context, userId);

            return View(tasks);
        } 

        /*
        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        } */

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoWebsite.Models.Task t)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                t.UserId = userId;

                await ApplicationDb.AddTaskAsync(_context, t);

                //TempData["Message"] = $"{t.Title} was added successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ToDoWebsite.Models.Task t = await ApplicationDb.GetTaskAsync(_context, id);

            _context.Entry(t).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
