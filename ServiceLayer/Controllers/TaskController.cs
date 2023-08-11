using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;


namespace ServiceLayer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TaskController : Controller
{
    OrganizeMeRepository repository;
    public TaskController()
    {
        repository = new OrganizeMeRepository();
    }

    //Create Task
    [HttpPost]
    public bool CreateTask(DataAccessLayer.Models.Task task)
    {
        bool status = false;
        try
        {
            status = this.repository.CreateTask(task);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }
        return status;
    }

    //Read Task
    [HttpGet]
    public JsonResult ReadTask(decimal empId)
    {
        try
        {
            var taskList = this.repository.ReadTaskByEmpId(empId);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }

    // Update Task
    [HttpPut]
    public JsonResult UpdateTask(DataAccessLayer.Models.Task taskObj)
    {
        bool status = false;
        try
        {
            status = this.repository.UpdateTask(taskObj.TaskId, taskObj.TaskName, taskObj.TaskNote, taskObj.TaskCategoryId, taskObj.TaskListId, taskObj.DueDate, taskObj.AssignedTo, taskObj.Priority, taskObj.TaskStatus, taskObj.DeleteFlagT);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }

        return Json(status);
    }

    //Search Task Using SubStr
    [HttpGet]
    public JsonResult FetchTaskUsingSubstr(decimal empId, string subStr)
    {
        try
        {
            var taskList = this.repository.FetchTaskUsingSubstr(empId, subStr);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }

    //Read Task by Date PENDING
    [HttpGet]
    public JsonResult ReadTaskByDeadline(decimal empId)
    {
        try
        {
            var taskList = this.repository.ReadTaskByDeadline(empId);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }
    //Read Task by importance
    [HttpGet]
    public JsonResult ReadTaskByPriority(decimal empId, byte priority)
    {
        try
        {
            var taskList = this.repository.ReadTaskByPriority(empId, priority);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }


    }

    //Read Task assigned to me
    [HttpGet]
    public JsonResult ReadTaskByAssignedTo(decimal empId)
    {
        try
        {
            var taskList = this.repository.ReadTaskByAssignedTo(empId);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }

    //Read Task by TaskId
    [HttpGet]
    public JsonResult ReadTaskByTaskId(string taskId)
    {
        try
        {
            var taskList = this.repository.ReadTaskByTaskId(taskId);
            DataAccessLayer.Models.Task task;
            var tasks = new List<DataAccessLayer.Models.Task>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.Task
                    {
                        EmpId = t.EmpId,
                        TaskId = t.TaskId,
                        TaskName = t.TaskName,
                        TaskNote = t.TaskNote,
                        TaskCategoryId = t.TaskCategoryId,
                        TaskListId = t.TaskListId,
                        DateCreated = t.DateCreated,
                        DueDate = t.DueDate,
                        AssignedTo = t.AssignedTo,
                        Priority = t.Priority,
                        TaskStatus = t.TaskStatus,
                        DeleteFlagT = t.DeleteFlagT
                    };
                    tasks.Add(task);
                }
            }
            return Json(tasks);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }

}






























































    // //Delete Task
    // [HttpDelete]
    // public JsonResult DeleteTask(String taskId)
    // {
    //     var status = false;
    //     try
    //     {
    //         status = this.repository.DeleteTask(taskId);
    //     }
    //     catch (Exception ex)
    //     {
    //         System.Console.WriteLine("TaskController Exception", ex);
    //         status = false;
    //     }
    //     return Json(status);
    // }



    // // Assign Task
    // [HttpPut]
    // public JsonResult AssignTask(DataAccessLayer.Models.Task taskObj)
    // {
    //     bool status = false;
    //     try
    //     {
    //         status = this.repository.AssignTask(taskObj.EmpId, taskObj.TaskId, taskObj.AssignedTo);
    //     }
    //     catch (Exception ex)
    //     {
    //         System.Console.WriteLine("TaskController Exception", ex);
    //         status = false;
    //     }

    //     return Json(status);
    // }





    // //Delete Task
    // [HttpDelete]
    // public JsonResult DeleteTaskList(byte taskLId)
    // {
    //     var status = false;
    //     try
    //     {
    //         status = this.repository.DeleteTaskList(taskLId);
    //     }
    //     catch (Exception ex)
    //     {
    //         System.Console.WriteLine("TaskController Exception", ex);
    //         status = false;
    //     }
    //     return Json(status);
    // }