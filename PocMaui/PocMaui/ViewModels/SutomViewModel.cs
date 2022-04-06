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
            // Generate the word
            var correctWord = await _wordService.GetRandomWord(); 
            CorrectWord = _wordService.NormalizeString(correctWord);

            var firstWord = CorrectWord.Select((c, i) => 
            {
                var sutomWordEntityWrapper = new SutomWordEntityWrapper();
                if (i == 0)
                {
                    sutomWordEntityWrapper.Value = c;
                    sutomWordEntityWrapper.Status = 2;
                }

                return sutomWordEntityWrapper; 
            });

            Words.Add(firstWord);
        }

        public Command UserValidWordCommand { get; set; }
        private async Task OnUserValidWordCommand()
        {

            if(Words.Count() >= 6)
            {
                await App.Current.MainPage.DisplayAlert("...", $"You loose this game", "Ok");
                return;
            }

            if (CorrectWord.Length != _userWord.Length)
                return;

            var word = new List<SutomWordEntityWrapper>();


            for (int i = 0; i < CorrectWord.Length; i++)
            {

                if (_userWord[i] == CorrectWord[i])
                    word.Add(new SutomWordEntityWrapper() { Status = 2, Value = _userWord[i] });
                else if (CorrectWord.Contains(_userWord[i]))
                    word.Add(new SutomWordEntityWrapper() { Status = 1, Value = _userWord[i] });
                else
                    word.Add(new SutomWordEntityWrapper() { Status = 0, Value = _userWord[i] });
            }

            Words.Add(word);

            if (CorrectWord == UserWord)
            {
                await App.Current.MainPage.DisplayAlert("Good game", $"You win this game {Words.Count()}/6", "Ok");
                return;
            }

            UserWord = "";
        }

        #region Props

        #region CorrectWord
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
        #endregion

        #region UserWord
        private string _userWord;
        public string UserWord
        {
            get => _userWord;
            set
            {
                _userWord = value;
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
