using DorisTodo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorisTodo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public TodoController(TodoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // for now i don't want to deal with manage DateTime overhead
            var todoItem = new Models.TodoItem { Content = request.Content, CreatedAt = GetCurrentDateTimeAtVietnam() };
            var addTodoTask = await _dbContext.TodoItems.AddAsync(todoItem);
            var saveChangeTask = await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsync), new { id = todoItem.Id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var todo = await _dbContext.TodoItems.SingleOrDefaultAsync(t => t.Id == id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _dbContext.TodoItems.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAsync(int id)
        {
            var todo = await _dbContext.TodoItems.SingleOrDefaultAsync(t => t.Id == id);

            if (todo is null)
            {
                return BadRequest();
            }

            _dbContext.TodoItems.Remove(todo);
            await _dbContext.SaveChangesAsync();

            return Ok(todo);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _dbContext.TodoItems.SingleOrDefaultAsync(t => t.Id == id);

            if (todo is null)
            {
                return BadRequest();
            }

            todo.Content = request.Content;
            await _dbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> MarkDoneAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _dbContext.TodoItems.SingleOrDefaultAsync(t => t.Id == id);

            if (todo is null)
            {
                return BadRequest();
            }

            todo.IsDone = true;
            todo.DoneAt = GetCurrentDateTimeAtVietnam();
            await _dbContext.SaveChangesAsync();

            return Ok(todo);
        }

        private DateTime GetCurrentDateTimeAtVietnam()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now,
                             TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh"));
        }

    }
}
