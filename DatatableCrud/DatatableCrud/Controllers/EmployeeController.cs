using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatatableCrud.Models;

namespace DatatableCrud.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                List<Employee> emplist = db.Employee.ToList<Employee>();
                return Json(new { data = emplist}, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                using (DatatableCRUDEntities db = new DatatableCRUDEntities())
                {
                    return View(db.Employee.Where(x => x.Id == id).FirstOrDefault());
                }
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                if (emp.Id == 0)
                {
                    db.Employee.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                Employee emp = db.Employee.Where(x => x.Id == id).FirstOrDefault();
                db.Employee.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Delete Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}