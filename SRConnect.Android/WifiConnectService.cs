using System;
using System.Linq;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using SRConnect.Droid;
using SRConnect.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(WifiConnectService))]
namespace SRConnect.Droid
{

    public class WifiConnectService: IWifiConnect
    {
        [Obsolete]
        public void ConnectToWifi(string ssid, string password)
        {
            if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Q)
            {
                var formattedSsid = $"\"{ssid}\"";
                var formattedPassword = $"\"{password}\"";

                var wifiConfig = new WifiConfiguration
                {
                    Ssid = formattedSsid,
                    PreSharedKey = formattedPassword
                };

                var wifiManager = (WifiManager)Android.App.Application.Context
                        .GetSystemService(Context.WifiService);


                var addNetwork = wifiManager.AddNetwork(wifiConfig);

                var networks = wifiManager.ConfiguredNetworks;
                string networkssids = "";
                networks.ForEach(n =>
                    networkssids += $"{n.Ssid} \n"
                );
                
               


                Application.Current.MainPage.DisplayAlert("Wifi Test", $"Available network: \n {networkssids}", "OK");

                //if (network == null)
                //{
                //    Console.WriteLine($"Cannot connect to network: {formattedSsid}");
                Application.Current.MainPage.DisplayAlert("Wifi Test", $"Cannot connect to network: {formattedSsid}", "OK");
                //    return;
                //}

            }
            else
            {
                WifiNetworkSpecifier.Builder builder = new WifiNetworkSpecifier.Builder();
                WifiNetworkSpecifier networkSpecifier = builder
                                                            .SetSsid(ssid)
                                                            .SetWpa2Passphrase(password)
                                                            .Build();

                NetworkRequest.Builder networkRequest = new NetworkRequest.Builder();
                networkRequest
                        .SetNetworkSpecifier(networkSpecifier)
                        .Build();

                ConnectivityManager connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);

                NetworkCallback networkCallback = new NetworkCallback(connectivityManager);
            }

     

            //Application.Current.MainPage.DisplayAlert("Wifi Test", ssid, "OK");
        }



        private class NetworkCallback : ConnectivityManager.NetworkCallback
        {
            private ConnectivityManager _conn;
            public Action<Network> NetworkAvailable { get; set; }
            public Action NetworkUnavailable { get; set; }

            public NetworkCallback(ConnectivityManager connectivityManager)
            {
                _conn = connectivityManager;
            }

            public override void OnAvailable(Network network)
            {
                base.OnAvailable(network);
                // Need this to bind to network otherwise it is connected to wifi 
                // but traffic is not routed to the wifi specified
                _conn.BindProcessToNetwork(network);
                NetworkAvailable?.Invoke(network);
            }

            public override void OnUnavailable()
            {
                base.OnUnavailable();

                NetworkUnavailable?.Invoke();
            }
        }


    }


}

