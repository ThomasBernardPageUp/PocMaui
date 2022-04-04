using PocMaui.Models.Entities;
using PocMaui.Repositories.Interfaces;
using PocMaui.Services.Interfaces;

namespace PocMaui.Services
{
    public class ColorService : IColorService
    {
        private readonly IRepository<ColorEntity> _colorRepository;

        public ColorService()
        {
            _colorRepository = Services.ServiceProvider.GetService<IRepository<ColorEntity>>();
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
    }
}
