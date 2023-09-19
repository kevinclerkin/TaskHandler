using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskHandlerAPI.Data;
using TaskHandlerAPI.Models;



namespace TaskHandlerAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
    private readonly DataContext _dataContext;

    public TaskController(DataContext context)
    {
      _dataContext = context;
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskToDo>>> GetAllTasks()
    {
      var tasks = await _dataContext.TasksToDo.ToListAsync();
      return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<TaskToDo>>> CreateTask(TaskToDo task)
    {
      _dataContext.TasksToDo.Add(task);
      await _dataContext.SaveChangesAsync();

      return Ok(await _dataContext.TasksToDo.ToListAsync());

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<TaskToDo>>> UpdateTask(int id, TaskToDo updateReminder)
    {
      var dbTask = await _dataContext.TasksToDo.FindAsync(id);

      if (dbTask == null)
      {
        return BadRequest("Task not found!");
      }

      dbTask.Reminder = updateReminder.Reminder;

      await _dataContext.SaveChangesAsync();

      return Ok(await _dataContext.TasksToDo.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<TaskToDo>>> DeleteTask(int id)
    {
      var dbTask = await _dataContext.TasksToDo.FindAsync(id);
      if(dbTask == null)
      {
        return BadRequest("Task not found");
      }

      _dataContext.TasksToDo.Remove(dbTask);

      await _dataContext.SaveChangesAsync();

      return Ok(await _dataContext.TasksToDo.ToListAsync());

    }



  }
}
