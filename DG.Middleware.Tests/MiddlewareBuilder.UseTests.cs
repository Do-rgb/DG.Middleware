using NUnit.Framework;

namespace DG.Middleware.Tests
{
    public partial class MiddlewareBuilderTests
    {
        [Test]
        public void Use_OneCorrectFunc_FuncSizeEqualOne()
        {
            var builder = new MiddlewareBuilderStub();
            var expectedSize = 1;
            //Act
            builder.Use(next =>
            {
                return next;
            });
            //Assert
            Assert.AreEqual(expectedSize, builder.ArraySize);
        }

        [Test]
        public void Use_NoInput_FuncSizeEqualZero()
        {
            var builder = new MiddlewareBuilderStub();
            var expectedSize = 0;
            //Act
            //Assert
            Assert.AreEqual(expectedSize, builder.ArraySize);
        }
    }
}