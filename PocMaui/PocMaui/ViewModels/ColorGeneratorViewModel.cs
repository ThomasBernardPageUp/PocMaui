using PocMaui.Models.Entities;
using PocMaui.Services.Interfaces;
using PocMaui.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.ViewModels
{
    public class ColorGeneratorViewModel : BaseViewModel
    {
        #region Privates
        private readonly IColorService _colorService;
        #endregion

        #region CTOR
        public ColorGeneratorViewModel(INavigation navigation) : base(navigation)
        {
            GenerateColorsCommand = new Command(async () => await OnGenerateColorsCommand());
            SaveColorsCommand = new Command(async () => await OnSaveColorsCommand());
            CheckPictureColorsCommand = new Command(async () => await OnCheckPictureColorsCommand());

            PictureLink = "https://sightengine.com/assets/img/examples/example7.jpg";

            _colorService = Services.ServiceProvider.GetService<IColorService>();
        }
        #endregion

        public string PictureLink { get; set; }

        #region GeneratedColors
        private ObservableCollection<ColorEntity> _generatedColors;
        public ObservableCollection<ColorEntity> GeneratedColors
        {
            get => _generatedColors;
            set
            {
                _generatedColors = value;
                NotifyPropertyChanged(nameof(GeneratedColors));
            }
        }
        #endregion


        #region Methods


        public Command CheckPictureColorsCommand { get; set; }
        public async Task OnCheckPictureColorsCommand()
        {
            var colors = await _colorService.GetPictureColorsAsync(PictureLink);
            GeneratedColors = new ObservableCollection<ColorEntity>(colors);
        }

        #region GenerateColorsCommand => OnGenerateColorsCommand
        public Command GenerateColorsCommand { get; set; }
        public async Task OnGenerateColorsCommand()
        {
            var colors = await _colorService.GenerateColorsAsync();
            GeneratedColors = new ObservableCollection<ColorEntity>(colors);
        }
        #endregion

        #region SaveColorsCommand => OnSaveColorsCommand
        public Command SaveColorsCommand { get; set; }
        private async Task OnSaveColorsCommand()
        {
            if (GeneratedColors == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please generate a theme before !", "Ok");
                return;
            }
            await _colorService.SaveColorDatabaseAsync(GeneratedColors);
        }
        #endregion

        #endregion
    }
}