using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;


namespace ServiceLayer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    OrganizeMeRepository repository;
    public UserController()
    {
        repository = new OrganizeMeRepository();
    }

    //Validate Login
    [HttpPost]
    public JsonResult ValidateUserCredentials(DataAccessLayer.Models.User userObj)
    {
        decimal response;
        try
        {
            response = this.repository.ValidateUserCredentials(userObj.EmpId, userObj.UserPassword);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("UserController Exception", ex);
            response = 0;
        }

        return Json(response);
    }

    //Register User
    [HttpPost]
    public JsonResult RegisterUser(DataAccessLayer.Models.User user)
    {
        bool status = false;
        string message;

        try
        {
            status = repository.RegisterUser(user);
            if (status)
            {
                message = "Successful addition operation of EmpId: " + user.EmpId;
            }
            else
            {
                message = "Unsuccessful addition operation!";
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("UserController Exception", ex);
            message = "Some error occured, please try again!";
        }
        return Json(status);
    }

    //Read EmpIds
    [HttpGet]
    public JsonResult ReadEmpId()
    {
        try
        {
            var taskList = this.repository.ReadEmpId();
            // DataAccessLayer.Models.Task task;
            // var tasks = new List<DataAccessLayer.Models.Task>();
            // if (taskList.Any())
            // {
            //     foreach (var t in taskList)
            //     {
            //         task = new DataAccessLayer.Models.Task
            //         {
            //             EmpId = t.EmpId,
            //             TaskId = t.TaskId,
            //             TaskName = t.TaskName,
            //             TaskNote = t.TaskNote,
            //             TaskCategoryId = t.TaskCategoryId,
            //             TaskListId = t.TaskListId,
            //             DateCreated = t.DateCreated,
            //             DueDate = t.DueDate,
            //             AssignedTo = t.AssignedTo,
            //             Priority = t.Priority,
            //             TaskStatus = t.TaskStatus,
            //             DeleteFlagT = t.DeleteFlagT
            //         };
            //         tasks.Add(task);
            //     }
            // }
            return Json(taskList);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("TaskController Exception", ex);
            return null;
        }

    }

}


