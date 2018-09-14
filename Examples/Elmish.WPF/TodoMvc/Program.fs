module Program

open System
open Elmish
open Elmish.WPF
open TodoMvc.Views
open TodoMvc.Types

// MODEL
type Todo =
    { Title : string
      IsCompleted : bool
      Id : Guid }

type Model =
    { Todos : Todo list
      Filter : TodosFilter
      NewTitle : string }

let newTodo title =
    { Title = title
      IsCompleted = false
      Id = Guid.NewGuid() }

let init () =
    { Todos = [newTodo "test1"; newTodo "test2"]
      Filter = TodosFilter.All
      NewTitle = "" }

// UPDATE
type Msg =
    | UpdateTitle of string
    | Add of string
    | Edit of Guid*string
    | Delete of Guid
    | ChangeCompletedState of Guid*bool
    | ChangeAllCompletedStates of bool
    | ClearCompleted
    | ChangeFilter of TodosFilter

let modifyTodoById todos id modifier =
    todos |> List.map (fun t ->
        if t.Id = id then modifier t
        else t )

let addTodo model title =
    { model with Todos = if System.String.IsNullOrEmpty title then model.Todos else model.Todos @ [newTodo title] }

let update msg model =
    match msg with
    | UpdateTitle title -> { model with NewTitle = title }
    | Add title -> {addTodo model title with NewTitle = "" }
    | Edit (id,title) -> { model with Todos = modifyTodoById model.Todos id (fun t -> {t with Title = title}) }
    | Delete id -> { model with Todos = List.filter (fun t -> t.Id <> id) model.Todos }
    | ChangeCompletedState (id,isCompleted) -> { model with Todos = modifyTodoById model.Todos id (fun t -> {t with IsCompleted = isCompleted}) }
    | ChangeAllCompletedStates isCompleted -> { model with Todos = List.map (fun t -> {t with IsCompleted = isCompleted}) model.Todos }
    | ClearCompleted -> { model with Todos = List.filter (fun t -> not t.IsCompleted) model.Todos }
    | ChangeFilter filter -> { model with Filter = filter }

let counterMessage model =
    let activeTodoCount = List.length (List.map (fun x -> x.IsCompleted) model.Todos)
    let itemWord = if activeTodoCount <= 1 then "item" else "items"
    sprintf "%d %s left" activeTodoCount itemWord

let filteredTodos todos filter =
    match filter with
    | TodosFilter.Active -> List.filter (fun t -> not t.IsCompleted) todos
    | TodosFilter.Completed -> List.filter (fun t -> t.IsCompleted) todos
    | _ -> todos

let getTodoById todos id =
    todos |> List.filter (fun t -> t.Id = id) |> Seq.exactlyOne

let bindings model dispatch =
  [
    "MainAndFooterAreVisible" |> Binding.oneWay (fun m -> not m.Todos.IsEmpty)

    // Header
    "NewTitle" |> Binding.twoWay
        (fun m -> m.NewTitle)
        (fun t m -> UpdateTitle t)
    "Add" |>  Binding.cmd
        (fun m -> Add m.NewTitle)
    "MarkAllAsComplete" |> Binding.twoWay
        (fun m -> List.forall (fun x -> x.IsCompleted) m.Todos)
        (fun v m -> v |> ChangeAllCompletedStates)

    // Main
    "Todos" |> Binding.oneWaySeq
        (fun m -> filteredTodos m.Todos m.Filter)
        (fun t -> t.Id)
        (=)
    "Delete" |> Binding.paramCmd
        (fun p m -> Delete (p :?> Guid))

    "ChangeCompletedState" |> Binding.paramCmd (fun p m ->
        let (id,isCompleted) = p :?> Object*Object
        ChangeCompletedState (id :?> Guid,isCompleted :?> bool))

    // Footer
    "Counter" |> Binding.oneWay (fun m -> counterMessage m)
    "Filter" |> Binding.twoWay
        (fun m -> m.Filter)
        (fun v m -> v |> ChangeFilter)
    "ClearCompleted" |> Binding.cmd (fun m -> ClearCompleted)
    "ClearCompletedIsVisible" |> Binding.oneWay (fun m -> List.exists (fun t -> t.IsCompleted) m.Todos)
  ]

//Header title
//SelectedFilter

[<EntryPoint; STAThread>]
let main argv =
  Program.mkSimple init update bindings
  |> Program.withConsoleTrace
  |> Program.runWindowWithConfig
      { ElmConfig.Default with LogConsole = true }
      (MainWindow())
