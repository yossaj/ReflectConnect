using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SRConnect.Models;
using SRConnect.Views;
using SRConnect.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using SRConnect.Services;
using Xamarin.Essentials;

namespace SRConnect.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class ItemsPage : MvxContentPage<ItemsViewModel>
    {

        public ItemsPage()
        {
            InitializeComponent();

        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            bool hasLocationPermission = await CheckLocationPermission();
            if (hasLocationPermission)
            {
                Xamarin.Forms.DependencyService.Get<IWifiConnect>().ConnectToWifi("VM6980772", "6gmvhqsXnthr");
            }
            else
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if(await CheckLocationPermission())
                {
                    Xamarin.Forms.DependencyService.Get<IWifiConnect>().ConnectToWifi("VM6980772", "6gmvhqsXnthr");
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

        async void AddItem_Clicked(object sender, EventArgs e)
        {
          await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
             }

        public async Task<bool> CheckLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            return status == PermissionStatus.Granted;
        }

    }
}