using System;

namespace ModrinthSharp
{
    public class ModrinthApiException : Exception
    {
        public ModrinthApiException(string message) : base(message)
        {
            
        }
    }
}