using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            IEnumerable<TodoItem> items = new[]
            {
                new TodoItem
                {
                    Title = "Learn .NET Core",
                    DueAt = DateTimeOffset.Now.AddDays(3)
                },
                new TodoItem
                {
                    Title = "Learn ES6",
                    DueAt = DateTimeOffset.Now.AddDays(5)
                },
                new TodoItem
                {
                    Title = "Learn React",
                    DueAt = DateTimeOffset.Now.AddDays(9)
                }
            };

            return Task.FromResult(items);
        }

        public async Task<bool> AddItemAsync(NewTodoItem newItem)
        {
            return true;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            return true;
        }
    }
}
