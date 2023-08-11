using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;


namespace ServiceLayer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class TaskListController : Controller
{
    OrganizeMeRepository repository;
    public TaskListController()
    {
        repository = new OrganizeMeRepository();
    }

    //Create TaskList
    [HttpPost]
    public bool CreateTaskList(string listName)
    {
        bool status = false;
        try
        {
            status = this.repository.CreateTaskList(listName);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }
        return status;
    }

    //Read TaskList by EmpId
    [HttpGet]
    public JsonResult FetchTaskListByEmpId(decimal empId)
    {
        try
        {
            var taskList = this.repository.FetchTaskListByEmpId(empId);
            DataAccessLayer.Models.TaskList task;
            var tasks = new List<DataAccessLayer.Models.TaskList>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.TaskList
                    {
                        TaskListId = (byte)t.TaskListId,
                        TaskListName = t.TaskListName,
                        DeleteFlagTl = t.DeleteFlagTl
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

    //Udate TaskList
    [HttpPut]
    public JsonResult UpdateTaskList(TaskList taskLObj)
    {
        bool status = false;
        try
        {
            status = this.repository.UpdateTaskList(taskLObj.TaskListId, taskLObj.TaskListName, taskLObj.DeleteFlagTl);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }

        return Json(status);
    }

    //Progress tracking of taskList
    [HttpGet]
    public JsonResult CalculateProgressTracking(decimal empId, byte listId)
    {
        try
        {
            double taskList = this.repository.CalculateProgressTracking(empId, listId);

            return Json(taskList);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }
}