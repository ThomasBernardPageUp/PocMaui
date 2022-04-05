using PocMaui.Models.Entities;
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
        public SutomViewModel(INavigation navigation):base(navigation)
        {
            UserValidWordCommand = new Command(async () => await OnUserValidWordCommand());

            Words = new ObservableCollection<IEnumerable<SutomWordEntityWrapper>>();
        }

        public Command UserValidWordCommand { get; set; }
        private async Task OnUserValidWordCommand()
        {
            if (CorrectWord.Length != _userWord.Length)
                return;

            var word = new List<SutomWordEntityWrapper>();


            for (int i = 0; i < CorrectWord.Length; i++)
            {

                if (_userWord[i] == CorrectWord.ToLower()[i])
                    word.Add(new SutomWordEntityWrapper() { Status = 2, Value = _userWord[i] });
                else if (CorrectWord.ToLower().Contains(_userWord[i]))
                    word.Add(new SutomWordEntityWrapper() { Status = 1, Value = _userWord[i] });
                else
                    word.Add(new SutomWordEntityWrapper() { Status = 0, Value = _userWord[i] });
            }

            Words.Add(word);

            UserWord = "";
        }

        #region Props
        public string CorrectWord => "Banale";

        #region UserWord
        private string _userWord;
        public string UserWord
        {
            get => _userWord;
            set
            {
                _userWord = value.ToLower();
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
