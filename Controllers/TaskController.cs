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

        public async Task<IActionResult> Finished()
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
        public async Task<ActionResult> Edit(int id)
        {
            ToDoWebsite.Models.Task t = await ApplicationDb.GetTaskAsync(_context, id);

            // pass product to view
            return View(t);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDoWebsite.Models.Task t)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(t).State = EntityState.Modified;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                t.UserId = userId;
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            return View(t);
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

        public async Task<ActionResult> MarkDone(int id)
        {
            if (ModelState.IsValid) 
            {
                ToDoWebsite.Models.Task t = await ApplicationDb.GetTaskAsync(_context, id);

                _context.Entry(t).State = EntityState.Modified;
                t.IsComplete = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> MarkNotDone(int id)
        {
            if (ModelState.IsValid)
            {
                ToDoWebsite.Models.Task t = await ApplicationDb.GetTaskAsync(_context, id);

                _context.Entry(t).State = EntityState.Modified;
                t.IsComplete = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Finished");
        }
    }
}
