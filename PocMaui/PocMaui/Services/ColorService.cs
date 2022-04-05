﻿using PocMaui.Commons;
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

        public async Task<IEnumerable<ColorEntity>> SaveColorDatabaseAsync(IEnumerable<ColorEntity> colors)
        {
            return colors.Select(c => _colorRepository.Insert(c)).ToList();
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
            var random = new Random();
            string randomHexa = random.Next(0, 16777215).ToString("X");

            var url = Constants.GetColorsApiEndPoint + randomHexa;
            var colorsRoot = await _httpService.SendHttpRequest<ColorDTODown>(url, HttpMethod.Get);
            return colorsRoot.Colors;
        }

        public async Task<IEnumerable<ColorEntity>> GetPictureColorsAsync(string pictureUrl)
        {
            var url = $"{Constants.GetPictureColorsApiBaseUrl}?models=properties&url={pictureUrl}";
            var res = await _httpService.SendHttpRequest<PictureColorsDTODown>(url, HttpMethod.Get);

            return res.Colors.Accent.Select(c => new ColorEntity(c.Hex.ToUpper(), c.Hex.ToUpper()));
        }
    }
}
