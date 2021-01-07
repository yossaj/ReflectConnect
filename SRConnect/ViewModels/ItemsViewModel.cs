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
        private ObservableCollection<WifiNetwork> availableNetworks { get; set; }
        public ObservableCollection<WifiNetwork> AvailableNetworks
        {
            get => availableNetworks;
            set
            {
                availableNetworks = value;
                RaisePropertyChanged(() => AvailableNetworks);
            }
        }

        public Command ScanForNetworksCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            AvailableNetworks = new ObservableCollection<WifiNetwork>();
            ScanForNetworksCommand = new Command(async () => await ScanForNetworksAsync());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    AvailableNetworks.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        public override async Task Initialize()
        {
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
            IsBusy = true;
            Xamarin.Forms.DependencyService.Get<IWifiScan>().ScanForAvailableNetworks();
            MessagingCenter.Subscribe<object, IList<WifiNetwork>>(Application.Current, "WifiList", async (sender, wifiNetworks) =>
            {
                try
                {
                    AvailableNetworks.Clear();
                    foreach (var network in wifiNetworks)
                    {
                        AvailableNetworks.Add(network);
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
            });
        }
    }
}