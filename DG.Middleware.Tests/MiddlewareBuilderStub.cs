namespace DG.Middleware.Tests
{
    class MiddlewareBuilderStub : MiddlewareBuilder<MiddlewareItemStub>
    {
        public int ArraySize => base._middlewares.Count;
    }
}
