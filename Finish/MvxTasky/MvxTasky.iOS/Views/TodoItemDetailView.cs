using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;

using UIKit;

namespace MvxTasky.iOS.Views
{
    public partial class TodoItemDetailView : MvxViewController
    {
        public TodoItemDetailView() : base("TodoItemDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            bindingInit();
            ctrlInit();
        }

        private void bindingInit()
        {
            var set = this.CreateBindingSet<TodoItemDetailView, Core.ViewModels.TodoItemDetailViewModel>();
            set.Bind(id).To(vm => vm.ID);
            set.Bind(name).To(vm => vm.Name);
            set.Bind(notes).To(vm => vm.Notes);
            set.Bind(done).To(vm => vm.Done);
            set.Bind(saveButton).To(vm => vm.SaveCommand);
            set.Bind(deleteButton).To(vm => vm.DeleteCommand);
            set.Bind(saveButton).For(v => v.Enabled).To(vm => vm.CanExecuteSave);
            set.Bind(deleteButton).For(v => v.Enabled).To(vm => vm.CanExecuteDelete);
            set.Apply();
        }

        private void ctrlInit()
        {
            #region Name
            //Enter
            name.ShouldReturn += (textField) =>
            {
                name.ResignFirstResponder();
                return true;
            };
            //欄外タップ
            var txtnameGesture = new UITapGestureRecognizer(() =>
            {
                this.name.ResignFirstResponder();
            });
            View.AddGestureRecognizer(txtnameGesture);
            #endregion

            #region Notes
            //Enter
            notes.ShouldReturn += (textField) =>
            {
                notes.ResignFirstResponder();
                return true;
            };
            //欄外タップ
            var txtNotesGesture = new UITapGestureRecognizer(() =>
            {
                this.notes.ResignFirstResponder();
            });
            View.AddGestureRecognizer(txtNotesGesture);
            #endregion
        }
    }
}