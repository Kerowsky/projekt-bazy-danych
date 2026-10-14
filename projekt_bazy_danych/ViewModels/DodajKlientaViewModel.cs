using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.Common.Utils;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    public partial class DodajKlientaViewModel : ObservableObject
    {
        #region Fields
        private readonly Datacontext context;

        private readonly AddDataService addDataService;
        #endregion

        #region Properties
        [ObservableProperty]
        private string? imie;

        [ObservableProperty]
        private string? nazwisko;

        [ObservableProperty]
        private string? pesel;
        #endregion

        #region Constructors
        public DodajKlientaViewModel(
            Datacontext context,
            AddDataService addDataService)
        {
            this.context = context;
            this.addDataService = addDataService;
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void DodajUzytkownika()
        {
            _ = DodajUzytkownikaAsync();
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        private async Task DodajUzytkownikaAsync()
        {
            ResultWithMessage result = await addDataService.AddNewKlient(Imie, Nazwisko, Pesel);

            if (!result.IsValid)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Message);
            }

        }
        #endregion
    }
}
