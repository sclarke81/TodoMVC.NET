using System;
using System.Linq;
using FluentAssertions;
using Redux;
using TodoMvc.States;
using Xunit;
using TodoMvc.Store;
using TodoMvc.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace TodoMvc.Tests
{
    public static class HeaderViewModelTests
    {
        [Theory]
        [InlineData(0, Visibility.Hidden)]
        [InlineData(1, Visibility.Visible)]
        public static void MarkAllAsComplete_VisibilityIsCorrect(int numberOfTodos, Visibility expectedVisibility)
        {
            // Arrange
            Store<ApplicationState> store = StoreHelpers.GetStore();
            for (int i = 0; i < numberOfTodos; i++)
            {
                store.AddTodo("title", new Guid(), false);
            }
            var model = new HeaderViewModel(store);

            // Act

            // Assert
            model.MarkAllAsCompleteVisibility.Should().Be(expectedVisibility);
        }

        [Theory]
        [InlineData(0, true, false)]
        [InlineData(2, false, false)]
        [InlineData(2, true, true)]
        public static void MarkAllAsComplete_IsCheckedIsCorrect(int numberOfTodos, bool todoIsComplete, bool expectedIsChecked)
        {
            // Arrange
            Store<ApplicationState> store = StoreHelpers.GetStore();
            for (int i = 0; i < numberOfTodos; i++)
            {
                store.AddTodo("title", new Guid(), todoIsComplete);
            }
            var model = new HeaderViewModel(store);

            // Act

            // Assert
            model.MarkAllAsCompleteIsChecked.Should().Be(expectedIsChecked);
        }

        [Fact]
        public static void TodoAdded_CommandWorks()
        {
            // Arrange
            var actionLogger = new LoggingMiddleware();
            Store<ApplicationState> store = StoreHelpers.GetStore()
                .AddMiddlewares(actionLogger.Middleware);
            var model = new HeaderViewModel(store);

            // Act
            ((ICommand)model.TodoAdded).Execute("");

            // Assert
            actionLogger.LoggedActions.Where(a => a is TodoAddedAction).Count().Should().Be(1);
        }

        [Fact]
        public static void MarkAllAsComplete_CommandWorks()
        {
            // Arrange
            var actionLogger = new LoggingMiddleware();
            Store<ApplicationState> store = StoreHelpers.GetStore()
                .AddMiddlewares(actionLogger.Middleware);
            var model = new HeaderViewModel(store);

            // Act
            ((ICommand)model.MarkAllAsComplete).Execute(true);

            // Assert
            actionLogger.LoggedActions.Where(a => a is AllTodosIsCompletedChangedAction).Count().Should().Be(1);
        }
    }
}
