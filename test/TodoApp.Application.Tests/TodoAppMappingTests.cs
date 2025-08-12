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
            // Arrange
            var todoItem = new TodoItem
            {
                Text = "Test Todo Item"
            };

            // Act
            var dto = _mapper.Map<TodoItem, TodoItemDto>(todoItem);

            // Assert
            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(todoItem.Id);
            dto.Text.ShouldBe(todoItem.Text);
        }


        [Fact]
        public void Should_Handle_Null_TodoItem()
        {
            // Act
            var dto = _mapper.Map<TodoItemDto>(null as TodoItem);

            // Assert
            dto.ShouldBeNull();
        }
    }
}
