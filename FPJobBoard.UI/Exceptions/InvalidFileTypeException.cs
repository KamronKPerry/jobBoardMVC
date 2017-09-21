using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPJobBoard.UI.Exceptions
{
    public class InvalidFileTypeException : Exception
    {
        //3 ctors - or ctr with 3 overloads
        public InvalidFileTypeException() { }
        public InvalidFileTypeException(string message) : base(message) { }
        public InvalidFileTypeException(string message, Exception inner) : base(message, inner) { }

    }
}