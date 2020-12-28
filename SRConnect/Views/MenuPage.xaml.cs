using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SRConnect.Models;
using SRConnect.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnect.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuPage : MvxContentPage<MenuViewModel>
    {
    
        public MenuPage()
        {
            InitializeComponent();
        }
    }
}