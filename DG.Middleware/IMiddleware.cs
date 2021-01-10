using System.Threading.Tasks;

namespace DG.Middleware
{
    /// <summary>
    /// Определяет функцию, которую можно использовать в конвеере обработки объекта типа <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемого объекта.</typeparam>
    public interface IMiddleware<T>
    {
        /// <summary>
        /// Метод обработки объекта.
        /// </summary>
        /// <param name="item">Обрабатываемый объект.</param>
        /// <returns><see cref="Task"/>, поток, в котором выполняется обработка объекта.</returns>
        Task InvokeAsync(T item);
    }
}
