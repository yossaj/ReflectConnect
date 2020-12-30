using System;
using NetworkExtension;
using SRConnect.iOS;
using SRConnect.Services;
using UIKit;
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
                    //var dismissed = UIAlertAction();

                    var alert = UIAlertController.Create(ssid, password, UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK" , UIAlertActionStyle.Default, (UIAlertAction obj) =>
                    {

                    }));
                    var rootVC = UIApplication.SharedApplication.Windows[0].RootViewController;
                    rootVC.PresentViewController(alert, true, null);
                    Console.WriteLine($"Error while connecting to WiFi network {ssid}: {error}");
                }
            });
        }
    }
}
