using Acr.UserDialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvxTasky.Core.Services.Todo;
using MvxTasky.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvxTasky.Core.Test
{
    [TestClass]
    public class TodoItemDetailViewModelTest : MvxTestBase
    {
        [TestMethod]
        public void TodoItemDetailViewModelCloseTest()
        {

            var dialog = new Mock<IUserDialogs>();
            var todoService = new Mock<ITodoItemService>();
            var vm = new TodoItemDetailViewModel(todoService.Object,dialog.Object);
            vm.CloseCommand.Execute();
            Assert.AreEqual(1, base.MockDispatcher.Hints.Count);
        }

        [TestMethod]
        public void DeleteOKTest()
        {
            var dialog = new Mock<IUserDialogs>();
            //ConfirmAsync()が呼ばれたとき、Trueを返す設定
            dialog.Setup(p => p.ConfirmAsync(It.IsAny<ConfirmConfig>(), null)).Returns(Task.FromResult(true));

            var items = new List<TodoItem>() { new TodoItem { ID = 1, Name = "MockObject", Notes = "", Done = false } };

            var todoService = new Mock<ITodoItemService>();
            todoService.Setup(p => p.GetTask(items[0].ID)).Returns(items[0]);
            var vm = new TodoItemDetailViewModel(todoService.Object, dialog.Object);
            vm.Init(items[0]);

            vm.DeleteCommand.Execute();
                        
            Assert.AreEqual(1, base.MockDispatcher.Hints.Count);
        }


        [TestMethod]
        public void DeleteCancelTest()
        {
            var dialog = new Mock<IUserDialogs>();
            //ConfirmAsync()が呼ばれたとき、Falseを返す設定
            dialog.Setup(p => p.ConfirmAsync(It.IsAny<ConfirmConfig>(), null)).Returns(Task.FromResult(false));

            var items = new List<TodoItem>() { new TodoItem { ID = 1, Name = "MockObject", Notes = "", Done = false } };

            var todoService = new Mock<ITodoItemService>();
            todoService.Setup(p => p.GetTask(items[0].ID)).Returns(items[0]);
            var vm = new TodoItemDetailViewModel(todoService.Object, dialog.Object);
            vm.Init(items[0]);

            vm.DeleteCommand.Execute();

            Assert.AreEqual(0, base.MockDispatcher.Hints.Count);
        }
    }
}
