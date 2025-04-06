using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Lab3.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Data;
using System.Windows.Input;
using Lab3.Exceptions;
using Lab3.Utils;

namespace Lab3.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PrivateFields
        private string? _firstName { get; set; }
        private string? _lastName { get; set; }
        private DateTime? _birthday { get; set; }
        private string? _email { get; set; }
        private Person? _person { get; set; }
        private string? _message { get; set; }
        private bool _isNotLoading { get; set; }
        #endregion
        #region PublicFields
        public string? FirstName
        {
            private get => _firstName;
            set
            {
                _firstName = value;
                UpdateCanExecute();
            } 
        }
        public string? LastName
        {
            private get => _lastName;
            set
            {
                _lastName = value;
                UpdateCanExecute();
            }
        }
        public DateTime? Birthday
        {
            private get => _birthday;
            set
            {
                _birthday = value;
                UpdateCanExecute();
            }
        }
        public string? Email
        {
            private get => _email;
            set
            {
                _email = value;
                UpdateCanExecute();
            }
        }
        public string? Message
        {
            get => _message;
            private set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public bool IsNotLoading
        {
            get => _isNotLoading;
            private set
            {
                _isNotLoading = value;
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }
        #endregion

        public AsyncRelayCommand ProceedCommand { get; }

        public MainViewModel()
        {
            ProceedCommand = new AsyncRelayCommand(Proceed, CanExecute);
            IsNotLoading = true;
        }

        private async Task Proceed()
        {
            IsNotLoading = false;
            Mouse.OverrideCursor = Cursors.Wait;
            
            try
            {
                Validators.ValidateEmail(Email);
                Validators.ValidateBirthday(Birthday);

                await Task.Delay(3000); // Simulate work

                await Task.Run(() =>
                    _person = new Person(FirstName!, LastName!, Email!, Birthday ?? DateTime.Today));
                await Task.Run(() =>
                    ConstructMessage());
            } 
            catch (UserNotBornYetException)
            {
                MessageBox.Show("You are too old! Pick another birth date plese.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = null;
            }
            catch (UserAlreadyDeadException)
            {
                MessageBox.Show("You are not alive yet! Pick another birth date plese.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = null;
            }
            catch (InvalidEmailException)
            {
                MessageBox.Show("Your email cannot be real. Please, change it!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = null;
            }
            finally
            {
                IsNotLoading = true;
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void ConstructMessage()
        {
            string name = $"You are {_person!.FirstName} {_person.LastName}, a ";
            string age = _person.IsAdult ? "grown-up " : "child ";
            string zodiac = $"{_person.SunSign}-{_person.ChineseSign}! So cool! ";
            string birthday = $"You were born on {_person.Birthday.Date:d} and today is ";
            string day = _person.IsBirthday ? "YOUR BIRTHDAY! CONGRATS! " : "a good day :) ";
            string mail = $"Your email is {_person.Email} by the way.";
            Message = name + age + zodiac + birthday + day + mail;
            OnPropertyChanged(nameof(Message));
        }

        private bool CanExecute()
        {
            return !String.IsNullOrWhiteSpace(FirstName) 
                && !String.IsNullOrWhiteSpace(LastName)
                && !String.IsNullOrWhiteSpace(Email)
                && Birthday != null; 
        }

        private void UpdateCanExecute()
        {
            ProceedCommand.NotifyCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
