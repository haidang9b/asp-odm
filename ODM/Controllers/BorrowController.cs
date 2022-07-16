using ODM.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.Controllers
{
    public class BorrowController : Controller
    {
        // GET: Borrow
        [Authorize]
        public ActionResult Index()
        {
            var brws = BorrowDAO.Instance.GetListBorrow();
            brws.Reverse();
            return View(brws);
        }
    }
}