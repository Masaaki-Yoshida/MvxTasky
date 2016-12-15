using Acr.UserDialogs;
using MvvmCross.Platform;
using MvxTasky.Core.Services.Todo;

namespace MvxTasky.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            Mvx.ConstructAndRegisterSingleton<ITodoItemService, TodoItemServiceSQLite>();
            RegisterAppStart<ViewModels.TodoItemListViewModel>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

        }
    }
}
