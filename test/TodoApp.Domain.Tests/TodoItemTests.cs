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
            var todoText = "Complete domain tests";
            var todoItem = new TodoItem { Text = todoText };

            todoItem.ShouldNotBeNull();
            todoItem.Text.ShouldBe(todoText);
            todoItem.Id.ShouldBe(Guid.Empty);
        }

        [Fact]
        public void Should_Update_Text()
        {
            var todoItem = new TodoItem { Text = "Initial text" };
            var newText = "Updated text";

            todoItem.Text = newText;

            todoItem.Text.ShouldBe(newText);
        }

        [Fact]
        public void Should_Not_Accept_Too_Long_Text()
        {
            var longText = new string('x', 1001);

            Should.Throw<ArgumentException>(() =>
                new TodoItem { Text = longText }
            );
        }
    }
}
