using System;
using MvvmCross.Commands;
using SQLite;

namespace SRConnect.Models
{
    [Table("devices")]
    public class WifiNetwork
    {
       [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string SSID { get; set; }
        public string NetworkPass { get; set; }
        public int NetworkId { get; set;}
        [Ignore]
        public bool Connected { get; set; }
        [Ignore]
        public bool Connecting { get; set; }
        public bool Saved { get; set; }
        public string DeviceName { get; set; }
        [Ignore]
        public IMvxCommand Connect { get; set; }
    }
}
