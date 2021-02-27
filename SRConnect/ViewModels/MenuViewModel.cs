using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SRConnect.Models;

namespace SRConnect.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private List<HomeMenuItem> menuItems;
        public List<HomeMenuItem> MenuItems
        {
            get => menuItems;
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        readonly IMvxNavigationService _navigationService;



        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task Initialize()
        {
            MenuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse", Navigate = new MvxAsyncCommand(async () => await _navigationService.Navigate<ItemsViewModel>()) },
                new HomeMenuItem { Id = MenuItemType.About, Title = "About", Navigate = new MvxAsyncCommand(async () => await _navigationService.Navigate<AboutViewModel>()) },
                new HomeMenuItem { Id= MenuItemType.Devices, Title="Devices", Navigate = new MvxAsyncCommand(async () => await _navigationService.Navigate<DeviceViewModel>()) },
                new HomeMenuItem { Id = MenuItemType.Class, Title = "Class", Navigate = new MvxAsyncCommand(async () => await _navigationService.Navigate<Class1>()) },
            };
            return base.Initialize();
        }
    }
}
