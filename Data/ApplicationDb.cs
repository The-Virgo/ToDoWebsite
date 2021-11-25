using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoWebsite.Data
{
    public class ApplicationDb
    {
        public static async Task<ToDoWebsite.Models.Task> AddTaskAsync(ApplicationDbContext _context, ToDoWebsite.Models.Task t)
        {
            //Add to DB
            _context.Tasks.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public static async Task<List<ToDoWebsite.Models.Task>> GetTasksAsync(ApplicationDbContext _context, string id)
        {
            return
                await (from t in _context.Tasks
                       where t.UserId == id
                       orderby t.Title ascending
                       select t)
                       .ToListAsync();

        }

        public static async Task<ToDoWebsite.Models.Task> GetTaskAsync(ApplicationDbContext context, int taskId)
        {
            ToDoWebsite.Models.Task t = await (from Tasks in context.Tasks
                               where Tasks.TaskId == taskId
                               select Tasks).SingleAsync();

            return t;
        }
    }
}
