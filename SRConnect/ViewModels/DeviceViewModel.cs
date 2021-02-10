using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SRConnect.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {
        public DeviceViewModel()
        {
            Title = "Device";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.smartreflectors.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
