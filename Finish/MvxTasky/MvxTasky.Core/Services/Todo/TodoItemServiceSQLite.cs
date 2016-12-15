using MvvmCross.Plugins.Sqlite;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvxTasky.Core.Services.Todo
{
    public class TodoItemServiceSQLite : ITodoItemService
    {
        private readonly IMvxSqliteConnectionFactory _sqliteConnectionFactory;
        private readonly SQLiteConnection _con;

        public TodoItemServiceSQLite(IMvxSqliteConnectionFactory factory)
        {
            _sqliteConnectionFactory = factory;
            _con = factory.GetConnection("todoitem.sqlite");
            _con.CreateTable<TodoItem>();
        }

        public bool CreateTask(TodoItem item)
        {
            return _con.Insert(item) > 0 ? true : false;
        }

        public bool DeleteTask(int id)
        {
            return _con.Delete<TodoItem>(id) > 0 ? true : false;
        }

        public TodoItem GetTask(int id)
        {
            return _con.Table<TodoItem>().Where(p => p.ID == id).FirstOrDefault();
        }

        public ObservableCollection<TodoItem> GetTasks()
        {
            var tasks = _con.Table<TodoItem>().Where(p => p.ID != -1).ToList();
            return tasks == null ? null : new ObservableCollection<TodoItem>(tasks);
        }

        public bool UpdateTask(TodoItem item)
        {
            return _con.Update(item) > 0 ? true : false;
        }
    }
}
