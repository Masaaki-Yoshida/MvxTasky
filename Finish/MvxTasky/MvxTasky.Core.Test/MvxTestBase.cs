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
