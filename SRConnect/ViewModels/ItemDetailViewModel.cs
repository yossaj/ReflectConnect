using System;

using SRConnect.Models;

namespace SRConnect.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public WifiNetwork WifiNetwork { get; set; }
        public ItemDetailViewModel(WifiNetwork wifiNetwork = null)
        {
            Title = wifiNetwork?.SSID;
            WifiNetwork = wifiNetwork;
        }
    }
}
