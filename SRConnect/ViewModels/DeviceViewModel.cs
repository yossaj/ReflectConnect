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

        public override Task Initialize()
        {
            var testdata = SetDummyData();
            AddDatbase(testdata.ToList());
            GetWifiNetworks();
            return base.Initialize();
        }

        public ObservableCollection<WifiNetwork> SetDummyData()
        {
            WifiNetwork wifiNetwork1 = new WifiNetwork()
            {
                SSID = "TestSSID",
                DeviceName = "MySmartLight",
                Saved = true,
                Connected = true,
                Connecting = false


            };

            WifiNetwork wifiNetwork2 = new WifiNetwork()
            {
                SSID = "TestSSID",
                DeviceName = "MySmartValve",
                Saved = true,
                Connected = true,
                Connecting = false
            };

            WifiNetwork wifiNetwork3 = new WifiNetwork()
            {
                SSID = "TestSSID",
                DeviceName = "MySmartShoes",
                Saved = true,
                Connected = false,
                Connecting = false
            };
            ObservableCollection<WifiNetwork> dummyCollection = new ObservableCollection<WifiNetwork>();
            dummyCollection.Add(wifiNetwork1);
            dummyCollection.Add(wifiNetwork2);
            dummyCollection.Add(wifiNetwork3);
            return dummyCollection;
        }

        void AddDatbase(IList devicelist)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices_db.sqlite")))
            {
                conn.CreateTable<WifiNetwork>();
                conn.InsertAll(devicelist);
            }
        }

        ObservableCollection<WifiNetwork> GetWifiNetworks()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "devices_db.sqlite")))
            {
                conn.CreateTable<WifiNetwork>();
                List<WifiNetwork> devicelist = conn.Table<WifiNetwork>().ToList();
            }
        }




    }
}
