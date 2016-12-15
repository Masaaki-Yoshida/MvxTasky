using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvxTasky.Core.Services.Todo
{
    public class TodoItemServiceOnMemory : ITodoItemService
    {
        private ObservableCollection<TodoItem> _items = new ObservableCollection<TodoItem>();

        public bool CreateTask(TodoItem item)
        {
            _items.Add(item);
            return true;
        }

        public bool DeleteTask(int id)
        {
            try
            {
                return _items.Remove(_items.Where(p => p.ID == id).FirstOrDefault());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TodoItem GetTask(int id)
        {
            return _items.Where(p => p.ID == id).FirstOrDefault();
        }

        public ObservableCollection<TodoItem> GetTasks()
        {
            return _items;
        }

        public bool UpdateTask(TodoItem item)
        {
            TodoItem target = _items.Where(p => p.ID == item.ID).FirstOrDefault();
            if (target != null)
            {
                //更新
                target.Name = item.Name;
                target.Notes = item.Notes;
                target.Done = item.Done;
                return true;
            }
            return false;
        }
    }
}
