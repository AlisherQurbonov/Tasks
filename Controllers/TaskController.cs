using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}