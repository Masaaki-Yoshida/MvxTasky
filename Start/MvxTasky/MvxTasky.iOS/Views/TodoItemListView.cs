using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvxTasky.Core.ViewModels;
using System;

using UIKit;

namespace MvxTasky.iOS.Views
{
    public partial class TodoItemListView : MvxViewController
    {
        public TodoItemListView() : base("TodoItemListView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var addBtn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            addBtn.Clicked += delegate { };
            NavigationItem.SetRightBarButtonItem(addBtn,false);

            var set = this.CreateBindingSet<TodoItemListView, Core.ViewModels.TodoItemListViewModel>();
            set.Bind(addBtn).To(vm => vm.AddCommand);
            set.Apply();

            var source = new MvxStandardTableViewSource(TableView, UITableViewCellStyle.Subtitle, new NSString("MyCellId"), "TitleText Name; DetailText Notes;");
            this.CreateBinding(source).To<TodoItemListViewModel>(vm => vm.Items).Apply();
            this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<TodoItemListViewModel>(vm => vm.EditCommand).Apply();
            TableView.Source = source;
            TableView.ReloadData();
        }
    }
}