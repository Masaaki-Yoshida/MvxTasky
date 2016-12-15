using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvxTasky.Core.Services.Todo;

namespace MvxTasky.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            Mvx.ConstructAndRegisterSingleton<ITodoItemService, TodoItemServiceOnMemory>();
            RegisterAppStart<ViewModels.TodoItemListViewModel>();
        }
    }
}
