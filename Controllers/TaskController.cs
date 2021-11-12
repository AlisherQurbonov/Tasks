using System.Security.Principal;
using System.Net.Security;
using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using tasks.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tasks.Mapper;
using tasks.Model;
using tasks.Services;
using System.Threading.Tasks;

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
    //   Post bu yangi task yaratadi

       [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatedTask([FromBody]NewTask newTasking)
        {
            var taskEntity = newTasking.ToTaskEntity();
            var insertResult = await _storage.InsertTaskAsync(taskEntity);

            if(insertResult.IsSuccess)
            {
                return CreatedAtAction("CreatedTask", taskEntity); 
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = insertResult.exception.Message });
        }

        // [HttpPost]
        // [Consumes(MediaTypeNames.Application.Json)]
        // public async Task<IActionResult> CreateTask([FromBody]NewTask newTask)
        // {
        //     var taskEntity = newTask.ToTaskEntity();
        //     var insertResult = await _storage.InsertTaskAsync(taskEntity);

        //     if(insertResult.IsSuccess)
        //     {
        //         return CreatedAtAction("CreateTask", taskEntity); // task qaytarishdan avval modelga conver qilish kerak
        //     }

        //     return StatusCode((int)HttpStatusCode.InternalServerError, new { message = insertResult.exception.Message });
        // }

        
        
        // Get bu yangi taskni tekshiradi 

        [HttpGet]
        public async Task<IActionResult> QueryTasks([FromQuery]TaskQuery query)
        {
            var tasks = await _storage.GetTasksAsync(title: query.Title, id: query.Id);

            if(tasks.Any())
            {
                return Ok(tasks);
            }

            return NotFound("No tasks exist!");
        }
       


    //    Put bu taskni ismini uzgartirishni  xal qiladi

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync([FromBody]UpdatedTask updatedTask)
        {
            var entity = updatedTask.ToTaskEntity();
            var updateResult = await _storage.UpdateTaskAsync(entity);

            if(updateResult.isSuccess)
            {
                return Ok();
            }

            return BadRequest(updateResult.exception.Message);
        }

       
  

    //  Get id Id bilan tekshiradi
         [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetTask([FromRoute]Guid Id)
        {
            var task = await _storage.GetTaskingAsync(Id);
               

            if(task is default(Tasking))
            {
                return NotFound($"User with ID {Id} not found");
            }

            return Ok(task);
        }


        // Delete bu uchiradi

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {

            var tasking =  await _storage.RemoveAsync(Id);
           
            
            if(tasking.IsSuccess)
            {
                return Ok();
            }

            return NotFound(tasking.exception.Message);
        }
    }
}