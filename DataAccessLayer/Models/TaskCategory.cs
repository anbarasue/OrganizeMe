using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class TaskCategory
    {
        public TaskCategory()
        {
            Tasks = new HashSet<Task>();
        }

        public byte TaskCategoryId { get; set; }
        public string TaskCategoryName { get; set; } = null!;
        public byte DeleteFlagTc { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
