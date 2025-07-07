namespace Rira.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; private set; }


        /// <inheritdoc />
        public DomainException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
