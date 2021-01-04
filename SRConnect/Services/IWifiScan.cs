using System;
using System.Collections;
using System.Collections.Generic;
using SRConnect.Models;

namespace SRConnect.Services
{
    public interface IWifiScan
    {
        List<WifiNetwork> ScanForAvailableNetworks();
    }
}
