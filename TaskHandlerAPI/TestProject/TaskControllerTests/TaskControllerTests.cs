using Xunit;
using System.Collections.Generic;
using System.Linq;
using TaskHandlerAPI.Controllers;
using TaskHandlerAPI.Data;
using TaskHandlerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TestProject.TaskControllerTests
{
  public class TaskControllerTests
  {
    [Fact]
    public async Task GetAllTasks_ReturnsAllTasks()
    {
      var tasks = new List<TaskToDo>
        {
            new TaskToDo { Id = 1, Text = "Task 1", Day = "Monday", Reminder = true },
            new TaskToDo { Id = 2, Text = "Task 2", Day = "Tuesday", Reminder = false },
        };

      var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;

      using (var context = new DataContext(options))
      {
        context.TasksToDo.AddRange(tasks);
        context.SaveChanges();
      }

      using (var context = new DataContext(options))
      {
        var controller = new TaskController(context);

        
        var result = await controller.GetAllTasks();

        var okResult = Assert.IsType<ActionResult<IEnumerable<TaskToDo>>>(result);
        


      }


    }

  }
  
}
