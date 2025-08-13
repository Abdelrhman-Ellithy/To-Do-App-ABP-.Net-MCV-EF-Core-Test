using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace TodoApp.EntityFrameworkCore
{
    public class TodoItemRepositoryTests : TodoAppEntityFrameworkCoreTestBase
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;

        public TodoItemRepositoryTests()
        {
            _todoItemRepository = GetRequiredService<IRepository<TodoItem, Guid>>();
        }

        [Fact]
        public async Task Should_Insert_TodoItem()
        {
            var todoItem = new TodoItem { Text = "Test todo" };

            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.InsertAsync(todoItem);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var savedItem = await _todoItemRepository.FindAsync(todoItem.Id);
                savedItem.ShouldNotBeNull();
                savedItem.Text.ShouldBe("Test todo");
            });
        }

        [Fact]
        public async Task Should_Get_List_Of_TodoItems()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Item 1" });
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Item 2" });
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Item 3" });
            });

            var items = await WithUnitOfWorkAsync(async () =>
            {
                return await _todoItemRepository.GetListAsync();
            });

            items.Count.ShouldBeGreaterThanOrEqualTo(3);
            items.ShouldContain(x => x.Text == "Item 1");
            items.ShouldContain(x => x.Text == "Item 2");
            items.ShouldContain(x => x.Text == "Item 3");
        }

        [Fact]
        public async Task Should_Delete_TodoItem()
        {
            var todoItem = new TodoItem { Text = "Will be deleted" };
            
            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.InsertAsync(todoItem);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.DeleteAsync(todoItem.Id);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var deletedItem = await _todoItemRepository.FindAsync(todoItem.Id);
                deletedItem.ShouldBeNull();
            });
        }

        [Fact]
        public async Task Should_Update_TodoItem()
        {
            var todoItem = new TodoItem { Text = "Original text" };
            
            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.InsertAsync(todoItem);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var item = await _todoItemRepository.GetAsync(todoItem.Id);
                item.Text = "Updated text";
                await _todoItemRepository.UpdateAsync(item);
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var updatedItem = await _todoItemRepository.FindAsync(todoItem.Id);
                updatedItem.ShouldNotBeNull();
                updatedItem.Text.ShouldBe("Updated text");
            });
        }

        [Fact]
        public async Task Should_Query_TodoItems()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Write code" });
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Write tests" });
                await _todoItemRepository.InsertAsync(new TodoItem { Text = "Something else" });
            });

            var items = await WithUnitOfWorkAsync(async () =>
            {
                return await _todoItemRepository.GetListAsync();
            });

            var filteredItems = items.Where(x => x.Text.Contains("Write")).ToList();

            filteredItems.Count.ShouldBe(2);
            filteredItems.ShouldContain(x => x.Text == "Write code");
            filteredItems.ShouldContain(x => x.Text == "Write tests");
        }
    }
}
