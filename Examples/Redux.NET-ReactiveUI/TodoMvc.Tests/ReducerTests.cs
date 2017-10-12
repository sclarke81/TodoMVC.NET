using System;
using System.Linq;
using FluentAssertions;
using Redux;
using TodoMvc.States;
using Xunit;

namespace TodoMvc.Tests
{
    public static class ReducerTests
    {
        [Fact]
        public static void Todo_can_be_added()
        {
            // Arrange
            string todoTitle = "title";
            Store<ApplicationState> store = StoreHelpers.GetStore();

            // Act
            store.Dispatch(new TodoAddedAction() { Text = todoTitle });

            // Assert
            store.GetState().Todos.Count().Should().Be(1);
            store.GetState().Todos.First().Title.Should().Be(todoTitle);
        }

        [Fact]
        public static void Todo_can_be_deleted()
        {
            // Arrange
            var id = new Guid();
            Store<ApplicationState> store = StoreHelpers
                .GetStore()
                .AddTodo("title", id, false);

            // Act
            store.Dispatch(new TodoDeletedAction() { Id = id });

            // Assert
            store.GetState().Todos.Count().Should().Be(0);
        }

        [Fact]
        public static void Todo_can_be_edited()
        {
            // Arrange
            var id = new Guid();
            string newTitle = "new title";
            Store<ApplicationState> store = StoreHelpers
               .GetStore()
               .AddTodo("title", id, false);

            // Act
            store.Dispatch(new TodoEditedAction() { Id = id, Text = newTitle });

            // Assert
            store.GetState().Todos.First().Title.Should().Be(newTitle);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public static void Todo_iscomplete_can_be_changed(bool isComplete)
        {
            // Arrange
            var id = new Guid();
            Store<ApplicationState> store = StoreHelpers
              .GetStore()
              .AddTodo("title", id, !isComplete);

            // Act
            store.Dispatch(new TodoIsCompletedChangedAction() { Id = id, IsCompleted = isComplete });

            // Assert
            store.GetState().Todos.First().IsCompleted.Should().Be(isComplete);
        }

        [Fact]
        public static void Completed_todos_can_be_cleared()
        {
            // Arrange
            Store<ApplicationState> store = StoreHelpers
                .GetStore()
                .AddTodo("title1", new Guid(), false)
                .AddTodo("title2", new Guid(), true);

            // Act
            store.Dispatch(new CompletedTodosClearedAction());

            // Assert
            store.GetState().Todos.Where(t => t.IsCompleted).Count().Should().Be(0);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public static void All_todos_iscomplete_can_be_changed(bool isComplete)
        {
            // Arrange
            Store<ApplicationState> store = StoreHelpers
               .GetStore()
               .AddTodo("title1", new Guid(), false)
               .AddTodo("title2", new Guid(), true);

            // Act
            store.Dispatch(new AllTodosIsCompletedChangedAction() { IsCompleted = isComplete });

            // Assert
            store.GetState().Todos.Where(t => t.IsCompleted == isComplete).Count().Should().Be(2);
        }

        [Theory]
        [InlineData(TodosFilter.All)]
        [InlineData(TodosFilter.Active)]
        [InlineData(TodosFilter.Completed)]
        public static void Filter_can_be_changed(TodosFilter filter)
        {
            // Arrange
            Store<ApplicationState> store = StoreHelpers.GetStore();

            // Act
            store.Dispatch(new FilterChangedAction() { Filter = filter });

            // Assert
            store.GetState().Filter.Should().Be(filter);
        }
    }
}
