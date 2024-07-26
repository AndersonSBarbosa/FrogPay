namespace FrogPay.Core.Exceptions
{
    public class DomainExceptions : Exception
    {
        internal List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;

        public DomainExceptions() { }

        public DomainExceptions(string message, List<string> errors) : base(message)
        {
            _errors = errors;
        }

        public DomainExceptions(string message) : base(message) { }

        public DomainExceptions(string message, Exception innerException) : base(message, innerException) { }
    }
}
