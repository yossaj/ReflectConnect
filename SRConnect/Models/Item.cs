using System;
using MvvmCross.Commands;

namespace SRConnect.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public IMvxCommand Connect { get; set; }
    }
}