﻿using System;

namespace ModrinthSharp
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
            
        }
    }
}