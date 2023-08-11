using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;


namespace ServiceLayer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class TaskCategoryController : Controller
{
    OrganizeMeRepository repository;
    public TaskCategoryController()
    {
        repository = new OrganizeMeRepository();
    }

    //Create TaskCategory
    [HttpPost]
    public bool CreateTaskCategory(string listName)
    {
        bool status = false;
        try
        {
            status = this.repository.CreateTaskCategory(listName);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }
        return status;
    }

    //Read TaskCategory by EmpId
    [HttpGet]
    public JsonResult FetchTaskCategoryByEmpId(decimal empId)
    {
        try
        {
            var taskList = this.repository.FetchTaskCategoryByEmpId(empId);
            DataAccessLayer.Models.TaskCategory task;
            var tasks = new List<DataAccessLayer.Models.TaskCategory>();
            if (taskList.Any())
            {
                foreach (var t in taskList)
                {
                    task = new DataAccessLayer.Models.TaskCategory
                    {
                        TaskCategoryId = (byte)t.TaskCategoryId,
                        TaskCategoryName = t.TaskCategoryName,
                        DeleteFlagTc = t.DeleteFlagTc
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

    //Udate TaskCategory
    [HttpPut]
    public JsonResult UpdateTaskCategory(TaskCategory taskLObj)
    {
        bool status = false;
        try
        {
            status = this.repository.UpdateTaskCategory(taskLObj.TaskCategoryId, taskLObj.TaskCategoryName, taskLObj.DeleteFlagTc);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            status = false;
        }

        return Json(status);
    }

}

