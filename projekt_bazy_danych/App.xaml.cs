using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Providers;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.ViewModels;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<Datacontext>();
                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<MainWindow>();
                    services.AddTransient<DodajZamowienieViewModel>();
                    services.AddTransient<DodajZamowienieWindow>();
                    services.AddSingleton<DataProvider>();
                    services.AddSingleton<FactoryOrderService>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            MainWindow? mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
