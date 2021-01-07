using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SRConnect.Models;
using SRConnect.ViewModels;

namespace SRConnect.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var wifiNetwork = new WifiNetwork
            {
                SSID = "Item 1",
                DeviceName = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(wifiNetwork);
            BindingContext = viewModel;
        }
    }
}