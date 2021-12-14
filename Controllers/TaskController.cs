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
    /// <summary>
    /// Controls all interactions that Task class and Views have with the database
    /// </summary>
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

        
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Allows user to create task with inputed data
        /// </summary>
        /// <param name="t">The task being created</param>
        /// <returns>
        /// Action Result
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoWebsite.Models.Task t)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                t.UserId = userId;

                await ApplicationDb.AddTaskAsync(_context, t);

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            ToDoWebsite.Models.Task t = await ApplicationDb.GetTaskAsync(_context, id);

            // pass task to view
            return View(t);
        }

        /// <summary>
        /// Changes existing task info to what is set by user
        /// </summary>
        /// <param name="t">The task being edited</param>
        /// <returns>
        /// Action Result
        /// </returns>
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

        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Deletes selected task
        /// </summary>
        /// <param name="id">Id of selected task</param>
        /// <returns>
        /// Action Result
        /// </returns>
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

        /// <summary>
        /// changes the isComplete boolean property of a task to true 
        /// </summary>
        /// <param name="id">The id of the selected task</param>
        /// <returns>
        /// Action Result
        /// </returns>
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

        /// <summary>
        /// Changes the isComplete boolean property of a task to false
        /// </summary>
        /// <param name="id">The id of the selected task</param>
        /// <returns>
        /// Action Result
        /// </returns>
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
