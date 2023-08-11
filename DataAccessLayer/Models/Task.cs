using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Task
    {
        public decimal? EmpId { get; set; }
        public string TaskId { get; set; } = null!;
        public string TaskName { get; set; } = null!;
        public string? TaskNote { get; set; }
        public byte? TaskCategoryId { get; set; }
        public byte? TaskListId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? AssignedTo { get; set; }
        public byte Priority { get; set; }
        public byte TaskStatus { get; set; }
        public byte DeleteFlagT { get; set; }

        public virtual User? AssignedToNavigation { get; set; }
        public virtual User? Emp { get; set; }
        public virtual TaskCategory? TaskCategory { get; set; }
        public virtual TaskList? TaskList { get; set; }
    }
}
