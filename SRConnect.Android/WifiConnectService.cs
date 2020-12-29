using System;
using SRConnect.Droid;
using SRConnect.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(WifiConnectService))]
namespace SRConnect.Droid
{

    public class WifiConnectService: IWifiConnect
    {

        public void ConnectToWifi(string ssid, string password)
        {
            Application.Current.MainPage.DisplayAlert("Wifi Test", ssid, "OK");
        }
    }
}
