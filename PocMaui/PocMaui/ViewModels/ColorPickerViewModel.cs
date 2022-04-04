using PocMaui.Models.Entities;
using PocMaui.Repositories;
using PocMaui.Repositories.Interfaces;
using PocMaui.ViewModels.Base;

namespace PocMaui.ViewModels
{
    public class ColorPickerViewModel : BaseViewModel
    {
        #region Privates
        private readonly IRepository<ColorEntity> _colorRepository;
        #endregion

        #region CTOR
        public ColorPickerViewModel()
        {
            _colorRepository = Services.ServiceProvider.GetService<IRepository<ColorEntity>>();
            this.SaveColorCommand = new Command(async () => await OnSaveColorCommand());
            _colorRepository = new Repository<ColorEntity>();
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

        #region Methods
        public Command SaveColorCommand { get; set; }
        public async Task OnSaveColorCommand()
        {
            var colorEntity = new ColorEntity("test", FrameColor.ToHex());

            var colorName = await Application.Current.MainPage.DisplayPromptAsync("Information", "Enter your color name");

            colorEntity.Name = colorName;

            _colorRepository.Insert(colorEntity);
        }
        #endregion
    }
}
