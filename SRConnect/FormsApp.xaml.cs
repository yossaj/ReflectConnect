using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SRConnect.Services;
using SRConnect.Views;

namespace SRConnect
{
    public partial class FormsApp : Application
    {

        public FormsApp()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
