using System;
using System.ComponentModel;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SRConnect.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SRConnect.Models;

namespace SRConnect.Views
{
    [MvxMasterDetailPagePresentation(WrapInNavigationPage = true, NoHistory = true)]
    public partial class DevicePage : MvxContentPage<DeviceViewModel>
    {

        public DevicePage()
        {
            InitializeComponent();

            
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            WifiNetwork device = (WifiNetwork)args.Parameter;
            ViewModel.DeleteNetwork(device);
        }


    }
}
