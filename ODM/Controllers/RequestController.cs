using ODM.DAO;
using ODM.Models;
using ODM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        // GET: Request
        [HttpGet]
        public ActionResult Index()
        {
            var rs = RequestDAO.Instance.getListRequest();
            rs.Reverse();
            return View(rs);
        }

        [HttpPost]
        public JsonResult AcceptClient(int id)
        {
            try
            {
                var username = User.Identity.Name;

                var exist = RequestDAO.Instance.GetRequestByIDUsername(id, username);

                if (exist == null)
                {
                    return Json(new { success = false, message = "Yêu cầu này không tồn tại" });
                }
                RequestDAO.Instance.Complete(exist.id);
                var item = ProductDAO.Instance.SetStatus(exist.product_id, true);
                var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var newBorrow = new Borrow { user_id = exist.user_id, product_id = exist.product_id, timeReceive = timeNow, timeReturn = "", isReturn = false };
                if (BorrowDAO.Instance.Add(newBorrow) > 0)
                {
                    return Json(new { success = true, message = "Bạn đã chấp nhận yêu cầu" });
                }
                return Json(new { success = false, message = "Không thể chấp nhận yêu cầu này" });
            }
            catch
            {
                return Json(new { success = false, message = "Không thể chấp nhận yêu cầu này" });
            }
           
        }


        [HttpPost]
        public JsonResult CancelClient(int id)
        {
            try
            {
                var username = User.Identity.Name;
                var exist = RequestDAO.Instance.GetRequestByIDUsername(id, username);
                if (User.IsInRole("admin"))
                {
                    RequestDAO.Instance.declineRequest(id);
                    return Json(new { success = true, message = "Yêu cầu này đã xóa" });
                }
                if (exist == null)
                {
                    return Json(new { success = false, message = "Yêu cầu này không tồn tại" });
                }
               
                RequestDAO.Instance.declineRequest(exist.id);
                return Json(new { success = true, message = "Yêu cầu này đã xóa" });
            }
            catch
            {
                return Json(new { success = false, message = "Không thể hủy yêu cầu này" });
            }
        }

        [HttpPost]
        public JsonResult ReturnClient(int id)
        {
            try
            {
                var username = User.Identity.Name;

                var borrow = BorrowDAO.Instance.GetBorrowByIDUsername(id, username);
                
                
                if(borrow == null)
                {
                    return Json(new { success = false, message = "Mã mượn không hợp lệ" });
                }

                var exist = RequestDAO.Instance.GetByCondition(borrow.user_id, borrow.product_id, true);
                if (exist != null)
                {
                    return Json(new { success = false, message = "Yêu cầu trả này đã tồn tại, vui lòng chờ" });
                }
                var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var newRequest = new Request { user_id = borrow.user_id, product_id = borrow.product_id, isReturn = true, timeRequest = timeNow };
                RequestDAO.Instance.Add(newRequest);
                return Json(new { success = true, message = "Khởi tạo yêu cầu thành công" });
            }
            catch
            {
                return Json(new { success = false, message = "Không thể khởi tạo yêu cầu này" });
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        public JsonResult ReturnClientAccept(int id)
        {
            try
            {
                var exist = RequestDAO.Instance.GetRequestByID(id, true);
                if (exist == null)
                {
                    return Json(new { success = false, message = "Yêu cầu này không tồn tại" });
                }
                if(exist.timeCompletion != null)
                {
                    return Json(new { success = false, message = "Yêu cầu này đã hoàn thành rồi" });
                }
                RequestDAO.Instance.Complete(exist.id);
                var item = ProductDAO.Instance.SetStatus(exist.product_id, false);
                BorrowDAO.Instance.ReturnProduct(exist.user_id, exist.product_id);
                return Json(new { success = true, message = "Đã nhận thiết bị" });

            }
            catch
            {
                return Json(new { success = false, message = "Không thể chấp nhận yêu cầu này" });
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult History()
        {
            var requests = RequestDAO.Instance.GetRequestsComplete();
            List<RequestViewModel> rs = new List<RequestViewModel>();
            foreach(var item in requests)
            {
                if (User.IsInRole("user") && User.Identity.Name == item.user.username)
                {
                    rs.Add(new RequestViewModel { id = item.id, username = item.user.username , fullname = item.user.fullName, idProduct=item.product_id, nameProduct = item.product.name, isReturn = item.isReturn, timeRequest = item.timeRequest, timeCompletion = item.timeCompletion,state= item.state});
                }
                if (User.IsInRole("admin"))
                {
                    rs.Add(new RequestViewModel { id = item.id, username = item.user.username, fullname = item.user.fullName, idProduct = item.product_id, nameProduct = item.product.name, isReturn = item.isReturn, timeRequest = item.timeRequest, timeCompletion = item.timeCompletion, state =  item.state });
                }
            }
            rs.Reverse();
            return View(rs);
        }

    }
}