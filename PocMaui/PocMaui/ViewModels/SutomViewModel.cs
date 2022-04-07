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
        private int _numberOfTry = 6;
        private bool _isGameFinished;

        public SutomViewModel(INavigation navigation):base(navigation)
        {
            KeyboardLetterPressedCommand = new Command<string>(async (string letter) => await OnKeyboardLetterPressedCommand(letter));

            _wordService = Services.ServiceProvider.GetService<IWordService>();

            Words = new ObservableCollection<ObservableCollection<SutomWordEntityWrapper>>();
        }

        public async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            // Generate the word
            var correctWord = await _wordService.GetRandomWord(); 
            CorrectWord = _wordService.NormalizeString(correctWord);
            CorrectWord = "Pompier";

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

            Words.Add(new ObservableCollection<SutomWordEntityWrapper>(firstWord));
        }

        public Command<string> KeyboardLetterPressedCommand { get; set; }
        private async Task OnKeyboardLetterPressedCommand(string letter)
        {
            if (_isGameFinished)
                return;

            if (letter == "Delete")
            {
                var isFirstLetter = Words.LastOrDefault().Skip(1).FirstOrDefault(l => l.Value.ToString() != "\0") == null;

                if (isFirstLetter)
                    return;

                Words.LastOrDefault().LastOrDefault(l => l.Value.ToString() != "\0").Value = "\0".ToCharArray()[0];
                return;
            }

            var lastWhiteLetter = Words.LastOrDefault().FirstOrDefault(l => l.Value.ToString() == "\0");
            if (lastWhiteLetter != null)
                lastWhiteLetter.Value = letter.ToCharArray()[0];

            // If it's the last letter of the word
            if (Words.LastOrDefault().FirstOrDefault(l => l.Value.ToString() == "\0") == null)
            {
                // Check response
                CheckLastWord();

                _isGameFinished = await CheckEndGame();

                if (!_isGameFinished)
                {
                    // Add new line
                    AddNewLine();
                }
            }
        }

        private async Task<bool> CheckEndGame()
        {
            // 1 Check if the user win
            if (!Words.LastOrDefault().Any(l => l.Status != 2))
            {
                await App.Current.MainPage.DisplayAlert("GG", $"You win this game with the score : {Words.Count()}/{_numberOfTry}", "Ok");
                return true;
            }
            
            // 2 Check if user can't retry
            if (Words.Count() >= _numberOfTry)
            {
                await App.Current.MainPage.DisplayAlert("...", $"You lose this game : {Words.Count()}/{_numberOfTry}", "Ok");
                return true;
            }

            return false;
        }

        private void CheckLastWord()
        {
            var userWord = Words.LastOrDefault();

            for (int i = 0; i < userWord.Count(); i++)
            {
                // If it's the correct answer
                if (CorrectWord[i] == userWord[i].Value)
                    userWord[i].Status = 2;
                else if (CorrectWord.Contains(userWord[i].Value) && CorrectWord.Skip(1).Where(l => l == userWord[i].Value).Count() >= userWord.Skip(1).Where(l => l.Value == userWord[i].Value).Count())
                    userWord[i].Status = 1;
            }
        }

        private void AddNewLine()
        {
            var newWord = Words.LastOrDefault().Select((letter, i) =>
            {
                if (i >= 1)
                {
                    return new SutomWordEntityWrapper("\0".ToCharArray()[0]);
                }
                else
                {
                    return new SutomWordEntityWrapper(letter.Value) { Status = 2 };
                }
            });

            Words.Add(new ObservableCollection<SutomWordEntityWrapper>(newWord));
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

        #region Words
        private ObservableCollection<ObservableCollection<SutomWordEntityWrapper>> _words;
        public ObservableCollection<ObservableCollection<SutomWordEntityWrapper>> Words
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
