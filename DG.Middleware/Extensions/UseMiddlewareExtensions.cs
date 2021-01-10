using System;
using System.Linq;
using System.Threading.Tasks;

namespace DG.Middleware
{
    public static class UseMiddlewareExtensions
    {
        const string InvokeAsyncMethodName = nameof(IMiddleware<object>.InvokeAsync);

        /// <summary>
        /// Добавляет промежуточный класс обработки в конвейер обработки.
        /// </summary>
        /// <typeparam name="TMiddleware">Промежуточный класс обработки.</typeparam>
        /// <typeparam name="T">Тип обрабатываемого объекта.</typeparam>
        /// <param name="builder"><see cref="IMiddlewareBuilder{T}"/> объект.</param>
        /// <param name="args">Аргументы передаваемые в конструктор промежуточного класса обработки.</param>
        /// <returns><see cref="IMiddlewareBuilder{T}"/> объект.</returns>
        public static IMiddlewareBuilder<T> UseMiddleware<TMiddleware, T>(this IMiddlewareBuilder<T> builder, params object[] args) where TMiddleware : IMiddleware<T>
        {
            return builder.Use(next =>
            {
                var middleware = typeof(TMiddleware);

                //Добавляется 1, так как конструктор типа наследуемого от IMiddleware<T>
                //первым аргументом должен принимать ссылку на следующий делегат
                var argsLength = args.Length + 1;

                if (!middleware
                    .GetConstructors()
                    .Any(c => c.GetParameters().Length != 0
                        && c.GetParameters()[0].ParameterType == typeof(Func<T, Task>)
                        && c.GetParameters().Length.Equals(argsLength)))
                {
                    throw new InvalidOperationException($"The class [{middleware.Name}] does not have a suitable constructor");
                }

                var methodInfo = middleware.GetMethod(InvokeAsyncMethodName);

                var ctorArgs = new object[argsLength];
                ctorArgs[0] = next;
                Array.Copy(args, 0, ctorArgs, 1, args.Length);

                var instance = Activator.CreateInstance(middleware, ctorArgs);

                return (Func<T, Task>)methodInfo.CreateDelegate(typeof(Func<T, Task>), instance);
            });
        }

    }
}
