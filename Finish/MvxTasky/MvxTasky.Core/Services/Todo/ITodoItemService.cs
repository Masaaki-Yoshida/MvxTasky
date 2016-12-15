using System.Collections.ObjectModel;

namespace MvxTasky.Core.Services.Todo
{
    public interface ITodoItemService
    {
        TodoItem GetTask(int id);
        bool CreateTask(TodoItem item);
        bool UpdateTask(TodoItem item);
        bool DeleteTask(int id);
        ObservableCollection<TodoItem> GetTasks();
    }
}
