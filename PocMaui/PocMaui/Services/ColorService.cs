using PocMaui.Commons;
using PocMaui.Models.DTOs.Down;
using PocMaui.Models.Entities;
using PocMaui.Repositories.Interfaces;
using PocMaui.Services.Interfaces;

namespace PocMaui.Services
{
    public class ColorService : IColorService
    {
        private readonly IRepository<ColorEntity> _colorRepository;
        private readonly IHttpService _httpService;

        public ColorService()
        {
            _colorRepository = ServiceProvider.GetService<IRepository<ColorEntity>>();
            _httpService = ServiceProvider.GetService<IHttpService>();
        }

        public async Task<ColorEntity> GetColorDatabaseAsync(int colorId)
        {
            return _colorRepository.GetById(colorId);
        }

        public async Task<List<ColorEntity>> GetColorsDatabaseAsync()
        {
            return _colorRepository.Get();
        }

        public async Task<ColorEntity> SaveColorDatabaseAsync(ColorEntity color)
        {
            return _colorRepository.Insert(color);
        }

        public async Task DeleteColorDatabaseAsync(ColorEntity color)
        {
            _colorRepository.Delete(color);
        }

        public async Task DeleteColorsDatabaseAsync()
        {
            _colorRepository.Clear();
        }
        public async Task<List<PocMaui.Models.DTOs.Down.Color>> GenerateColorsAsync()
        {
            var colorsRoot = await _httpService.SendHttpRequest<ColorDTODown>(Constants.GetColorsApiEndPoint, HttpMethod.Get);
            return colorsRoot.Colors;
        }
    }
}
