using PocMaui.Models.Entities;
using PocMaui.Repositories;
using PocMaui.Repositories.Interfaces;
using PocMaui.Services.Interfaces;
using PocMaui.ViewModels.Base;

namespace PocMaui.ViewModels
{
    public class ColorPickerViewModel : BaseViewModel
    {
        #region Privates
        private readonly IColorService _colorService;
        #endregion

        #region CTOR
        public ColorPickerViewModel()
        {
            _colorService = Services.ServiceProvider.GetService<IColorService>();
            SaveColorCommand = new Command(async () => await OnSaveColorCommand());

            LoadColors();
        }
        #endregion

        #region Props

        public string HexaColorString => $"{FrameColor.ToHex()}";

        #region RedSliderValue
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
        #endregion

        #region GreenSliderValue
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
        #endregion

        #region BlueSliderValue
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
        #endregion

        #region Colors
        private List<ColorEntity> _colors;
        public List<ColorEntity> Colors
        {
            get => _colors;
            set
            {
                _colors = value;
                NotifyPropertyChanged(nameof(Colors));
            }
        }
        #endregion

        public Color FrameColor  => Color.FromRgb(RedSliderValue, GreenSliderValue, BlueSliderValue);
        #endregion

        #region Methods
        public Command SaveColorCommand { get; set; }
        public async Task OnSaveColorCommand()
        {
            var colorEntity = new ColorEntity("test", FrameColor.ToHex());

            var colorName = await Application.Current.MainPage.DisplayPromptAsync("Information", "Enter your color name");

            colorEntity.Name = colorName;

            await _colorService.SaveColorDatabaseAsync(colorEntity);
            Colors.Add(colorEntity);
        }

        public async Task LoadColors()
        {
            Colors = await _colorService.GetColorsDatabaseAsync();
        }
        #endregion
    }
}
