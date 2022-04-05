using PocMaui.Models.Entities;
using PocMaui.Services.Interfaces;
using PocMaui.ViewModels.Base;
using PocMaui.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.ViewModels
{
    public class SutomViewModel : BaseViewModel
    {
        private readonly IWordService _wordService;
        public SutomViewModel(INavigation navigation):base(navigation)
        {
            UserValidWordCommand = new Command(async () => await OnUserValidWordCommand());

            _wordService = Services.ServiceProvider.GetService<IWordService>();

            Words = new ObservableCollection<IEnumerable<SutomWordEntityWrapper>>();
        }

        public async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            CorrectWord = await _wordService.GetRandomWord();
        }

        public Command UserValidWordCommand { get; set; }
        private async Task OnUserValidWordCommand()
        {
            if (CorrectWord.ToUpper() == UserWord)
            {
                await App.Current.MainPage.DisplayAlert("Good game", $"You win this game {Words.Count()}/6", "Ok");
                return;
            }
            else if(Words.Count() >= 6)
            {
                await App.Current.MainPage.DisplayAlert("...", $"You loose this game", "Ok");
                return;
            }


            if (CorrectWord.Length != _userWord.Length)
                return;

            var word = new List<SutomWordEntityWrapper>();


            for (int i = 0; i < CorrectWord.Length; i++)
            {

                if (_userWord[i] == CorrectWord.ToUpper()[i])
                    word.Add(new SutomWordEntityWrapper() { Status = 2, Value = _userWord[i] });
                else if (CorrectWord.ToUpper().Contains(_userWord[i]))
                    word.Add(new SutomWordEntityWrapper() { Status = 1, Value = _userWord[i] });
                else
                    word.Add(new SutomWordEntityWrapper() { Status = 0, Value = _userWord[i] });
            }

            Words.Add(word);

            UserWord = "";
        }

        #region Props

        private string _correctWord;
        public string CorrectWord
        {
            get => _correctWord;
            set
            {
                _correctWord = value.ToUpper();
                NotifyPropertyChanged(nameof(CorrectWord));
            }
        }

        #region UserWord
        private string _userWord;
        public string UserWord
        {
            get => _userWord;
            set
            {
                _userWord = value.ToUpper();
                NotifyPropertyChanged(nameof(UserWord));
            }
        }
        #endregion

        #region Words
        private ObservableCollection<IEnumerable<SutomWordEntityWrapper>> _words;
        public ObservableCollection<IEnumerable<SutomWordEntityWrapper>> Words
        {
            get => _words;
            set
            {
                _words = value;
                NotifyPropertyChanged(nameof(Words));
            }
        }
        #endregion

        #endregion
    }
}
