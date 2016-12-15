using MvvmCross.Core.ViewModels;
using MvxTasky.Core.Services.Todo;

namespace MvxTasky.Core.ViewModels
{
    public class TodoItemDetailViewModel : MvxViewModel
    {
        #region メンバー変数
        private ITodoItemService _service;
        #endregion

        #region コンストラクタ

        public TodoItemDetailViewModel(ITodoItemService todoItemService)
        {
            _service = todoItemService;
        }

        public void Init(TodoItem param)
        {
            ID = param.ID;
            Name = param.Name;
            Notes = param.Notes;
            Done = param.Done;
        }
        #endregion

        #region プロパティ

        private int _Id;
        public int ID
        {
            get { return _Id; }
            set
            {
                _Id = value;
                RaisePropertyChanged("ID");
                RaisePropertyChanged(() => CanExecuteDelete);
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged("Name");
                RaisePropertyChanged(() => CanExecuteSave);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; RaisePropertyChanged("Notes"); }
        }


        private bool _Done;
        public bool Done
        {
            get { return _Done; }
            set { _Done = value; RaisePropertyChanged("Done"); }
        }
        #endregion

        #region DeleteCommand

        MvxCommand _DeleteCommand;
        public MvxCommand DeleteCommand
        {
            get
            {
                _DeleteCommand = _DeleteCommand ?? new MvxCommand(ExecuteDelete, (() => CanExecuteDelete));
                return _DeleteCommand;
            }
        }
        public bool CanExecuteDelete
        {
            get
            {
                return _service.GetTask(this.ID) == null ? false : true;
            }
        }
        private void ExecuteDelete()
        {
            _service.DeleteTask(ID);
            Close(this);
        }
        #endregion

        #region SaveCommand
        MvxCommand _SaveCommand;
        public MvxCommand SaveCommand
        {
            get
            {
                _SaveCommand = _SaveCommand ?? new MvxCommand(ExecuteSave, () => CanExecuteSave);
                return _SaveCommand;
            }
        }

        public bool CanExecuteSave
        {
            get
            {
                return (!string.IsNullOrEmpty(Name));
            }
        }
        private void ExecuteSave()
        {
            var item = new TodoItem()
            {
                ID = this.ID,
                Name = this.Name,
                Notes = this.Notes,
                Done = this.Done
            };

            if(_service.GetTask(item.ID) == null)
            {
                _service.CreateTask(item);
            }else
            {
                _service.UpdateTask(item);
            }
            Close(this);
        }
        #endregion

        #region CloseCommand
        MvxCommand _CloseCommand;
        public MvxCommand CloseCommand
        {
            get
            {
                _CloseCommand = _CloseCommand ?? new MvxCommand(() => { Close(this); });
                return _CloseCommand;
            }
        }
        #endregion
    }
}
