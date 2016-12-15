using MvvmCross.Core.ViewModels;
using MvxTasky.Core.Services.Todo;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvxTasky.Core.ViewModels
{
    public class TodoItemListViewModel : MvxViewModel
    {
        #region メンバー変数
        private ITodoItemService _service;
        #endregion

        #region プロパティ
        public ObservableCollection<TodoItem> Items { get; set; }
        #endregion

        #region コンストラクタ

        public TodoItemListViewModel(ITodoItemService todoItemService)
        {
            _service = todoItemService;
        }

        public override void Start()
        {
            Items = _service.GetTasks();
        }

        #endregion

        #region EditCommand

        private MvxCommand<TodoItem> _EditCommand;
        public MvxCommand<TodoItem> EditCommand
        {
            get
            {
                _EditCommand = _EditCommand ?? new MvxCommand<TodoItem>(ExecuteEdit);
                return _EditCommand;
            }
        }

        private void ExecuteEdit(TodoItem param)
        {
            NavigationNext(param);
        }
        #endregion

        #region AddCommand
        private MvxCommand _AddCommand;
        public MvxCommand AddCommand
        {
            get
            {
                _AddCommand = _AddCommand ?? new MvxCommand(ExecuteAdd);
                return _AddCommand;
            }
        }

        private void ExecuteAdd()
        {
            var param = new TodoItem();
            param.ID = Items != null && Items.Count > 0 ? Items.Max(p => p.ID) + 1 : 1;
            NavigationNext(param);
        }

        public void NavigationNext(TodoItem param)
        {
            ShowViewModel<TodoItemDetailViewModel>(param);
        }

        #endregion
    }
}
