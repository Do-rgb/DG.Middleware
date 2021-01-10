using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DG.Middleware
{
    /// <summary>
    /// <inheritdoc cref="IMiddlewareBuilder{T}"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="IMiddlewareBuilder{T}"/></typeparam>
    public class MiddlewareBuilder<T> : IMiddlewareBuilder<T>
    {
        /// <summary>
        /// Хранит промежуточные делегаты обработки объекта.
        /// </summary>
        protected readonly IList<Func<Func<T, Task>, Func<T, Task>>> _middlewares = new List<Func<Func<T, Task>, Func<T, Task>>>();

        public virtual Func<T, Task> Build()
        {
            var result = LastDelegateCreate();

            foreach (var middleware in _middlewares.Reverse())
            {
                result = middleware(result);
            }

            return result;
        }

        public virtual IMiddlewareBuilder<T> Use(Func<Func<T, Task>, Func<T, Task>> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        /// <summary>
        /// Создает делегат-заглушку, вызываемый в конце конвеера обработки.
        /// </summary>
        /// <returns>Делегат заглушка.</returns>
        protected virtual Func<T, Task> LastDelegateCreate()
        {
            return T => Task.CompletedTask;
        }
    }
}
