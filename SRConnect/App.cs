using System;
using MvvmCross.ViewModels;
using SRConnect.ViewModels;
using SRConnect.Views;

namespace SRConnect
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MasterDetailViewModel>();
        }
    }
}
