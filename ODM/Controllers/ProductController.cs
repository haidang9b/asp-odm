using ODM.DAO;
using ODM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize(Roles = "admin")]
        public ActionResult Index(int category = 0)
        {
            var products = ProductDAO.Instance.GetList();
            if(category != 0)
            {
                products = ProductDAO.Instance.GetListByCategory(category);
            }
            products.Reverse();
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var item = ProductDAO.Instance.GetByID(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Product/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var selectID = CategoryDAO.Instance.GetSelectList();
            ViewBag.category_id = selectID;
            ViewData["category_id"] = selectID;
            return View();
        }

        // POST: Product/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rand = DateTime.Now.ToString("hhmmssddMMyyyy");
                    string rootPath = Server.MapPath("~/ImagesUpload");
                    
                    string path = Path.Combine(Server.MapPath("~/ImagesUpload") , rand + Path.GetFileName(file.FileName).ToLower());
                    file.SaveAs(path);
                    /*System.IO.File.Move(path, "~/ImagesUpload/" + rand + file.FileName);*/
                    product.image = "/ImagesUpload/" + rand+ file.FileName;
                    ProductDAO.Instance.Add(product);
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var selectID = CategoryDAO.Instance.GetList();
            ViewBag.Category = new SelectList(selectID, "id", "name");
            try
            {
                var item = ProductDAO.Instance.GetByID(id);
                if(item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            catch
            {

                return HttpNotFound();
            }
           
        }

        // POST: Product/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            var selectID = CategoryDAO.Instance.GetList();
            ViewBag.Category = new SelectList(selectID, "id", "name");
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Information of product is invalid";
                    return View(product);
                }
                product.id = id;
                if(ProductDAO.Instance.Edit(product) > 0)
                {
                    ViewBag.Success = "Update information is successfully";
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here
                ViewBag.Error = "Update information is failed";
                return View(product);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var item = ProductDAO.Instance.GetByID(id);
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Product/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                if (ProductDAO.Instance.Delete(id) > 0)
                {
                    return RedirectToAction("Index");
                }
                // TODO: Add delete logic here

                return HttpNotFound();
            }
            catch
            {
                return View();
            }
        }
        
       
        

        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult Provide(int idProduct, string username)
        {
            var product = ProductDAO.Instance.GetByID(idProduct);
            var user = AccountDAO.Instance.GetByUsername(username);

            if(product == null)
            {
                return Json(new { success = false, message = "Not exist No. Product"});
            }
            if (user == null)
            {
                return Json(new { success = false, message = "Not exist this user" });
            }

            if (product.status)
            {
                return Json(new { success = false, message = "this product is deployed, please choose other product" });
            }

            var existProduct = RequestDAO.Instance.GetExistProduct(product.id, false);

            if(existProduct != null)
            {
                return Json(new { success = false, message = "Sản phẩm này đã được khởi tạo rồi" });
            }

            var exist = RequestDAO.Instance.getExistRequest(user.id, product.id, false);
            if (exist != null)
            {
                return Json(new { success = false, message = "Yêu cầu này đã được khởi tạo rồi" });
            }

            var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            var item = new Request { user_id = user.id, product_id = product.id, timeRequest = timeNow, isReturn = false };

            if(RequestDAO.Instance.Add(item) > 0)
            {
                return Json(new { success = true, message = "Yêu cầu cấp thiết bị đã được tạo" });
            }
            return Json(new { success = false, message = "Không thể tạo yêu cầu" });
        }



        
    }
}
