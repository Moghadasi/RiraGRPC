using System.Diagnostics.CodeAnalysis;

namespace Rira.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class OutUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCode]
        [return: NotNull]
        public static TOutput Out<TOutput>(out TOutput variable, TOutput value)
        {
            variable = value!;
            return variable;
        }
    }
}
