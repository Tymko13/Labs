using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class UserAlreadyDeadException : Exception
    {
        public UserAlreadyDeadException() { }

        public UserAlreadyDeadException(string message)
            : base(message) { }
    }
}
