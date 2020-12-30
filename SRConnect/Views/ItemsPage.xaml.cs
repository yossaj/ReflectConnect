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

namespace SRConnect.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class ItemsPage : MvxContentPage<ItemsViewModel>
    {

        public ItemsPage()
        {
            InitializeComponent();

        }

        void OnItemSelected(object sender, EventArgs args)
        {
            Xamarin.Forms.DependencyService.Get<IWifiConnect>().ConnectToWifi("", "");
            //var layout = (BindableObject)sender;
            //var item = (Item)layout.BindingContext;
            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

    }
}