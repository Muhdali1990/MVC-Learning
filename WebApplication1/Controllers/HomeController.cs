using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


        ObjectCache cache = MemoryCache.Default;
        List<Customer> Cust;
        public HomeController()
        {
            Cust = cache["cust"] as List<Customer>;
            if (Cust == null)
            {
                Cust = new List<Customer>();
            }
        }
        public void SaveCache()
        {
            cache["cust"] = Cust;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperProperty = "This Is My First WEb App";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ViewCustomerDetails( Customer PostedData)
        {
            Customer cst = new Customer();
            cst.Id = Guid.NewGuid().ToString();
            cst.Name = PostedData.Name;
            cst.Telephone = PostedData.Telephone;
            return View(cst);

        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer Custr)
        {
            Custr.Id = Guid.NewGuid().ToString();
            Cust.Add(Custr);
            SaveCache();
            return RedirectToAction("CustomerList");
            
        }
        public ActionResult CustomerList()
        {
           
            return View(Cust);
        }
    }
}