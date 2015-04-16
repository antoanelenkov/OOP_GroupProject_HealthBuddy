namespace HealthBuddy.Exceptions
{
    using System;

    class UnHealthyException : Exception
    {
        public UnHealthyException(string message)
            : base(message)
        {
        }
    }
}
