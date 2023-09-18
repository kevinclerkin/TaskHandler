using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskHandlerAPI.Models;

namespace TaskHandlerAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TaskController : ControllerBase
  {
    [HttpGet]
    public async Task<ActionResult<List<TaskToDo>>> GetAllTasks()
    {
      return new List<TaskToDo>
      {
        new TaskToDo { Id = 1, Text = "Laundry", Day = "Wednesday 13th May", Reminder = false}
      };
    }
     
  }
}
