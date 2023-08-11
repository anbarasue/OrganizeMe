using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DataAccessLayer
{
    public class OrganizeMeRepository
    {
        private OrganizeMeFinalDBContext context { get; set; }
        public OrganizeMeRepository()
        {
            context = new OrganizeMeFinalDBContext();
        }

        //Validate Login
        public decimal ValidateUserCredentials(decimal empId, string password)
        {
            decimal usrName;
            try
            {
                var objUser = (from usr in context.Users
                               where usr.EmpId == empId && usr.UserPassword == password
                               select usr.UserName).FirstOrDefault();

                if (objUser != null)
                {
                    usrName = empId;
                }
                else
                {
                    usrName = 0;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                usrName = 0;
            }
            return usrName;
        }

        //Register User
        public bool RegisterUser(User user)
        {
            bool status;
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                status = false;
            }

            return status;
        }

        //Read EmpIds
        public List<Decimal> ReadEmpId()
        {
            List<Decimal> empId;
            try
            {
                empId = context.Users.Select(emp=>emp.EmpId).ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                empId = new List<Decimal>();
            }
            return empId;
        }

        //Generate TaskId
        public string GenerateTaskId()
        {
            string tId;
            try
            {
                tId = (from s in context.TaskLists
                       select OrganizeMeFinalDBContext.GenerateNewTaskId())
                             .FirstOrDefault();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                tId = "";
            }
            return tId;
        }

        //Create Task
        public bool CreateTask(Models.Task task)
        {
            bool status = false;
            try
            {
                task.TaskId = GenerateTaskId();
                task.DueDate= null;
                context.Tasks.Add(task);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                status = false;
            }
            return status;
        }

        //Read Task by EmpId 
        public List<Models.Task> ReadTaskByEmpId(decimal empId)
        {
            List<Models.Task> lstTask;
            try
            {
                SqlParameter prmEmpId = new("@EmpId", empId);
                lstTask = context.Tasks
                                    .FromSqlRaw("SELECT * FROM Task WHERE EmpId=@EmpId or AssignedTo=@EmpId AND DeleteFlagT=0", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }
   
        //Update Task
        public bool UpdateTask(string taskId, string taskName, string taskNote, byte? taskCategoryId, byte? taskListId, DateTime? dueDate, decimal? assignedTo, byte priority, byte taskStatus, byte deleteFlagT)
        {
            bool status = false;

            Models.Task task = new Models.Task();
            try
            {
                using (var con = new OrganizeMeFinalDBContext())
                {
                    task = (from t in con.Tasks where t.TaskId == taskId select t).First<Models.Task>();
                    task.TaskName = taskName;
                    task.TaskNote = taskNote;
                    task.TaskCategoryId = taskCategoryId;
                    task.TaskListId = taskListId;
                    task.AssignedTo = assignedTo;
                    task.Priority = priority;
                    task.TaskStatus = taskStatus;
                    task.DeleteFlagT = deleteFlagT;
                    con.Tasks.Update(task);
                    con.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                status = false;
            }
            return status;
        }

        // //Delete Task
        // public bool DeleteTask(string taskId)
        // {
        //     bool status = false;
        //     try
        //     {
        //         var task = (from t in context.Tasks
        //                     where t.TaskId == taskId
        //                     select t).First<Models.Task>();
        //         context.Tasks.Remove(task);
        //         context.SaveChanges();
        //         status = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         System.Console.WriteLine("Reposiotory Exception", ex);
        //         status = false;
        //     }
        //     return status;
        // }


        //Search Task Using SubStr
        public List<Models.Task> FetchTaskUsingSubstr(decimal empId, string subStr)
        {
            List<Models.Task> lstTask = new List<Models.Task>();
            try
            {
                lstTask = (from st in context.Tasks
                           where st.EmpId == empId && st.TaskName.ToLower().Contains(subStr.ToLower())
                           orderby st.TaskName ascending
                           select st).ToList<Models.Task>();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }

        // //Assign Task
        // public bool AssignTask(decimal? empId, string taskId, decimal? assignedTo)
        // {
        //     bool status = false;

        //     Models.Task task = new Models.Task();
        //     try
        //     {
        //         using (var con = new OrganizeMeFinalDBContext())
        //         {
        //             task = (from t in con.Tasks where t.TaskId == taskId select t).First<Models.Task>();
        //             task.AssignedTo = assignedTo;
        //             con.Tasks.Update(task);
        //             con.SaveChanges();


        //             status = true;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         System.Console.WriteLine("Reposiotory Exception", ex);
        //         status = false;
        //     }
        //     return status;
        // }

        //Read Task by Deadline 
        public List<Models.Task> ReadTaskByDeadline(decimal empId)
        {
            List<Models.Task> lstTask = new List<Models.Task>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                lstTask = context.Tasks
                                    .FromSqlRaw("SELECT * FROM Task WHERE EmpId=@EmpId AND Convert(date,DueDate)=Convert(date,getdate())", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }

        //Read Task by Importance 
        public List<Models.Task> ReadTaskByPriority(decimal empId, byte priority)
        {
            List<Models.Task> lstTask = new List<Models.Task>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                SqlParameter prmPriority = new SqlParameter("@Priority", priority);
                lstTask = context.Tasks
                                    .FromSqlRaw("SELECT * FROM Task WHERE EmpId=@EmpId AND Priority=@Priority", prmEmpId, prmPriority).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }

        //Read Task Assigned to Me 
        public List<Models.Task> ReadTaskByAssignedTo(decimal empId)
        {
            List<Models.Task> lstTask = new List<Models.Task>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                lstTask = context.Tasks
                                    .FromSqlRaw("SELECT * FROM Task WHERE AssignedTo=@EmpId", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }

        //Read Task by TaskId 
        public List<Models.Task> ReadTaskByTaskId(string taskId)
        {
            List<Models.Task> lstTask = new List<Models.Task>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@taskId", taskId);
                lstTask = context.Tasks
                                    .FromSqlRaw("SELECT * FROM Task WHERE TaskId=@taskId", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.Task>();
            }
            return lstTask;
        }

        //Generate TaskList Id
        public byte GenerateTaskListId()
        {
            byte tlId;
            try
            {
                tlId = (from s in context.TaskLists
                        select OrganizeMeFinalDBContext.GenerateNewTaskListId())
                             .FirstOrDefault();
                Console.WriteLine(tlId);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                tlId = 0;
            }
            return tlId;
        }

        //Create TaskList
        public bool CreateTaskList(string listName)
        {
            Models.TaskList taskList = new();
            bool status = false;
            try
            {
                taskList.TaskListId = GenerateTaskListId();
                taskList.TaskListName = listName;
                context.TaskLists.Add(taskList);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                status = false;
            }
            return status;
        }

        //Fetch TaskList by EmpId 
        public List<Models.TaskList> FetchTaskListByEmpId(decimal empId)
        {
            List<Models.TaskList> lstTask = new List<Models.TaskList>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                lstTask = context.TaskLists
                                    .FromSqlRaw("Select Distinct tl.* from TaskList tl join Task t on tl.TaskListId=t.TaskListId where t.EmpId= @EmpId", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex.Message);
                lstTask = new List<Models.TaskList>();
            }
            return lstTask;
        }
        //Update TaskList
        public bool UpdateTaskList(byte taskListId, string taskLsitName, byte deleteFlagtl)
        {
            bool status = false;

            Models.TaskList task = new Models.TaskList();
            try
            {
                using (var con = new OrganizeMeFinalDBContext())
                {
                    task = (from t in con.TaskLists where t.TaskListId == taskListId select t).First<Models.TaskList>();
                    task.TaskListName = taskLsitName;
                    task.DeleteFlagTl = deleteFlagtl;
                    con.TaskLists.Update(task);
                    con.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                status = false;
            }
            return status;
        }

        //Generate TaskCategory Id
        public byte GenerateTaskCategoryId()
        {
            byte tcId;
            try
            {
                tcId = (from s in context.TaskLists
                        select OrganizeMeFinalDBContext.GenerateNewTaskCategoryId())
                             .FirstOrDefault();
                Console.WriteLine(tcId);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                tcId = 0;
            }
            return tcId;
        }

        //Create TaskCategory
        public bool CreateTaskCategory(string categoryName)
        {
            Models.TaskCategory taskCategory = new();
            bool status = false;
            try
            {
                taskCategory.TaskCategoryId = GenerateTaskCategoryId();
                taskCategory.TaskCategoryName = categoryName;
                context.TaskCategories.Add(taskCategory);
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                status = false;
            }
            return status;
        }

        //Fetch TaskCategory by EmpId 
        public List<Models.TaskCategory> FetchTaskCategoryByEmpId(decimal empId)
        {
            List<Models.TaskCategory> lstTask = new List<Models.TaskCategory>();
            try
            {
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                lstTask = context.TaskCategories
                                    .FromSqlRaw("Select Distinct tc.* from TaskCategory tc join Task t on tc.TaskCategoryId=t.TaskCategoryId where t.EmpId= @EmpId", prmEmpId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                System.Console.WriteLine(ex);
                lstTask = new List<Models.TaskCategory>();
            }
            return lstTask;
        }

        //Update TaskCategory
        public bool UpdateTaskCategory(byte taskCategoryId, string taskCategoryName, byte deleteFlagtc)
        {
            bool status = false;

            Models.TaskCategory task = new Models.TaskCategory();
            try
            {
                using (var con = new OrganizeMeFinalDBContext())
                {
                    task = (from t in con.TaskCategories where t.TaskCategoryId == taskCategoryId select t).First<Models.TaskCategory>();
                    task.TaskCategoryName = taskCategoryName;
                    task.DeleteFlagTc = deleteFlagtc;
                    con.TaskCategories.Update(task);
                    con.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Reposiotory Exception", ex);
                status = false;
            }
            return status;
        }

        // //Delete TaskList
        // public bool DeleteTaskList(byte taskListId)
        // {
        //     bool status = false;
        //     try
        //     {
        //         var task = (from t in context.TaskLists
        //                     where t.TaskListId == taskListId
        //                     select t).First<Models.TaskList>();
        //         context.TaskLists.Remove(task);
        //         context.SaveChanges();
        //         status = true;
        //     }
        //     catch (Exception ex)
        //     {
        //         System.Console.WriteLine("Reposiotory Exception", ex);
        //         status = false;
        //     }
        //     return status;
        // }

        // sp Progress tracking of taskList
        public double CalculateProgressTracking(decimal empId, byte taskListId)
        {
            SqlParameter prmEmpId = new("@EmpId", empId);
            SqlParameter prmListId = new("@taskListId", taskListId);
            List<Models.Task> task = context.Tasks
                                .FromSqlRaw("SELECT * FROM Task where EmpId=@EmpId and TaskListId=@taskListId", prmEmpId, prmListId)
                                .ToList();

            int totalTask = task.Count();
            int doneTask = task.Count(task => task.TaskListId == taskListId && task.TaskStatus == 1);

            if (totalTask == 0)
            {
                return 0;
            }
            else
            {
                double progress = (double)doneTask / totalTask * 100;
                return progress;
            }
        }
    }
}