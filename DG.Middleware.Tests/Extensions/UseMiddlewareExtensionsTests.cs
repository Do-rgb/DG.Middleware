using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DG.Middleware.Tests
{
    class UseMiddlewareExtensionsTests
    {
        [Test]
        public void UseMiddleware_WithNoParameters_NotThrowsException()
        {
            var item = new MiddlewareItemStub();
            var builder = new MiddlewareBuilderStub();
            //Assert
            Assert.DoesNotThrow(() =>
            {
                builder.UseMiddleware<MiddlewareNoArgumentsStub, MiddlewareItemStub>();
                builder.Build();
            });
        }

        [Test]
        public void UseMiddleware_WithParameter_NotThrowsException()
        {
            var item = new MiddlewareItemStub();
            var builder = new MiddlewareBuilderStub();
            object[] args = { 5 };
            //Assert
            Assert.DoesNotThrow(() =>
            {
                builder.UseMiddleware<MiddlewareWithArgumentsStub, MiddlewareItemStub>(args);
                builder.Build();
            });
        }

        [Test]
        public void UseMiddleware_WithWrongOrderArguments_ThrowsException()
        {
            var item = new MiddlewareItemStub();
            var builder = new MiddlewareBuilderStub();
            object[] args = { 5 };

            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.UseMiddleware<MiddlewareWithWrongOrderOfArgumentsStub, MiddlewareItemStub>(args);
                builder.Build();
            });
        }

        private class MiddlewareNoArgumentsStub : Middleware<MiddlewareItemStub>, IMiddleware<MiddlewareItemStub>
        {
            public MiddlewareNoArgumentsStub(Func<MiddlewareItemStub, Task> next) : base(next) { }

            public override Task InvokeAsync(MiddlewareItemStub obj) => Task.CompletedTask;
        }

        private class MiddlewareWithArgumentsStub : Middleware<MiddlewareItemStub>, IMiddleware<MiddlewareItemStub>
        {
            private readonly int _value = 0;
            public MiddlewareWithArgumentsStub(Func<MiddlewareItemStub, Task> next, int value) : base(next)
            {
                _value = value;
            }

            public override Task InvokeAsync(MiddlewareItemStub obj)
            {
                obj.SomeValue = _value;
                return _next.Invoke(obj);
            }
        }

        private class MiddlewareWithWrongOrderOfArgumentsStub : Middleware<MiddlewareItemStub>, IMiddleware<MiddlewareItemStub>
        {
            private readonly int _value = 0;
            public MiddlewareWithWrongOrderOfArgumentsStub(int value, Func<MiddlewareItemStub, Task> next) : base(next)
            {
                _value = value;
            }

            public override Task InvokeAsync(MiddlewareItemStub obj)
            {
                obj.SomeValue = _value;
                return _next.Invoke(obj);
            }
        }
    }
}
