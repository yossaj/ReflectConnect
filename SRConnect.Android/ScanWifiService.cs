using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Net.Wifi;
using AndroidX.Lifecycle;
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

        public void ScanForAvailableNetworks()
        {
            var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

            WifiBroadcastReceiver wifiBroadcastReceiver = new WifiBroadcastReceiver(wifiManager);
            Android.App.Application.Context
                .RegisterReceiver(
                wifiBroadcastReceiver,
                new IntentFilter(WifiManager.ScanResultsAvailableAction)
                );

            wifiManager.StartScan();

        }

        private class WifiBroadcastReceiver : BroadcastReceiver
        {
            WifiManager wifiManager;
            public WifiBroadcastReceiver(WifiManager mWifiManager)
            {
                wifiManager = mWifiManager;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action.Equals(WifiManager.ScanResultsAvailableAction))
                {
                    IList<ScanResult> scanResults = wifiManager.ScanResults;
                    List<WifiNetwork> AvailableNetworks = new List<WifiNetwork>();
                    if (scanResults == null)
                    {
                        AvailableNetworks.Add(
                            new WifiNetwork
                            {
                                SSID = "ScanFailed"
                            }
                            );
                    }


                    foreach (var network in scanResults)
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

                    MessagingCenter.Send<object, IList<WifiNetwork>>(Application.Current, "WifiList", AvailableNetworks);
                }
            }
        }

    }
}
