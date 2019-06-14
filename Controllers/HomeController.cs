using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chefsnDishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace chefsnDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(j => j.Dishes)
                .ToList();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < AllChefs.Count; i++)
            {
                int birthYear = AllChefs[i].BDay.Year;
                AllChefs[i].Age = currentYear - birthYear;
            }
            return View(AllChefs);
        }

        [Route("newchef")]
        [HttpGet]
        public IActionResult NewChef()
        {
            return View();
        }

        [Route("addchef")]
        [HttpPost]
        public IActionResult AddChef(Chef chef)
        {
            if (ModelState.IsValid)
            {
                DateTime Today = DateTime.Now;
                if (chef.BDay > Today)
                {
                    ModelState.AddModelError("BDay", "Are you a time traveler?");
                    return View("NewChef");
                }
                if (Today.Year - chef.BDay.Year < 18)
                {
                    ModelState.AddModelError("BDay", "Aren't you a little young to be a Chef?");
                    return View("NewChef");
                }
                dbContext.Chefs.Add(chef);
                dbContext.SaveChanges();
                System.Console.WriteLine("Added "+chef.Name);
                return RedirectToAction("Index");
            }
            return View("NewChef");
        }

        [Route("dishes")]
        [HttpGet]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            for (int j = 0; j < AllDishes.Count; j++)
            {
                int findId = AllDishes[j].ChefId;
                Chef chef = dbContext.Chefs.FirstOrDefault(i => i.Id == findId);
                AllDishes[j].DishChef = chef;
            }
            return View(AllDishes);
        }

        [Route("newdish")]
        [HttpGet]
        public IActionResult NewDish()
        {
            DishView thisdish = new DishView();
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            thisdish.ListofChefs = AllChefs;
            return View(thisdish);
        }

        [Route("adddish")]
        [HttpPost]
        public IActionResult AddDish(DishView dish)
        {
            if (ModelState.IsValid)
            {
                DateTime Today = DateTime.Now;
                if (dish.Calories < 0)
                {
                    ModelState.AddModelError("Calories", "That's not enough calories!");
                    return View("NewDish");
                }
                Dish newdish = new Dish();
                newdish.Name = dish.Name;
                newdish.Calories = dish.Calories;
                newdish.ChefId = dish.ChefId;
                newdish.Tastiness = dish.Tastiness;
                newdish.UpdatedAt = DateTime.Now;
                newdish.CreatedAt = DateTime.Now;
                dbContext.Dishes.Add(newdish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            return View("NewDish");
        }
    }
}
