using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException() { }
        public InvalidEmailException(string message)
            : base(message) { }
    }
}
