using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SRConnect.Models;
using SRConnect.Views;
using SRConnect.Services;
using Xamarin.Essentials;

namespace SRConnect.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public override async Task Initialize()
        {
            await ExecuteLoadItemsCommand();
            await ScanForNetworksAsync();
            await base.Initialize();
        }

        public async Task<bool> CheckLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            return status == PermissionStatus.Granted;
        }

        public async Task ScanForNetworksAsync()
        {
            bool hasLocationPermission = await CheckLocationPermission();
            if (hasLocationPermission)
            {
                ShowNetworks();
            }
            else
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (await CheckLocationPermission())
                {
                    ShowNetworks();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Permission Not Granted",
                        "Location Permission Needs to be Granted In Order to Scan Networks",
                        "OK");
                }
            }
        }

        public async void ShowNetworks()
        {
            var networks = Xamarin.Forms.DependencyService.Get<IWifiScan>().ScanForAvailableNetworks();
            var networkNames = "";
            foreach (var network in networks)
            {
                networkNames += network.SSID + "\n";
            }
            await Application.Current.MainPage.DisplayAlert("Network Test", networkNames, "OK");
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}