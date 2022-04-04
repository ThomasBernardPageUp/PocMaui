using PocMaui.Models.Entities;
using PocMaui.Repositories;
using PocMaui.Repositories.Interfaces;
using PocMaui.Services.Interfaces;
using PocMaui.ViewModels.Base;
using System.Collections.ObjectModel;

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
            DeleteColorCommand = new Command<ColorEntity>(async (ColorEntity color) => await OnDeleteColorCommand(color));

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
        private ObservableCollection<ColorEntity> _colors;
        public ObservableCollection<ColorEntity> Colors
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
        #region DeleteColorCommand => OnDeleteColorCommand
        public Command<ColorEntity> DeleteColorCommand { get; set; }
        public async Task OnDeleteColorCommand(ColorEntity color)
        {
            Colors.Remove(color);
            await _colorService.DeleteColorDatabaseAsync(color);
        }
        #endregion

        #region SaveColorCommand => OnSaveColorCommand
        public Command SaveColorCommand { get; set; }
        public async Task OnSaveColorCommand()
        {
            var colorEntity = new ColorEntity("test", FrameColor.ToHex());

            // var colorName = await Application.Current.MainPage.DisplayPromptAsync("Information", "Enter your color name");
            var colorName = "color";

            colorEntity.Name = colorName;

            await _colorService.SaveColorDatabaseAsync(colorEntity);
            Colors.Add(colorEntity);
        }
        #endregion

        public async Task LoadColors()
        {
            Colors = new ObservableCollection<ColorEntity>(await _colorService.GetColorsDatabaseAsync());
        }
        #endregion
    }
}
