using PocMaui.Models.Entities;
using PocMaui.Repositories;
using PocMaui.Repositories.Interfaces;

namespace PocMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

		#region Repo
		builder.Services.AddSingleton<IRepository<ColorEntity>, Repository<ColorEntity>>();
		#endregion




		return builder.Build();
	}
}
