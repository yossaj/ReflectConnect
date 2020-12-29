using System;
using NetworkExtension;
using SRConnect.iOS;
using SRConnect.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(WifiConnectServiceIOS))]
namespace SRConnect.iOS
{
    public class WifiConnectServiceIOS : IWifiConnect
    {


        public void ConnectToWifi(string ssid, string password)
        {
            var wifiManager = new NEHotspotConfigurationManager();
            var wifiConfig = new NEHotspotConfiguration(ssid, password, false);

            wifiManager.ApplyConfiguration(wifiConfig, (error) =>
            {
                if (error != null)
                {
                    Console.WriteLine($"Error while connecting to WiFi network {ssid}: {error}");
                }
            });
        }
    }
}
