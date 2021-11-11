using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tasks.Mapper;
using tasks.Model;
using tasks.Services;

namespace tasks.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IStorageService _storage;

        public TaskController(ILogger<TaskController> logger, IStorageService storage)
      {
          _logger = logger;
          _storage = storage;
      }

      [HttpPost]
      [Consumes(MediaTypeNames.Application.Json)]
      public async Task<IActionResult> CrateTask([FromBody]NewTask newTask)
      {
         var taskEntity = newTask.ToTaskEntity();
         var insertResult = await _storage.InsertTaskAsync(taskEntity);  

         if(insertResult.IsSuccess)
         {
             return CreatedAtAction("CreateTask", taskEntity);
         }

         return StatusCode((int)HttpStatusCode.InternalServerError, new {message = insertResult.exception.Message});
      }
    }
}