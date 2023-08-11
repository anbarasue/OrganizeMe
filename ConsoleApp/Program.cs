using System;
using DataAccessLayer;
using DataAccessLayer.Models;
namespace ConsoleApp
{
    public class Program
    {
        static OrganizeMeDBContext context;
        static OrganizeMeRepository repository;
        
        static Program()
        {
            context = new OrganizeMeDBContext();
            repository = new OrganizeMeRepository(context);
        }
        static void Main(string[] args)
        {
            var data = repository.DisplayTasks();
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("Id\tTitle\t\tDescription\tTaskGroupId\tTaskCategoryId\tCreatedAt\t\t\tUpdatedAt\t\tCompleted");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            foreach (var val in data)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t\t{4}\t\t{5}\t\t{6}\t{7}", val.Id, val.Title, val.Description,  val.TaskGroupId, val.TaskCategoryId, val.CreatedAt, val.UpdatedAt, val.Completed);
            }
        }
    }
}