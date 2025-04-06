using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lab3.Exceptions;

namespace Lab3.Utils
{
    internal static class Validators
    {
        public static void ValidateEmail(string? email)
        {
            if(email == null) throw new ArgumentNullException();
            Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]", RegexOptions.Compiled);
            if (!EmailRegex.IsMatch(email))
                throw new InvalidEmailException();
        }

        public static void ValidateBirthday(DateTime? date)
        {
            if(date == null) throw new ArgumentNullException();
            if (date.Value.Date > DateTime.Today)
                throw new UserNotBornYetException();
            if (date.Value.Date < DateTime.Today.AddYears(-135))
                throw new UserAlreadyDeadException();
        }
    }
}
