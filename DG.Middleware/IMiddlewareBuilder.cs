using System;
using System.Threading.Tasks;

namespace DG.Middleware
{
    /// <summary>
    /// Определяет класс, который предоставляет механизм для настройки конвейера обработки объекта типа <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемого объекта.</typeparam>
    public interface IMiddlewareBuilder<T>
    {
        /// <summary>
        /// Создает делегат, используемый для обработки объекта.
        /// </summary>
        /// <returns>Делегат обработки объекта.</returns>
        Func<T, Task> Build();

        /// <summary>
        /// Добавляет промежуточный делегат в конвейер обработки объекта.
        /// </summary>
        /// <param name="middleware">Промежуточный делегат.</param>
        /// <returns><see cref="IMiddlewareBuilder{T}"/></returns>
        IMiddlewareBuilder<T> Use(Func<Func<T, Task>, Func<T, Task>> middleware);
    }
}
