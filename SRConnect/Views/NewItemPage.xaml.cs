using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SRConnect.Models;

namespace SRConnect.Views
{
    public partial class NewItemPage : ContentPage
    {
        public WifiNetwork WifiNetwork { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            WifiNetwork = new WifiNetwork
            {
                SSID = "Item name",
                DeviceName = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", WifiNetwork);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}