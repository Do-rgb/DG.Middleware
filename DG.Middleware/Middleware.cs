using System;
using System.Threading.Tasks;

namespace DG.Middleware
{
    /// <summary>
    /// Базовый класс используемый для реализации промежуточной функции обработки объекта.
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемого объекта.</typeparam>
    public abstract class Middleware<T> : IMiddleware<T>
    {
        /// <summary>
        /// Следующий делегат в конвейере обработки объекта.
        /// </summary>
        protected readonly Func<T, Task> _next;

        public Middleware(Func<T, Task> next)
        {
            _next = next;
        }

        /// <summary>
        /// <inheritdoc cref="IMiddleware{T}.InvokeAsync(T)"/>
        /// </summary>
        /// <param name="item"><inheritdoc cref="IMiddleware{T}.InvokeAsync(T)"/></param>
        /// <returns><inheritdoc cref="IMiddleware{T}.InvokeAsync(T)"/></returns>
        public abstract Task InvokeAsync(T item);
    }
}
