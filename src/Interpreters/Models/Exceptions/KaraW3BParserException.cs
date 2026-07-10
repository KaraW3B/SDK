using KaraW3B.SDK.Exceptions;

namespace KaraW3B.SDK.Interpreters.Models.Exceptions
{
    public sealed class KaraW3BParserException : KaraW3BException
    {
        public KaraW3BParserException(string message) : base(message)
        {
        }
    }
}
