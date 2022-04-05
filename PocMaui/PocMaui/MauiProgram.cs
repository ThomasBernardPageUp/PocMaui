using PocMaui.Models.Entities;
using PocMaui.Repositories;
using PocMaui.Repositories.Interfaces;
using PocMaui.Services;
using PocMaui.Services.Interfaces;

namespace PocMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

		#region Repositories
		builder.Services.AddSingleton<IRepository<ColorEntity>, Repository<ColorEntity>>();
		#endregion

		#region Services
		builder.Services.AddSingleton<IColorService, ColorService>();
		builder.Services.AddSingleton<IHttpService, HttpService>();
		#endregion




		return builder.Build();
	}
}
