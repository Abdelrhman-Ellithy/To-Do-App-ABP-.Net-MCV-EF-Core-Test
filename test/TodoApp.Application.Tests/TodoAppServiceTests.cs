using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace TodoApp
{
    public class TodoAppServiceTests : TodoAppApplicationTestBase
    {
        private readonly ITodoAppService _todoAppService;

        public TodoAppServiceTests()
        {
            _todoAppService = GetRequiredService<ITodoAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Valid_TodoItem()
        {
            var todoText = "test";
            var result = await _todoAppService.CreateAsync(todoText);
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.Text.ShouldBe(todoText);
        }

        [Fact]
        public async Task Should_Get_TodoItem_List()
        {
            await _todoAppService.CreateAsync("1");
            await _todoAppService.CreateAsync("2");
            var result = await _todoAppService.GetListAsync();
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Should_Delete_TodoItem()
        {
            var todo = await _todoAppService.CreateAsync("delete");
            
            await _todoAppService.DeleteAsync(todo.Id);
            
            var items = await _todoAppService.GetListAsync();
            items.ShouldNotContain(x => x.Id == todo.Id);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Deleting_NonExistent_Item()
        {
            var nonExistentId = Guid.NewGuid();

            await Should.ThrowAsync<InvalidOperationException>(
                async () => await _todoAppService.DeleteAsync(nonExistentId)
            );
        }
    }
}
