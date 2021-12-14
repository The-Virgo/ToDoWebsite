using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebsite.Models
{
    public class Task
    {
        /// <summary>
        /// The auto assigned id of a task
        /// </summary>
        [Key]
        public int TaskId { get; set; }

        /// <summary>
        /// The id of the user that created the task
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        /// <summary>
        /// The user that made the task
        /// </summary>
        public IdentityUser User { get; set; }

        /// <summary>
        /// The title of a task created by the user
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of a task created by the user
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Due Date of a task selected by a user
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Whether or not the task has been marked complete, automatically set false
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
