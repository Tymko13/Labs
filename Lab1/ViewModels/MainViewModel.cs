using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Lab1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime? _birthday;
        private int? _age;
        private string? _message;

        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _age = DateTime.Today.Year - value?.Year;
                    if (DateTime.Today.Month - value?.Month < 0) --_age;
                    else if (DateTime.Today.Month - value?.Month == 0
                        && DateTime.Today.Day - value?.Day < 0) --_age;

                    if (_age < 0 || _age > 135) resetBirthday();
                    else
                    {
                        if (DateTime.Now.Month == value?.Month && DateTime.Now.Day == value?.Day)
                        {
                            MessageBox.Show("Happy birthday!", "Hooray", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        _birthday = value;
                        constructMessage();
                    }
                }
            }
        }

        private void resetBirthday()
        {
            _age = null;
            _message = null;
            _birthday = null;
            MessageBox.Show("You are not alive! Pick another birth date plese.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Message));
        }

        public string? Message
        {
            get => _message;
        }

        private void constructMessage()
        {
            int day = _birthday?.Day ?? 0;
            int month = _birthday?.Month ?? 0;
            int year = _birthday?.Year ?? 0;
            string zodiac = month switch
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
            string[] chineseSigns = {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};
            string chineseZodiac = chineseSigns[year % 12];
            _message = $"You are a {_age} year old {zodiac}-{chineseZodiac}! So cool!";
            OnPropertyChanged(nameof(Message));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
