using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebsite.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        public IdentityUser UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsComplete { get; set; }
    }
}
