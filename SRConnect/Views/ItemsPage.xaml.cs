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