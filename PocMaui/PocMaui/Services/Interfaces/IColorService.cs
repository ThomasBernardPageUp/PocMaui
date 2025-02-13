﻿using PocMaui.Models.DTOs.Down;
using PocMaui.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Services.Interfaces
{
    public interface IColorService
    {
        Task<ColorEntity> SaveColorDatabaseAsync(ColorEntity color);
        Task<List<ColorEntity>> GetColorsDatabaseAsync();
        Task<ColorEntity> GetColorDatabaseAsync(int colorId);
        Task DeleteColorDatabaseAsync(ColorEntity color);
        Task DeleteColorsDatabaseAsync();
        Task<IEnumerable<ColorEntity>> GenerateColorsAsync();
        Task<IEnumerable<ColorEntity>> SaveColorDatabaseAsync(IEnumerable<ColorEntity> colors);
        Task<IEnumerable<ColorEntity>> GetPictureColorsAsync(string pictureUrl);
    }
}
