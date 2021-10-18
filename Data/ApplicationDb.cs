using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebsite.Data
{
    public class ApplicationDb
    {
        public static async Task<ToDoWebsite.Models.Task> AddProductAsync(ApplicationDbContext _context, ToDoWebsite.Models.Task t)
        {
            //Add to DB
            _context.Tasks.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }
    }
}
