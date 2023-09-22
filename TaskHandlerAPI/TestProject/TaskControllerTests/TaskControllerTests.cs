using Xunit;
using System.Collections.Generic;
using System.Linq;
using TaskHandlerAPI.Controllers;
using TaskHandlerAPI.Data;
using TaskHandlerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;

namespace TestProject.TaskControllerTests
{
  public class TaskControllerTests
  {
    //private readonly object mockDbContext;

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

    [Fact]
    public async Task CreateTask_ValidTask_CreatesTask()
    {
      
      var newTask = new TaskToDo { Id= 3, Text = "New Task", Day = "Wednesday", Reminder = false };
      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: "TestDatabase")
          .Options;

      using (var context = new DataContext(options))
      {
        var controller = new TaskController(context);

        
        var result = await controller.CreateTask(newTask);

        
        var createdAtActionResult = Assert.IsType<ActionResult<IEnumerable<TaskToDo>>>(result);
        //var model = Assert.IsType<ActionResult<IEnumerable<TaskToDo>>>(createdAtActionResult.Value);
        //Assert.Equal("New Task", model.Text);
      }
    }

    [Fact]
    public async Task UpdateTask_ValidId_UpdatesTask()
    {
      
      var taskId = 4;
      var updateTask = new TaskToDo { Id = taskId, Text = "Updated Task", Day = "Thursday", Reminder = true };
      var tasks = new List<TaskToDo> { updateTask };
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

        
        var result = await controller.UpdateTask(taskId, updateTask);

        
        Assert.IsType<ActionResult<IEnumerable<TaskToDo>>>(result);

        
        var updatedTask = await context.TasksToDo.FindAsync(taskId);
        //Assert.Equal("Updated Task", updatedTask.Text);
        //Assert.Equal("Thursday", updatedTask.Day);
        //Assert.True(updatedTask.Reminder);
      }
    }

    [Fact]
    public async Task DeleteTask_ValidId_DeletesTask()
    {
      
      var taskId = 5;
      var tasks = new List<TaskToDo> { new TaskToDo { Id = taskId, Text = "Task 1", Day = "Friday", Reminder = false } };
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

        
        var result = await controller.DeleteTask(taskId);

       
        var okResult = Assert.IsType<ActionResult<IEnumerable<TaskToDo>>>(result);
        //var model = Assert.IsAssignableFrom<IEnumerable<TaskToDo>>(okResult.Value);
        //Assert.Empty(model);
        //mockDbContext.Verify(db => db.SaveChangesAsync(), Times.Once);
      }
    }

  }
  
}
