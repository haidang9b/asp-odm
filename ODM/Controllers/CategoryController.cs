using ODM.DAO;
using ODM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.Controllers
{
    [Authorize]
    
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var categories = CategoryDAO.Instance.GetList();
            categories.Reverse();
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var item = CategoryDAO.Instance.GetByID(id);
            if(item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Category/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (CategoryDAO.Instance.Add(category) > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.Message = "Thêm loại thiết bị thất bại";
                // TODO: Add insert logic here
                return View(category);
                
            }
            catch
            {
                ViewBag.Message = "Thêm loại thiết bị có lỗi";
                return View(category);
            }
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var item = CategoryDAO.Instance.GetByID(id);
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                category.id = id;
                if (ModelState.IsValid)
                {
                    if (CategoryDAO.Instance.Edit(category) > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                // TODO: Add update logic here
                ViewBag.Message = "Cập nhật loại thiết bị thất bại";
                return View(category);
                
            }
            catch
            {
                ViewBag.Message = "Cập nhật loại thiết bị có lỗi";
                return View(category);
            }
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            
            var item = CategoryDAO.Instance.GetByID(id);
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
