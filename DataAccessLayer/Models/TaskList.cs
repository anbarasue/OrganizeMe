using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class TaskList
    {
        public TaskList()
        {
            Tasks = new HashSet<Task>();
        }

        public byte TaskListId { get; set; }
        public string TaskListName { get; set; } = null!;
        public byte DeleteFlagTl { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
