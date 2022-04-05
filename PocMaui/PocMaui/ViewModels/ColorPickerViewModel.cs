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
        public ColorPickerViewModel(INavigation navigation):base(navigation)
        {
            _colorService = Services.ServiceProvider.GetService<IColorService>();
            SaveColorCommand = new Command(async () => await OnSaveColorCommand());
            DeleteColorCommand = new Command<ColorEntity>(async (ColorEntity color) => await OnDeleteColorCommand(color));
            ResetHistoryCommand = new Command(async () => await OnResetHistoryCommand());
            SelectColorCommand = new Command<ColorEntity>(async (ColorEntity color) => await OnSelectColorCommand(color));
            GenerateColorsCommand = new Command(async () => await OnGenerateColorsCommand());

        }
        #endregion

        public void OnNavigatedTo(NavigatedToEventArgs args)
        {
            LoadColors();
        }

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
                NotifyPropertyChanged(nameof(RedSliderValue));
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
                NotifyPropertyChanged(nameof(GreenSliderValue));
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
                NotifyPropertyChanged(nameof(BlueSliderValue));
            }
        }
        #endregion

        public Color FrameColor => Color.FromRgb(RedSliderValue, GreenSliderValue, BlueSliderValue);

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

        #endregion

        #region Methods

        #region GenerateColorsCommand => OnGenerateColorsCommand
        public Command GenerateColorsCommand { get; set; }
        public async Task OnGenerateColorsCommand()
        {
            await Navigation.PushAsync(new ColorGeneratorPage());
        }
        #endregion

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

        #region ResetHistoryCommand => OnResetHistoryCommand
        public Command ResetHistoryCommand { get; set; }
        public async Task OnResetHistoryCommand()
        {
            Colors = new ObservableCollection<ColorEntity>();
            await _colorService.DeleteColorsDatabaseAsync();
        }
        #endregion

        #region SelectColorCommand => OnSelectColorCommand
        public Command<ColorEntity> SelectColorCommand { get; set; }
        public async Task OnSelectColorCommand(ColorEntity colorEntity)
        {
            var color = Color.FromHex(colorEntity.HexaCode);
            RedSliderValue = (int)(color.Red*255);
            GreenSliderValue = (int)(color.Green*255);
            BlueSliderValue = (int)(color.Blue*255);
        }
        #endregion

        #region LoadColors
        public async Task LoadColors()
        {
            Colors = new ObservableCollection<ColorEntity>(await _colorService.GetColorsDatabaseAsync());
        }
        #endregion

        #endregion
    }
}
