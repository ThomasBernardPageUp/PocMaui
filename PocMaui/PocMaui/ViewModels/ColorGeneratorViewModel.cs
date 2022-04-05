using PocMaui.Services.Interfaces;
using PocMaui.ViewModels.Base;
using System;
using System.Collections.Generic;
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

        public ColorGeneratorViewModel(INavigation navigation) : base(navigation)
        {
            GenerateColorsCommand = new Command(async () => await OnGenerateColorsCommand());

            _colorService = Services.ServiceProvider.GetService<IColorService>();
        }


        public Command GenerateColorsCommand { get; set; }
        public async Task OnGenerateColorsCommand()
        {
            var colors = await _colorService.GenerateColorsAsync();
        }
    }
}