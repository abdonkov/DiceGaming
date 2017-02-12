using System;

namespace DiceGaming.Core.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base("Conflict!") { }
        public ConflictException(string message) : base(message) { }
    }
}