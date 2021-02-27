using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;

namespace SRConnect.Models
{
    public enum MenuItemType
    {
        Browse,
        About, 
        Devices,
        Class,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public IMvxCommand Navigate { get; set; }


    }
}
