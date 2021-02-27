using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SRConnect.ViewModels
{
    public class Class1 : BaseViewModel
    {
        public Class1()
        {
            Title = "Class";
        }

        public ICommand OpenWebCommand { get; }
    }
}
