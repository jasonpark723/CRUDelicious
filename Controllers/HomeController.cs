using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private CRUDContext dbContext;

        public HomeController(CRUDContext context)
        {
            dbContext = context;
        }


        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            foreach (var dish in AllDishes)
            {
                System.Console.WriteLine("DIsh is " + dish.Name);
            }
            return View(AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Create(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        // show one dish
        [HttpGet("{dishId}")]
        public IActionResult Show(int dishId)
        {
            Dish foundDish = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View(foundDish);
        }

        [HttpGet("edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish dishToEdit = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View(dishToEdit);
        }

        [HttpPost("Update/{dishId}")]
        public IActionResult Update(int dishId, Dish updatedDish)
        {
            Dish dishToUpdate = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (ModelState.IsValid && dishToUpdate != null)
            {
                dishToUpdate.Name = updatedDish.Name;
                dishToUpdate.Chef = updatedDish.Chef;
                dishToUpdate.Calories = updatedDish.Calories;
                dishToUpdate.Tastiness = updatedDish.Tastiness;
                dishToUpdate.Description = updatedDish.Description;
                dishToUpdate.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", updatedDish);
        }

        [HttpGet("delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dishToDelete = dbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dishToDelete != null)
            {
                dbContext.Dishes.Remove(dishToDelete);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
