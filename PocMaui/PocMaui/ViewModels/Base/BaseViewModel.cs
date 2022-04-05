using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace PocMaui.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigation Navigation;

        public BaseViewModel()
        {

        }

        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
