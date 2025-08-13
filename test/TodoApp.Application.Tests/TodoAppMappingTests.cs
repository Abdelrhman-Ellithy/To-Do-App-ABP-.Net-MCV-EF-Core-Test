using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using Xunit;

namespace TodoApp
{
    public class TodoAppMappingTests : TodoAppApplicationTestBase
    {
        private readonly IMapper _mapper;

        public TodoAppMappingTests()
        {
            _mapper = GetRequiredService<IMapper>();
        }

        [Fact]
        public void Should_Map_TodoItem_To_TodoItemDto()
        {
            var todoItem = new TodoItem
            {
                Text = "Test Todo Item"
            };

            var dto = _mapper.Map<TodoItem, TodoItemDto>(todoItem);

            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(todoItem.Id);
            dto.Text.ShouldBe(todoItem.Text);
        }

        [Fact]
        public void Should_Handle_Null_TodoItem()
        {
            var dto = _mapper.Map<TodoItemDto>(null as TodoItem);
            dto.ShouldBeNull();
        }
    }
}
