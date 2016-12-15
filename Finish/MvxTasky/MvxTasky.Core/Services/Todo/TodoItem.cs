using SQLite;

namespace MvxTasky.Core.Services.Todo
{
    public class TodoItem
    {
        [PrimaryKey,NotNull]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}
