using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab2.Models
{
    class Person
    {
        #region PrivateFields
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _email { get; set; }
        private DateTime _birthday { get; set; }
        private bool _isAdult { get; set; }
        private string _sunSign { get; set; }
        private string _chineseSign { get; set; }
        private bool _isBirthday { get; set; }
        #endregion
        #region PublicFields
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; }
        }
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; }
        }
        public string Email
        {
            get => _email;
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                ValidateBirthday(value);
                _birthday = value;
                CalcBirtdayDependencies();
            }
        }
        #endregion
        #region ReadonlyFields
        public bool IsAdult { get => _isAdult; }
        public string SunSign { get => _sunSign; }
        public string ChineseSign { get => _chineseSign; }
        public bool IsBirthday { get => _isBirthday; }
        #endregion

        public Person(string FirstName, string LastName, string Email, DateTime Birthday)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Birthday = Birthday;

        }
        public Person(string FirstName, string LastName, string Email) : this(FirstName, LastName, Email, DateTime.Today) {}
        public Person(string FirstName, string LastName, DateTime Birthday) : this(FirstName, LastName, "default@email", Birthday) {}

        private static void ValidateEmail(string email)
        {
            Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]", RegexOptions.Compiled);
            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException();
        }

        private static void ValidateBirthday(DateTime date)
        {
            if (date.Date > DateTime.Today || date.Date < DateTime.Today.AddYears(-135))
                throw new ArgumentOutOfRangeException();
        }

        private async Task CalcBirtdayDependencies()
        {
            await Task.Run(() => CalcIsAdult());
            await Task.Run(() => CalcSunSign());
            await Task.Run(() => CalcChineseSign());
            await Task.Run(() => CalcIsBirthday());
        }

        private void CalcIsAdult()
        {
            int age = DateTime.Today.Year - _birthday.Year;
            if (DateTime.Today.Month < _birthday.Month) --age;
            else if (DateTime.Today.Month == _birthday.Month
                && DateTime.Today.Day < _birthday.Day) --age;
            _isAdult = age >= 18;
        }

        private void CalcChineseSign()
        {
            _chineseSign = (_birthday.Year % 12) switch
            {
                0 => "Monkey",
                1 => "Rooster",
                2 => "Dog",
                3 => "Pig",
                4 => "Rat",
                5 => "Ox",
                6 => "Tiger",
                7 => "Rabbit",
                8 => "Dragon",
                9 => "Snake",
                10 => "Horse",
                11 => "Goat",
                _ => "Unknown",
            };
        }

        private void CalcSunSign()
        {
            int day = _birthday.Day;
            _sunSign = _birthday.Month switch
            {
                1 => (day <= 19) ? "Capricorn" : "Aquarius",
                2 => (day <= 18) ? "Aquarius" : "Pisces",
                3 => (day <= 20) ? "Pisces" : "Aries",
                4 => (day <= 19) ? "Aries" : "Taurus",
                5 => (day <= 20) ? "Taurus" : "Gemini",
                6 => (day <= 20) ? "Gemini" : "Cancer",
                7 => (day <= 22) ? "Cancer" : "Leo",
                8 => (day <= 22) ? "Leo" : "Virgo",
                9 => (day <= 22) ? "Virgo" : "Libra",
                10 => (day <= 22) ? "Libra" : "Scorpio",
                11 => (day <= 21) ? "Scorpio" : "Sagittarius",
                12 => (day <= 21) ? "Sagittarius" : "Capricorn",
                _ => "Unknown"
            };
        }
        private void CalcIsBirthday()
        {
            _isBirthday = DateTime.Today.Month == _birthday.Month
                && DateTime.Today.Day == _birthday.Day;
        }
    }
}
