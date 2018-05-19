using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeeMaker.DataContext;
using CoffeeMaker.Entities;

namespace CoffeeMaker.Controllers
{
    public class AddToMenuController : Controller
    {
        private DrinksDbContext drinksDb = new DrinksDbContext();
        private RecipeDbContext recipeDb = new RecipeDbContext();
        private StocksDbContext stocksDb = new StocksDbContext();

        // GET: Drinks
        public ActionResult AddToMenu()
        {
            return View(recipeDb.Recipes.ToList());
        }

        // GET: Drinks/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinks drinks = drinksDb.Drinks.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // GET: Drinks/Create
        public ActionResult CreateCoffee()
        {
            ViewBag.Drinks = drinksDb.Drinks.ToList();
            ViewBag.Stocks = stocksDb.Stocks.ToList();

            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult SubmitCoffee(Recipe recipe)
        {
            
            if (ModelState.IsValid)
            {
                recipe.RecipeId = Guid.NewGuid();
                recipeDb.Recipes.Add(recipe);
                recipeDb.SaveChanges();
                return RedirectToAction("AddToMenu");
            }

            return View(recipe);
        }

        // GET: Drinks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinks drinks = drinksDb.Drinks.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DrinkName,CoffeeBeans,Sugar,Milk")] Drinks drinks)
        {
            if (ModelState.IsValid)
            {
                drinksDb.Entry(drinks).State = EntityState.Modified;
                drinksDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drinks);
        }

        // GET: Drinks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinks drinks = drinksDb.Drinks.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Drinks drinks = drinksDb.Drinks.Find(id);
            drinksDb.Drinks.Remove(drinks);
            drinksDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                drinksDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
