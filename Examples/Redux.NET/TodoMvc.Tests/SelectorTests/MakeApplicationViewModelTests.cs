using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using Redux;
using TodoMvc.States;
using TodoMvc.ViewModels;
using Xunit;

namespace TodoMvc.Tests.SelectorTests
{
    public static class MakeApplicationViewModelTests
    {
        [Fact]
        public static void Main_and_footer_are_visible_when_todo_count_greater_than_0()
        {
            // Arrange
            var store = new Store<ApplicationState>(Reducers.ReduceApplication, new ApplicationState()
            {
                Filter = TodosFilter.All,
                Todos = ImmutableArray.Create<Todo>(new Todo
                {
                    Title = "title",
                    Id = new Guid(),
                    IsCompleted = false
                })
            });

            // Act
            ApplicationViewModel state = Selectors.MakeApplicationViewModel(store.GetState());

            // Assert
            state.MainAndFooterAreVisible.Should().Be(true);
        }

        [Fact]
        public static void Main_and_footer_are_not_visible_when_todo_count_is_0()
        {
            // Arrange
            var store = new Store<ApplicationState>(Reducers.ReduceApplication, new ApplicationState()
            {
                Filter = TodosFilter.All,
                Todos = ImmutableArray.Create<Todo>()
            });

            // Act
            ApplicationViewModel state = Selectors.MakeApplicationViewModel(store.GetState());

            // Assert
            state.MainAndFooterAreVisible.Should().Be(false);
        }
    }
}
