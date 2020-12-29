using System;
namespace SRConnect.Services
{
    public interface IWifiConnect
    {
        void ConnectToWifi(string ssid, string password);
    }
}
