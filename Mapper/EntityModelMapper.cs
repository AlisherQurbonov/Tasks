using System;
using System.Linq;
using tasks.Model;

namespace tasks.Mapper
{
      public static class EntityModelMapper
  {
      public static Model.NewTask ToTaskModel(this Entity.Task newTask)
        {
            return new Model.NewTask()
            {
                Title = newTask.Title,
                Description = newTask.Description,
                Tags = newTask.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                Location = new TaskLocation(){Latitude=Convert.ToDouble(newTask.Location.Split(',')[0]), Longitude=Convert.ToDouble(newTask.Location.Split(',')[1])},
                AtATime = newTask.AtATime,
                OnADay = newTask.OnADay,
                Repeat = newTask.Repeat.ToModelETaskRepeat(),
                Status = newTask.Status.ToModelETaskStatus(),
                Priority = newTask.Priority.ToModelETaskPriority(),
                Url = newTask.Url,
            };
        }
   }
}