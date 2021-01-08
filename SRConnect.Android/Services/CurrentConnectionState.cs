using System;
using Android.Content;
using Android.Net.Wifi;
using Xamarin.Forms;

namespace SRConnect.Droid.Services
{
    public class CurrentConnectionState
    {
        public CurrentConnectionState()
        {

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
                if (intent.Action.Equals(WifiManager.WifiStateChangedAction))
                {
                    Application.Current.MainPage.DisplayAlert("Failed to connect", "" , "OK");
                }
                MessagingCenter.Send<object, bool>(Application.Current, "WifiList", true);

            }
        }
    }
}
