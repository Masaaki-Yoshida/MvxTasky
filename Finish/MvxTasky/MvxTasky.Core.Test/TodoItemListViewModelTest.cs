using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvxTasky.Core.Services.Todo;
using MvxTasky.Core.ViewModels;
using System.Collections.Generic;

namespace MvxTasky.Core.Test
{
    [TestClass]
    public class TodoItemListViewModelTest : MvxTestBase
    {
        [TestMethod]
        public void TodoItemListViewModelNavigationTest()
        {
            var todoService = new Mock<ITodoItemService>();
            var vm = new TodoItemListViewModel(todoService.Object);
            vm.AddCommand.Execute();
            Assert.AreEqual(1,base.MockDispatcher.Requests.Count);
            Assert.AreEqual(typeof(TodoItemDetailViewModel), base.MockDispatcher.Requests[0].ViewModelType);
        }

        [TestMethod]
        public void AddComandTest()
        {
            //Mockで差し替えるTodoItemList
            var items = new List<TodoItem>(){ new TodoItem { ID = 1, Name = "MockObject", Notes = "Use for GetTasks Return" , Done = false } };

            //ITodoItemServiceのMockを作成
            var todoService = new Mock<ITodoItemService>();
            //GetTasks()が呼ばれたとき、itemsを返す設定
            todoService.Setup(p => p.GetTasks()).Returns(items);

            var vm = new TodoItemListViewModel(todoService.Object);
            //vmのライフサイクルメソッド呼び出し
            vm.Start();
            vm.AddCommand.Execute();

            //画面遷移リクエスト数が1件であること
            Assert.AreEqual(1, base.MockDispatcher.Requests.Count);

            //画面遷移リクエストパラメータが正しい事
            Assert.AreEqual((items[0].ID + 1).ToString(), base.MockDispatcher.Requests[0].ParameterValues["ID"]);
            Assert.IsNull(base.MockDispatcher.Requests[0].ParameterValues["Name"]);
            Assert.IsNull(base.MockDispatcher.Requests[0].ParameterValues["Notes"]);
            Assert.AreEqual(false.ToString(), base.MockDispatcher.Requests[0].ParameterValues["Done"]);
        }

        [TestMethod]
        public void EditComandTest()
        {
            //Mockで差し替えるTodoItemList
            var items = new List<TodoItem>() { new TodoItem { ID = 5, Name = "MockObject", Notes = "Use for GetTasks Return", Done = false } };

            //ITodoItemServiceのMockを作成
            var todoService = new Mock<ITodoItemService>();
            //GetTasks()が呼ばれたとき、itemsを返す設定
            todoService.Setup(p => p.GetTasks()).Returns(items);

            var vm = new TodoItemListViewModel(todoService.Object);
            //vmのライフサイクルメソッド呼び出し
            vm.Start();
            vm.EditCommand.Execute(items[0]);

            //画面遷移リクエスト数が1件であること
            Assert.AreEqual(1, base.MockDispatcher.Requests.Count);

            //画面遷移リクエストパラメータが正しい事
            Assert.AreEqual(items[0].ID.ToString(), base.MockDispatcher.Requests[0].ParameterValues["ID"]);
            Assert.AreEqual(items[0].Name, base.MockDispatcher.Requests[0].ParameterValues["Name"]);
            Assert.AreEqual(items[0].Notes, base.MockDispatcher.Requests[0].ParameterValues["Notes"]);
            Assert.AreEqual(items[0].Done.ToString(), base.MockDispatcher.Requests[0].ParameterValues["Done"]);
        }
    }
}
