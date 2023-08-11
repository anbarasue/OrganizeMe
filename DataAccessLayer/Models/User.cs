using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            TaskAssignedToNavigations = new HashSet<Task>();
            TaskEmps = new HashSet<Task>();
        }

        public decimal EmpId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string UserAddress { get; set; } = null!;

        public virtual ICollection<Task> TaskAssignedToNavigations { get; set; }
        public virtual ICollection<Task> TaskEmps { get; set; }
    }
}
