using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;



namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }


        public ViewMenuViewModel() { }

        public ViewMenuViewModel(Menu menu)
        {
            Menu = menu;
        }


        public ViewMenuViewModel(Menu menu, List<CheeseMenu> items)
        {
            Menu = menu;
            Items = items;


        }


    }
}
