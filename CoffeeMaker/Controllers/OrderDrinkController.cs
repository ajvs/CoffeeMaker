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
using CoffeeMaker.Models;
using Newtonsoft.Json;

namespace CoffeeMaker.Controllers
{
    public class OrderDrinkController : Controller
    {
        private OrdersDbContext ordersDb = new OrdersDbContext();
        private DrinksDbContext drinksDb = new DrinksDbContext();
        private StocksDbContext stocksDb = new StocksDbContext();
        private RecipeDbContext recipeDb = new RecipeDbContext();

        // GET: OrderDrink
        public ActionResult ViewMenu()
        {
            return View(drinksDb.Drinks.ToList());
        }

        public ActionResult OrderHistory()
        {
            return View(ordersDb.Orders.ToList());
        }

        public ActionResult ResetOrders()
        {
            bool success = true;
            try
            {
                ordersDb.Database.ExecuteSqlCommand("Truncate Table [dbo].[Orders]");
            }
            catch (Exception)
            {
                success = false;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrderHistory()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var drink in drinksDb.Drinks)
            {
                var orders = ordersDb.Orders.Where(o => o.DrinkOrderedGuid.Equals(drink.Id));
                int total = 0;
                if (orders.Count() > 0)
                {
                    total = orders.Sum(x => x.Quantity);
                }
                dataPoints.Add(new DataPoint(drink.DrinkName, total));
            }
            return Json(JsonConvert.SerializeObject(dataPoints), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlaceOrder(Orders order)
        {
            order.Id = Guid.NewGuid();
            order.DateOrdered = DateTime.UtcNow;
            ordersDb.Orders.Add(order);
            ordersDb.SaveChanges();
            bool success = true;
            string message = "Order Success! Enjoy your coffee.";

            try
            {
                var drink = drinksDb.Drinks.Find(order.DrinkOrderedGuid);
                var recipe = recipeDb.Recipes.Where(x => x.DrinkId.Equals(drink.Id));
                foreach (var ingredient in recipe)
                {
                    var updateStock = stocksDb.Stocks.Find(ingredient.StockId);
                    int requiredStocks = ingredient.Quantity * order.Quantity;
                    if (updateStock.Quantity < requiredStocks)
                    {
                        success = false;
                        message = "Sorry we have insufficient stocks. Try ordering less?";
                        break;
                    }
                    else
                    {
                        updateStock.Quantity -= requiredStocks;
                        stocksDb.Entry(updateStock).State = EntityState.Modified;
                        stocksDb.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = "Error encountered" + ex.Message;
            }

            return Json(new { message = message, success = success }, JsonRequestBehavior.AllowGet);
        }

        // GET: OrderDrink/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders drinks = ordersDb.Orders.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // GET: OrderDrink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDrink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CoffeeBeans,Sugar,Milk")] Orders order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                ordersDb.Orders.Add(order);
                ordersDb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: OrderDrink/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders drinks = ordersDb.Orders.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // POST: OrderDrink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CoffeeBeans,Sugar,Milk")] Orders order)
        {
            if (ModelState.IsValid)
            {
                ordersDb.Entry(order).State = EntityState.Modified;
                ordersDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: OrderDrink/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders drinks = ordersDb.Orders.Find(id);
            if (drinks == null)
            {
                return HttpNotFound();
            }
            return View(drinks);
        }

        // POST: OrderDrink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Orders drinks = ordersDb.Orders.Find(id);
            ordersDb.Orders.Remove(drinks);
            ordersDb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ordersDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
