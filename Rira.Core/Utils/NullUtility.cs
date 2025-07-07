using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Rira.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class NullUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="value"></param>
        /// <param name="onUsable"></param>
        /// <param name="onNull"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        public static TOutput? Usable<TOutput, TInput>(this TInput? value, Func<TInput, TOutput> onUsable, Func<TOutput?>? onNull = null) where TOutput : struct
        {
            return value == null ? onNull?.Invoke() : onUsable(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        public static bool HasValue([NotNullWhen(true)] this object? value)
        {
            return value != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        public static T EnsureUsable<T>([NotNull] this T? value, string? description = null, [CallerArgumentExpression("value")] string expression = "") where T : notnull
        {
            return value ?? throw new Exception($"Expression `{expression}` is null. {description}");
        }

        /// <summary>
        /// مطمئن می‌شود متغیر نول‌پذیر، مقدار داشته باشد و مقدار را برمی‌گرداند
        /// اگر متغیر نول باشد خطا صادر می‌کند
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        public static T EnsureValuable<T>(this T? value, string? description = null, [CallerArgumentExpression("value")] string expression = "")
            where T : struct
        {
            return value == null ? throw new Exception($"Expression `{expression}` is null. {description}") : value.Value;
        }

        /// <summary>
        /// مطمئن می‌شود متغیر نول‌پذیر، مقدار داشته باشد و مقدار را برمی‌گرداند
        /// اگر متغیر نول باشد خطا صادر می‌کند
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        public static T Or<T>(this T? value, T defaultValue, [CallerArgumentExpression("value")] string expression = "")
            where T : notnull
        {
            return value ?? defaultValue;
        }
    }
}
