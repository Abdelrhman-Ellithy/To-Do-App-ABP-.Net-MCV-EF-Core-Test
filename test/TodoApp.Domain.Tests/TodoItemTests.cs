using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace TodoApp
{
    public class TodoItemTests : TodoAppDomainTestBase
    {
        [Fact]
        public void Should_Create_TodoItem_With_Valid_Text()
        {
            // Arrange & Act
            var todoText = "Complete domain tests";
            var todoItem = new TodoItem { Text = todoText };

            // Assert
            todoItem.ShouldNotBeNull();
            todoItem.Text.ShouldBe(todoText);
        }



        [Fact]
        public void Should_Update_Text()
        {
            // Arrange
            var todoItem = new TodoItem { Text = "Initial text" };
            var newText = "Updated text";

            // Act
            todoItem.Text = newText;

            // Assert
            todoItem.Text.ShouldBe( newText);
        }

    }
}
