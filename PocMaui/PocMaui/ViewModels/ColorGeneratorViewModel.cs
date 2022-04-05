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

            _colorService = Services.ServiceProvider.GetService<IColorService>();
        }
        #endregion

        private ObservableCollection<PocMaui.Models.DTOs.Down.Color> _generatedColors;
        public ObservableCollection<PocMaui.Models.DTOs.Down.Color> GeneratedColors
        {
            get => _generatedColors;
            set
            {
                _generatedColors = value;
                NotifyPropertyChanged(nameof(GeneratedColors));
            }
        }


        #region Methods

        #region GenerateColorsCommand => OnGenerateColorsCommand
        public Command GenerateColorsCommand { get; set; }
        public async Task OnGenerateColorsCommand()
        {
            var colors = await _colorService.GenerateColorsAsync();
            GeneratedColors = new ObservableCollection<PocMaui.Models.DTOs.Down.Color>(colors);
        }
        #endregion

        public Command SaveColorsCommand { get; set; }
        private async Task OnSaveColorsCommand()
        {
            var colorsEntity = GeneratedColors.Select(c => new ColorEntity(c.Hex.Clean, c.Hex.Value));
            await _colorService.SaveColorDatabaseAsync(colorsEntity);
        }

        #endregion
    }
}