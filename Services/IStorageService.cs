using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tasks.Services
{
    public interface IStorageService
    {
         Task<(bool IsSuccess, Exception exception)> InsertTaskAsync(Entity.Tasking task);
         Task<(bool isSuccess, Exception exception)> UpdateTaskAsync(Entity.Tasking task);
        Task<(bool IsSuccess, Exception exception)> RemoveAsync(Guid Id);
         
         Task<Entity.Tasking> GetTaskingAsync(Guid Id);
         Task<bool> ExistsAsync(Guid Id);

         Task<List<Entity.Tasking>> GetTasksAsync(
            Guid id = default(Guid),
            string title = default(string),
            string description = default(string),
            string tags = default(string),
            Entity.ETaskPriorety? priorety = null,
            Entity.ETaskRepeat? repeat= null,
            Entity.ETaskStatus? status = null,
            DateTimeOffset onADay = default(DateTimeOffset),
            DateTimeOffset atATime = default(DateTimeOffset),
            string location = default(string),
            string url = default(string));
    }
}