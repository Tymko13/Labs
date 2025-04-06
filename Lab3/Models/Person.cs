using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Lab3.Exceptions;
using Lab3.Utils;

namespace Lab3.Models
{
    class Person
    {
        #region PrivateFields
        private string _email { get; set; }
        private DateTime _birthday { get; set; }
        #endregion
        #region ReadonlyFields
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public string Email
        {
            get => _email;
            private set
            {
                Validators.ValidateEmail(value);
                _email = value;
            }
        }
        public DateTime Birthday
        {
            get => _birthday;
            private set
            {
                Validators.ValidateBirthday(value);
                _birthday = value;
                CalcBirtdayDependencies();
            }
        }
        public bool IsAdult { get; private set; }
        public string SunSign { get; private set; }
        public string ChineseSign { get; private set; }
        public bool IsBirthday { get; private set; }
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

        private void CalcBirtdayDependencies()
        {
            CalcIsAdult();
            CalcSunSign();
            CalcChineseSign();
            CalcIsBirthday();
        }

        private void CalcIsAdult()
        {
            int age = DateTime.Today.Year - _birthday.Year;
            if (DateTime.Today.Month < _birthday.Month) --age;
            else if (DateTime.Today.Month == _birthday.Month
                && DateTime.Today.Day < _birthday.Day) --age;
            IsAdult = age >= 18;
        }

        private void CalcChineseSign()
        {
            ChineseSign = (_birthday.Year % 12) switch
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
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private void CalcSunSign()
        {
            int day = _birthday.Day;
            SunSign = _birthday.Month switch
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
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
        private void CalcIsBirthday()
        {
            IsBirthday = DateTime.Today.Month == _birthday.Month
                && DateTime.Today.Day == _birthday.Day;
        }
    }
}
