using PocMaui.ViewModels.Base;

namespace PocMaui.ViewModels
{
    public class ColorPickerViewModel : BaseViewModel
    {

        #region CTOR
        public ColorPickerViewModel()
        {
        }
        #endregion

        #region Props

        public string HexaColorString => $"{FrameColor.ToHex()}";

        private int _redSliderValue;

        public int RedSliderValue
        {
            get => _redSliderValue;
            set 
            { 
                _redSliderValue = value;
                NotifyPropertyChanged(nameof(FrameColor));
                NotifyPropertyChanged(nameof(HexaColorString));
            }
        }

        private int _greenSliderValue;
        public int GreenSliderValue
        {
            get => _greenSliderValue;
            set
            {
                _greenSliderValue = value;
                NotifyPropertyChanged(nameof(FrameColor));
                NotifyPropertyChanged(nameof(HexaColorString));
            }
        }

        private int _blueSliderValue;
        public int BlueSliderValue
        {
            get => _blueSliderValue;
            set
            {
                _blueSliderValue = value;
                NotifyPropertyChanged(nameof(HexaColorString));
                NotifyPropertyChanged(nameof(FrameColor));
            }
        }

        public Color FrameColor  => Color.FromRgb(RedSliderValue, GreenSliderValue, BlueSliderValue);  
        #endregion

    }
}
