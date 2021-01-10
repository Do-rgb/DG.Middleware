using NUnit.Framework;

namespace DG.Middleware.Tests
{
    public partial class MiddlewareBuilderTests
    {
        [Test]
        public void Build_RunWithoutInput_NotThrow()
        {
            var item = new MiddlewareItemStub();
            var build = new MiddlewareBuilderStub();
            var func = build.Build();
            //Assert
            Assert.DoesNotThrow(() => { func(item); });
        }

        [Test]
        public void Build_RunWithOneFunc_FuncWillExecute()
        {
            var item = new MiddlewareItemStub();
            var build = new MiddlewareBuilderStub();
            var expectedValue = 5;

            build.Use(async (someItem, next) =>
            {
                someItem.SomeValue = 5;
                await next.Invoke();
            });

            var func = build.Build();
            //Assert
            Assert.DoesNotThrow(() => { func(item); });
            Assert.AreEqual(expectedValue, item.SomeValue);
        }

        [Test]
        public void Build_RunWithMultiFunc_FuncWillExecute()
        {
            var item = new MiddlewareItemStub();
            var build = new MiddlewareBuilderStub();
            var exNumber = 5;
            var exStr = "str";

            build.Use(async (someItem, next) =>
            {
                someItem.SomeValue = 5;
                await next.Invoke();
            });

            build.Use(async (someItem, next) =>
            {
                someItem.SomeStr = "str";
                await next.Invoke();
            });

            var func = build.Build();
            //Assert
            Assert.DoesNotThrow(() => { func(item); });
            Assert.AreEqual(exNumber, item.SomeValue);
            Assert.AreEqual(exStr, item.SomeStr);
        }
    }
}
