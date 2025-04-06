using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class UserNotBornYetException : Exception
    {
        public UserNotBornYetException() { }

        public UserNotBornYetException(string message)
            : base(message) { }
    }
}
