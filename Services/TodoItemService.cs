using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            return await _context.TodoItems.Where(item => !item.IsDone).ToArrayAsync();
        }

        public async Task<bool> AddItemAsync(NewTodoItem newItem)
        {
            var dueDate = newItem.DueAt.HasValue && newItem.DueAt.Value > DateTime.Now ? newItem.DueAt.Value : DateTimeOffset.Now.AddDays(3);
            var todo = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = dueDate
            };

            _context.TodoItems.Add(todo);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return false;
            todo.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
