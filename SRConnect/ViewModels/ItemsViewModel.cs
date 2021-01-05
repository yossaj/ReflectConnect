using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SRConnect.Models;
using SRConnect.Views;
using SRConnect.Services;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace SRConnect.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Item> availableNetworks { get; set; }
        public ObservableCollection<Item> AvailableNetworks
        {
            get => availableNetworks;
            set
            {
                availableNetworks = value;
                RaisePropertyChanged(() => AvailableNetworks);
            }
        }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            AvailableNetworks = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                AvailableNetworks.Add(newItem);
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

        public void ShowNetworks()
        {
            Xamarin.Forms.DependencyService.Get<IWifiScan>().ScanForAvailableNetworks();
           
            MessagingCenter.Subscribe<object, IList<WifiNetwork>>(Application.Current, "WifiList", async (sender, wifiNetworks) =>
            {
                string networkNames = "";
                foreach (var network in wifiNetworks)
                {
                    networkNames += network.SSID + "\n";
                }
                await Application.Current.MainPage.DisplayAlert("Network Test", networkNames, "OK");
            });

            
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                AvailableNetworks.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    AvailableNetworks.Add(item);
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