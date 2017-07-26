# Requirements

These requirements are based on the [TodoMVC app spec] but tweaked slightly to remove the Javascript references. The [Backbone] example implementation of TodoMVC provides a nice demonstration of the desired behaviour.

[TodoMVC app spec]:(https://github.com/tastejs/todomvc/blob/master/app-spec.md)
[Backbone]:(http://todomvc.com/examples/backbone/)

## Visual layout

There are three sections:

1. Header - Has a checkbox on the left and text entry on the rest of the row.
1. Main - Contains one row per todo. Each todo has a checkbox on the left, a cancel button on the right and the todo title in the middle.
1. Footer - A single row that has a counter on the left, a clear completed button on the right and filter selection inthe middle.

## Functionality

### No todos

When there are no todos the main and footer sections should be hidden.

### New todo

New todos are entered in the input at the top of the app. The input element should be focused when the page is loaded, preferably by using the autofocus input attribute. Pressing Enter creates the todo, appends it to the todo list, and clears the input. Make sure to trim the input and then check that it's not empty before creating a new todo.

### Mark all as complete

This checkbox toggles all the todos to the same state as itself. Make sure to clear the checked state after the "Clear completed" button is clicked. The "Mark all as complete" checkbox should also be updated when single todo items are checked/unchecked. E.g. When all the todos are checked it should also get checked.

### Item

A todo item has three possible interactions:

1. Clicking the checkbox marks the todo as complete by updating its completed value.
1. Double-clicking the todo text activates editing mode.
1. Hovering over the todo shows the remove button.

### Editing

When editing mode is activated it will hide the other controls and bring forward an input that contains the todo title, which should be focused. The edit should be saved on both losing focus and enter, and the editing class should be removed. Make sure to trim the input and then check that it's not empty. If it's empty the todo should instead be destroyed. If escape is pressed during the edit, the edit state should be left and any changes be discarded.

### Counter

Displays the number of active todos in a pluralized form. Also make sure to pluralize the item word correctly: 0 items, 1 item, 2 items. Example: 2 items left

### Clear completed button

Removes completed todos when clicked. Should be hidden when there are no completed todos.

### Persistence

Your app should dynamically persist the todos to localStorage. If the framework has capabilities for persisting data (e.g. Backbone.sync), use that. Otherwise, use vanilla localStorage. If possible, use the keys id, title, completed for each item. Make sure to use this format for the localStorage name: todos-[framework]. Editing mode should not be persisted.

### Filtering

The following filtering should be implemented:

- All (default)
- Active
- Completed

When the filter changes, the todo list should be filtered on a model level and the selected class on the filter links should be toggled. When an item is updated while in a filtered state, it should be updated accordingly. E.g. if the filter is Active and the item is checked, it should be hidden. Make sure the active filter is persisted on reload.