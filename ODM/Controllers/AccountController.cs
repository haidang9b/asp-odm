using ODM.DAO;
using ODM.Models;
using ODM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ODM.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Login");

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // if is admin redirect to product else is rturen to borrow list
                if (User.IsInRole("admin"))
                    return Redirect("/Product");
                else
                    return Redirect("/Borrow/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exist = AccountDAO.Instance.GetByUsername(user.username);
                    if (exist == null)
                    {
                        ViewBag.Message = "Tài khoản hoặc mật khẩu không chính xác";
                        return View();
                    }
                    if (BCrypt.Net.BCrypt.Verify(user.password, exist.password) == false)
                    {
                        ViewBag.Message = "Tài khoản hoặc mât khẩu không chính xác";
                        return View();
                    }
                    else
                    {
                        Session["FullName"] = exist.fullName;
                        FormsAuthentication.SetAuthCookie(user.username, false);
                        var ticket = new FormsAuthenticationTicket(1, user.username, DateTime.Now
                            , DateTime.Now.AddDays(7), false, "");
                        var encrypt = FormsAuthentication.Encrypt(ticket);

                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                        HttpContext.Response.Cookies.Add(authCookie);


                        return Redirect("/Account/Login");
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Đăng nhập có lỗi";
                return View();
            }
        }


        /* 
         * [HttpGet]
         * public ActionResult Register()
              {
                  return View();
              }

              [HttpPost]
              [AllowAnonymous]
              [ValidateAntiForgeryToken]
              public ActionResult Register(User user)
              {
                  try
                  {
                      if (User.Identity.IsAuthenticated)
                      {
                          return Redirect("/Product");
                      }
                      if (ModelState.IsValid)
                      {
                          var isExist = AccountDAO.Instance.GetByUsername(user.username);
                          if (isExist == null)
                          {

                              user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                              user.role_id = "user";
                              if (AccountDAO.Instance.Add(user) > 0)
                              {
                                  ViewBag.Success = "Bạn đã đăng kí thành công";
                                  return RedirectToAction("/");
                              }

                          }
                          else
                          {
                              ViewBag.Message = "Tài khoản này đã tồn tại, vui lòng nhập lại thông tin";
                              user.password = "";
                              user.confirmPassword = "";
                              return View(user);
                          }
                      }
                      ViewBag.Message = "Có lỗi gì đó";
                      return View();

                  }
                  catch (Exception e)
                  {
                      return View();
                  }
              }*/
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var username = User.Identity.Name;
            var data = AccountDAO.Instance.GetByUsername(username);
            if (data == null)
            {
                return RedirectToAction("/Logout");
            }
            var rs = new ChangePasswordViewModel();
            return View(rs);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel value)
        {
            try
            {
                var username = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    var exist = AccountDAO.Instance.GetByUsername(username);
                    if (BCrypt.Net.BCrypt.Verify(value.oldPassword, exist.password) == false)
                    {
                        ViewBag.ErrorMessage = "Old password is invalid";
                        return View();
                    }

                    var hashed = BCrypt.Net.BCrypt.HashPassword(value.password);
                    if (AccountDAO.Instance.ChangePass(username, hashed) > 0)
                    {
                        ViewBag.SuccessMessage = "Change your password is successfully";

                        return View(new ChangePasswordViewModel());
                    }

                }
                ViewBag.ErrorMessage = "Information enter is invalid";
                return View();
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            var selectID = AccountDAO.Instance.GetRoles();
            ViewBag.Role = new SelectList(selectID, "id", "name");
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(User user, HttpPostedFileBase file)
        {
            try
            {
                var selectID = AccountDAO.Instance.GetRoles();
                ViewBag.Role = new SelectList(selectID, "id", "name");

                if (ModelState.IsValid)
                {
                    var isExist = AccountDAO.Instance.GetByUsername(user.username);
                    var hasEmail = AccountDAO.Instance.GetUserByEmail(user.email);
                    if (hasEmail != null)
                    {
                        ViewBag.ErrorMessage = "Email is existed, please enter another email";
                        return View(user);
                    }
                    if (IsValidEmailAddress(user.email) == false)
                    {
                        ViewBag.ErrorMessage = "Email is invalid, please enter another email";
                        return View(user);
                    }
                    if (isExist == null)
                    {
                        var fileName = "";
                        if (file != null)
                        {
                            var rand = DateTime.Now.ToString("hhmmssddMMyyyy");
                            string rootPath = Server.MapPath("~/ImagesUpload");

                            string path = Path.Combine(Server.MapPath("~/ImagesUpload"), rand + "avt" + Path.GetFileName(file.FileName).ToLower());
                            file.SaveAs(path);
                            fileName = "/ImagesUpload/" + rand + "avt" + file.FileName;
                        }
                        user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                        user.avatar = fileName.Length > 0 ? fileName : null;
                        if (AccountDAO.Instance.Add(user) > 0)
                        {
                            ViewBag.SuccessMessage = "Thêm tài khoản thành công";
                            return RedirectToAction("ListUser");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "This account is exist, please contact admin for provide password";
                        user.password = "";
                        user.confirmPassword = "";
                        return View(user);
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.ErrorMessage = "Có lỗi";
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ListUser()
        {
            var users = AccountDAO.Instance.GetUsers();
            users.Reverse();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(int id)
        {
            try
            {
                var selectID = AccountDAO.Instance.GetRoles();
                ViewBag.Role = new SelectList(selectID, "id", "name");
                var item = AccountDAO.Instance.GetUserByID(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                var user = new UpdatePasswordViewModel { id = item.id, fullName = item.fullName, role_id = item.role_id, password = "", username = item.username, confirmPassword = "" };
                return View(user);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(int id, UpdatePasswordViewModel user)
        {
            var selectID = AccountDAO.Instance.GetRoles();
            ViewBag.Role = new SelectList(selectID, "id", "name");
            try
            {
                var item = AccountDAO.Instance.GetUserByID(id);
                if (ModelState.IsValid)
                {
                    var hashed = BCrypt.Net.BCrypt.HashPassword(user.password);
                    if (AccountDAO.Instance.ChangePassword(id, hashed, user.role_id) > 0)
                    {
                        ViewBag.Success = "Update information is successfully";
                        return View(user);
                    }
                }
                ViewBag.Error = "Information is invalid";
                return View(user);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                var selectID = AccountDAO.Instance.GetRoles();
                ViewBag.Role = new SelectList(selectID, "id", "name");
                var item = AccountDAO.Instance.GetUserByID(id);
                var user = new EditUserViewModel { id = item.id, birthday = item.birthday, fullName = item.fullName, gender = item.gender, role_id = item.role_id, started = item.started };
                return View(user);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, EditUserViewModel user)
        {
            try
            {
                user.id = id;
                var selectID = AccountDAO.Instance.GetRoles();
                ViewBag.Role = new SelectList(selectID, "id", "name");
                if (ModelState.IsValid)
                {
                    if (AccountDAO.Instance.EditUser(user) > 0)
                    {
                        ViewBag.Success = "Cập nhật thông tin thành công";
                        return View(user);
                    }
                }
                ViewBag.Error = "Thông tin không hợp lệ";
                return View(user);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult Info()
        {
            try
            {
                var username = User.Identity.Name;
                var item = AccountDAO.Instance.GetByUsername(username);
                return View(item);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Profile(int id)
        {
            try
            {
                var item = AccountDAO.Instance.GetUserByID(id);
                return View(item);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }



        [HttpPost]
        [Authorize]
        public JsonResult UpdateAvatar(HttpPostedFileBase file)
        {
            try
            {
                var username = User.Identity.Name;
                var rand = DateTime.Now.ToString("hhmmssddMMyyyy");

                string path = Path.Combine(Server.MapPath("~/ImagesUpload"), rand + "avt" + Path.GetFileName(file.FileName).ToLower());
                file.SaveAs(path);
                var fileName = "/ImagesUpload/" + rand + "avt" + file.FileName;

                if (AccountDAO.Instance.ChangeImage(username, fileName) > 0)
                {
                    return Json(new { success = true, message = "Thay đổi ảnh đại diện thành công" });
                }
                return Json(new { success = false, message = "Không thể thay đổi ảnh đại diện lên" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Không thể upload ảnh lên" });
            }
        }

        [HttpGet]
        [Authorize]
        public JsonResult Filter(string keyword)
        {

            var list = AccountDAO.Instance.FilterUser("");
            if (!String.IsNullOrEmpty(keyword))
            {
                list = AccountDAO.Instance.FilterUser(keyword);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var item = AccountDAO.Instance.GetUserByID(id);
                return View(item);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(int id, User user)
        {
            try
            {
                if (AccountDAO.Instance.Delete(id) > 0)
                {
                    return RedirectToAction("ListUser");
                }
                else
                {
                    return View(user);
                }

            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        private static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}