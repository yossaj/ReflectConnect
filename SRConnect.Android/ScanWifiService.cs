using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Net.Wifi;
using SRConnect.Droid;
using SRConnect.Models;
using SRConnect.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScanWifiService))]
namespace SRConnect.Droid
{
    public class ScanWifiService : IWifiScan
    {

        public static IList<ScanResult> ScanResults;
        public ScanWifiService()
        {


        }

        public List<WifiNetwork> ScanForAvailableNetworks()
        {
            var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

            WifiBroadcastReciver wifiBroadcastReceiver = new WifiBroadcastReciver(wifiManager);
            Android.App.Application.Context
                .RegisterReceiver(
                wifiBroadcastReceiver,
                new IntentFilter(WifiManager.ScanResultsAvailableAction)
                );

            wifiManager.StartScan();

            List<WifiNetwork> AvailableNetworks = new List<WifiNetwork>();
            if (ScanResults == null)
            {
                AvailableNetworks.Add(
                    new WifiNetwork
                    {
                        SSID = "ScanFailed"
                    }
                    );
                return AvailableNetworks;
            }

            
            foreach (var network in ScanResults)
            {
                AvailableNetworks.Add(
                    new WifiNetwork
                    {
                        SSID = network.Ssid,
                        DeviceName = network.Ssid,
                        Connected = false,
                        Connecting = false,
                        Saved = false
                    }
                    );
            }


            return AvailableNetworks;
        }

        private class WifiBroadcastReciver : BroadcastReceiver
        {
            WifiManager wifiManager;
            public WifiBroadcastReciver(WifiManager mWifiManager)
            {
                wifiManager = mWifiManager;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action.Equals(WifiManager.ScanResultsAvailableAction))
                {
                    ScanResults = wifiManager.ScanResults;
                }
            }
        }

    }
}
