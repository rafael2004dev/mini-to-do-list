using Microsoft.AspNetCore.Mvc;

namespace Mini_API.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> Get()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult<TaskItem> Post(TaskItem task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return Created("", task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}