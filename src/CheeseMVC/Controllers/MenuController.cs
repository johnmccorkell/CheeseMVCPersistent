using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Menu> menus = context.Menus.ToList();

            return View(menus);

        }

        [HttpGet]
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
            
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name


                };
                context.Menus.Add(newMenu);
                context.SaveChanges();

                //return Redirect("/Menu/ViewMenu/" + newMenu.ID);
                return Redirect("/Menu/Index");
            }

            return View(addMenuViewModel);

        }

        public IActionResult ViewMenu(int id)
        {

            Menu menu = context.Menus.Single
                (m => m.ID == id);

            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel(menu, items);

            ViewBag.Title= menu.Name.ToString();

            return View(viewMenuViewModel);

        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            Menu menu = context.Menus.Single
                (m => m.ID == id);

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu,cheeses);
            
            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                        .Where(cm => cm.CheeseID == addMenuItemViewModel.cheeseID)
                        .Where(cm => cm.MenuID == addMenuItemViewModel.menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        CheeseID=addMenuItemViewModel.cheeseID,
                        MenuID = addMenuItemViewModel.menuID
                    };

                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                    return Redirect("/Menu/ViewMenu/" + newCheeseMenu.MenuID);

                };


            }
            return Redirect("/Menu/ViewMenu/" + addMenuItemViewModel.menuID);




        }




    }


    
}
