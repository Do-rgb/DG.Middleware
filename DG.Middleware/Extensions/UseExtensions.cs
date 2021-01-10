using System;
using System.Threading.Tasks;

namespace DG.Middleware
{
    public static class UseExtensions
    {
        /// <summary>
        /// Упрощает добавление промежуточного делегата в конвейер обработки.
        /// </summary>
        /// <typeparam name="T">Обрабатываемый тип.</typeparam>
        /// <param name="builder"><see cref="IMiddlewareBuilder{T}"/> объект.</param>
        /// <param name="middleware">Функция обрабатывающая запрос или вызывает следующую в конвейере обработки.</param>
        /// <returns><see cref="IMiddlewareBuilder{T}"/> объект.</returns>
        public static IMiddlewareBuilder<T> Use<T>(this IMiddlewareBuilder<T> builder, Func<T, Func<Task>, Task> middleware)
        {
            return builder.Use(next =>
            {
                return item => middleware(item, () => next(item));
            });
        }
    }
}
