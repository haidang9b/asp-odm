using ODM.Models;
using ODM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace ODM.DAO
{
    public class RequestDAO
    {
        private static RequestDAO instance;

        private ManagementContext db = new ManagementContext();
        public static RequestDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public int Add(Request request)
        {
            request.state = 0;
            db.requests.Add(request);
            return db.SaveChanges();
        }

        public int Complete(int id)
        {
            var item = db.requests.FirstOrDefault(i => i.id == id);
            var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            item.state = 1;
            item.timeCompletion = timeNow;
            return db.SaveChanges();

        }
        public int declineRequest(int idRequest)
        {
            try
            {
                var exist = db.requests.FirstOrDefault(r => r.id == idRequest);
                if (exist == null)
                {
                    return 0;
                }
                var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                exist.timeCompletion = timeNow;
                exist.state = -1;
                return db.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
        public int Remove(Request request)
        {
            db.requests.Remove(request);
            return db.SaveChanges();
        }

        public Request getExistRequest(int user, int product, bool isReturn)
        {
            return db.requests.FirstOrDefault(req => req.isReturn == isReturn && req.user_id == user && req.product_id == product && req.timeCompletion == null);
        }
        //kiem tra product co dc them vao request nao chua
        public Request GetExistProduct(int product, bool isReturn)
        {
            return db.requests.FirstOrDefault(req => req.product_id == product && req.isReturn == isReturn && req.timeCompletion == null);
        }

        public List<RequestViewModel> getListRequest()
        {
            List<RequestViewModel> rs = new List<RequestViewModel>();
            var query = from r in db.requests
                        join u in db.users on r.user_id equals u.id
                        join pr in db.products on r.product_id equals pr.id
                        select  new { 
                            id = r.id,
                            username = u.username,
                            fullname = u.fullName,
                            idProduct = pr.id,
                            nameProduct = pr.name,
                            timeRequest = r.timeRequest,
                            timeCompletion = r.timeCompletion,
                            isReturn = r.isReturn

                        };
            foreach(var item in query)
            {
                rs.Add(new RequestViewModel { id = item.id, username = item.username, fullname = item.fullname, idProduct = item.idProduct, nameProduct = item.nameProduct, isReturn = item.isReturn , timeRequest =item.timeRequest, timeCompletion=item.timeCompletion});
            }
            return rs;
        }

        public Request GetRequestByIDUsername(int id, string username)
        {
            var user = AccountDAO.Instance.GetByUsername(username);
            return db.requests.FirstOrDefault(r => r.id == id && r.user_id == user.id );
        }

        public Request GetRequestByID(int id, bool isReturn)
        {
            return db.requests.FirstOrDefault(r => r.id == id && r.isReturn == isReturn);
        }

        public Request GetByCondition(int user_id, int product_id, bool isReturn)
        {
            return db.requests.FirstOrDefault(r => r.user_id == user_id && r.product_id == product_id && r.isReturn == isReturn && r.timeCompletion == null);
        }

        public List<Request> GetRequestsComplete()
        {
            return db.requests.Include("User").Include("Product").Where(r => r.timeCompletion != null).ToList();
        }
    }
}