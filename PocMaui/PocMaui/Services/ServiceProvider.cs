namespace PocMaui.Services
{
    public static class ServiceProvider
    {
        public static TService GetService<TService>() => MauiApplication.Current.Services.GetService<TService>();
    }
}
