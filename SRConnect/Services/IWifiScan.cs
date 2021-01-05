using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRConnect.Models;

namespace SRConnect.Services
{
    public interface IWifiScan
    {
        void ScanForAvailableNetworks();
    }
}
