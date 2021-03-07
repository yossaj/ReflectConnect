using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SRConnect.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using MvvmCross.Commands;

namespace SRConnect.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {
     

        private ObservableCollection<WifiNetwork> _deviceCollection;
        public ObservableCollection<WifiNetwork> DeviceCollection
        {
            get => _deviceCollection;
            set => SetProperty(ref _deviceCollection, value);
        }

        public DeviceViewModel()
        {
            Title = "Devices";
        }

        public MvxCommand<WifiNetwork> DeleteDeviceCommand { get; set; }

        public override Task Initialize()
        {
            List<WifiNetwork> testdata = SetDummyData();
            DeleteDeviceCommand = new MvxCommand<WifiNetwork>((device) => DeleteNetwork(device));
            AddDatbase(testdata);
            DeviceCollection = GetWifiNetworks();
            return base.Initialize();
        }

   

        public List<WifiNetwork> SetDummyData()
        {
            WifiNetwork wifiNetwork1 = new WifiNetwork()
            {
                SSID = "TestSSID1",
                DeviceName = "MySmartLight",
                Saved = true,
                Connected = true,
                Connecting = false
            };

            WifiNetwork wifiNetwork2 = new WifiNetwork()
            {
                SSID = "TestSSID2",
                DeviceName = "MySmartValve",
                Saved = true,
                Connected = true,
                Connecting = false
            };

            WifiNetwork wifiNetwork3 = new WifiNetwork()
            {
                SSID = "TestSSID3",
                DeviceName = "MySmartShoes",
                Saved = true,
                Connected = false,
                Connecting = false
            };
            List<WifiNetwork> dummyList = new List<WifiNetwork>();
            dummyList.Add(wifiNetwork1);
            dummyList.Add(wifiNetwork2);
            dummyList.Add(wifiNetwork3);
            return dummyList;
        }

        void AddDatbase(IList devicelist)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices_db.sqlite")))
            {
                conn.CreateTable<WifiNetwork>();
                foreach(WifiNetwork device in devicelist)
                {
                    var isPresent = conn.Query<WifiNetwork>($"SELECT * FROM devices WHERE SSID = '{device.SSID}'");
                    if(isPresent.Count() == 0)
                    {
                        conn.Insert(device);
                    }
                }
            }
        }

        ObservableCollection<WifiNetwork> GetWifiNetworks()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices_db.sqlite")))
            {
                conn.CreateTable<WifiNetwork>();
                List<WifiNetwork> devicelist = conn.Table<WifiNetwork>().ToList();
                ObservableCollection<WifiNetwork> deviceCollection = new ObservableCollection<WifiNetwork>(devicelist);
                return deviceCollection;
            }
        }

        public void DeleteNetwork(WifiNetwork device)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices_db.sqlite")))
            {
               conn.Delete(device );
            }
            DeviceCollection = GetWifiNetworks();
        }




    }
}
