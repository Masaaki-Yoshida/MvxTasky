Webサイトまたはmarkdownプレビューとして表示している場合は、それぞれの枠内をコピー＆ペーストしてください。　　
メモ帳などのテキストエディタで開いている場合は、cs と の間の行をコピー＆ペーストしてください。　　

順次コードを張り付けていく部分もスニペットを用意していますが、その後の完成形も用意してありますので、適宜ご利用ください。　　

### App.cs

```cs
using Acr.UserDialogs;

Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
```

### TodoItemDetailViewModel.cs

```cs
#region メンバー変数
private IUserDialogs _dialog;
#endregion
```

```cs
public TodoItemDetailViewModel(ITodoItemService todoItemService,IUserDialogs dialog)
{
    _service = todoItemService;
    _dialog = dialog;
}
```

```cs
private async void ExecuteDelete()
{
    var config = new ConfirmConfig();
    config.Title = "確認";
    config.Message = "削除してよろしいでしょうか。";
    var result = await _dialog.ConfirmAsync(config,null);
    if (result) {
        _service.DeleteTask(ID);
        Close(this);
    }
}
```

### SqlitePluginBootstrap.cs

```cs
using MvvmCross.Platform.Plugins;

namespace MvxTasky.UWP.BootStrap
{
    public class SqlitePluginBootstrap 
        : MvxLoaderPluginBootstrapAction<MvvmCross.Plugins.Sqlite.PluginLoader, MvvmCross.Plugins.Sqlite.WindowsUWP.Plugin>
    { }
}
```

### TodoItemServiceSQLite.cs
```cs
using MvvmCross.Plugins.Sqlite;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace MvxTasky.Core.Services.Todo
{
    public class TodoItemServiceSQLite
    {
        private readonly IMvxSqliteConnectionFactory _sqliteConnectionFactory;
        private readonly SQLiteConnection _con;

        public TodoItemServiceSQLite(IMvxSqliteConnectionFactory factory)
        {
            _sqliteConnectionFactory = factory;
            _con = factory.GetConnection("todoitem.sqlite");
            _con.CreateTable<TodoItem>();
        }
```

### TodoItem.cs
```cs
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
```

### TodoItemServiceSQLite.cs
```cs
public class TodoItemServiceSQLite : ITodoItemService
```


```cs
using MvvmCross.Plugins.Sqlite;
using SQLite;
using System.Collections.Generic;
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

        public List<TodoItem> GetTasks()
        {
            return _con.Table<TodoItem>().Where(p => p.ID != -1).ToList();
        }

        public bool UpdateTask(TodoItem item)
        {
            return _con.Update(item) > 0 ? true : false;
        }
    }
}
```

### App.cs
```cs
Mvx.ConstructAndRegisterSingleton<ITodoItemService, TodoItemServiceOnMemory>();
↓
Mvx.ConstructAndRegisterSingleton<ITodoItemService, TodoItemServiceSQLite>();
```

### MockDispatcher.cs

```cs
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using System;
using System.Collections.Generic;

namespace MvxTasky.Core.Test
{
    public class MockDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return true;
        }

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return true;
        }
    }
}
```

### MvxTestBase.cs

```cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;

namespace MvxTasky.Core.Test
{
    public class MvxTestBase : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher { get; private set; }

        [TestInitialize]
        public void SetUp()
        {
            base.Setup();
        }

        protected override void AdditionalSetup()
        {
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
        }
    }
}
```

### TodoItemListViewModelTest.cs


```cs
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
```

### TodoItemDetailViewModelTest
```cs
[TestMethod]
public void TodoItemDetailViewModelCloseTest()
{

    var dialog = new Mock<IUserDialogs>();
    var todoService = new Mock<ITodoItemService>();
    var vm = new TodoItemDetailViewModel(todoService.Object,dialog.Object);
    vm.CloseCommand.Execute();
    Assert.AreEqual(1, base.MockDispatcher.Hints.Count);
}
```

```cs
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
```

```cs
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
```
