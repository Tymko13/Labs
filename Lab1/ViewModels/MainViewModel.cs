using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime? _birthday;
        private string? _message;

        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    constructMessage();
                    OnPropertyChanged(nameof(Birthday));
                }
            }
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
            _message = $"You are a {DateTime.Now.Year - year} year old {zodiac}-{chineseZodiac}! So cool!";
            OnPropertyChanged(nameof(Message));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
