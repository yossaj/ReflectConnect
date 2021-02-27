using System;
using System.ComponentModel;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SRConnect.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnect.Views
{
    [MvxMasterDetailPagePresentation(WrapInNavigationPage = true, NoHistory = true)]
    public partial class Page1 : MvxContentPage<Class1>
    {
        public Page1()
        {
            InitializeComponent();
        }

        int count = 0;
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"You Clickerd {count} times.";
        }
    }
}
