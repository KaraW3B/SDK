using KaraW3B.SDK.Exceptions;

namespace KaraW3B.SDK.Interpreters.Models.Exceptions
{
    public sealed class KaraW3BWriterException : KaraW3BException
    {
        public KaraW3BWriterException(string message) : base(message)
        {
        }
    }
}
