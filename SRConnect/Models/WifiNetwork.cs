using System;
namespace SRConnect.Models
{
    public class WifiNetwork
    {
        public string SSID { get; set; }
        public string NetworkPass { get; set; }
        public int NetworkId { get; set;}
        public bool Connected { get; set; }
        public bool Connecting { get; set; }
        public bool Saved { get; set; }
        public string DeviceName { get; set; }
    }
}
