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
    public class StocksController : Controller
    {
        private StocksDbContext db = new StocksDbContext();

        // GET: Stocks
        public ActionResult Stocks()
        {
            return View(db.Stocks.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocks stocks = db.Stocks.Find(id);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            return View(stocks);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item,Quantity")] Stocks stocks)
        {
            if (ModelState.IsValid)
            {
                stocks.Id = Guid.NewGuid();
                db.Stocks.Add(stocks);
                db.SaveChanges();
                return RedirectToAction("Stocks");
            }

            return View(stocks);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocks stocks = db.Stocks.Find(id);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            return View(stocks);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Item,Quantity")] Stocks stocks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stocks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stocks);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocks stocks = db.Stocks.Find(id);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            return View(stocks);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stocks stocks = db.Stocks.Find(id);
            db.Stocks.Remove(stocks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowStockChart()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            return View();
        }
        public ActionResult GetStocks()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var stock in db.Stocks)
            {
                dataPoints.Add(new DataPoint(stock.Item, stock.Quantity));
            }

            return Json(JsonConvert.SerializeObject(dataPoints), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetStocks()
        {
            bool success = true;
            try
            {
                foreach (var stock in db.Stocks)
                {
                    stock.Quantity = 15;
                    db.Entry(stock).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                success = false;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
