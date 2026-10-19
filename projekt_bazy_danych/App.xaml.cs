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
                    services.AddTransient<DodajKlientaViewModel>();
                    services.AddTransient<DodajKlientaView>();
                    services.AddTransient<DodajPojazdViewModel>();
                    services.AddTransient<DodajPojazdView>();
                    services.AddTransient<DataProvider>();
                    services.AddTransient<AddDataService>();
                    services.AddTransient<EditDataService>();
                    services.AddTransient<CsvExportService>();
                    services.AddTransient<EdytujZamowienieViewModel>();
                    services.AddTransient<EdytujZamowienieWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            MainWindow? mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);

            await sprawdzPolaczenieZBazaAsync();
        }

        /// <summary>
        /// Sprawdza dostępność bazy danych po uruchomieniu aplikacji i wyświetla komunikat w przypadku braku połączenia.
        /// </summary>
        private async Task sprawdzPolaczenieZBazaAsync()
        {
            try
            {
                using IServiceScope scope = AppHost!.Services.CreateScope();
                Datacontext context = scope.ServiceProvider.GetRequiredService<Datacontext>();
                bool polaczono = await context.Database.CanConnectAsync();

                if (!polaczono)
                {
                    MessageBox.Show(
                        "Nie udało się nawiązać połączenia z bazą danych MySQL. Aplikacja została uruchomiona w trybie offline.",
                        "Brak połączenia z bazą danych",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Nie udało się połączyć z bazą danych MySQL:\n{ex.Message}\nAplikacja została uruchomiona w trybie offline.",
                    "Brak połączenia z bazą danych",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
