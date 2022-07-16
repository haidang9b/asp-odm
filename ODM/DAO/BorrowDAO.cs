using ODM.Models;
using ODM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODM.DAO
{
    public class BorrowDAO
    {
        private static BorrowDAO instance;

        private ManagementContext db = new ManagementContext();
        public static BorrowDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BorrowDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public int Add(Borrow borrow)
        {
            db.borrows.Add(borrow);
            return db.SaveChanges();
        }

        public List<BorrowViewModel> GetListBorrow()
        {
            List<BorrowViewModel> rs = new List<BorrowViewModel>();
            var query = from b in db.borrows
                        join u in db.users on b.user_id equals u.id
                        join pr in db.products on b.product_id equals pr.id
                        select new
                        {
                            id = b.id,
                            username = u.username,
                            fullname = u.fullName,
                            idProduct = pr.id,
                            nameProduct = pr.name,
                            timeReceive = b.timeReceive,
                            timeReturn = b.timeReturn,
                            isReturn = b.isReturn

                        };
            foreach (var item in query)
            {
                rs.Add(new BorrowViewModel { id = item.id, 
                    username = item.username, 
                    fullname = item.fullname,
                    idProduct = item.idProduct, 
                    nameProduct = item.nameProduct, 
                    isReturn = item.isReturn, 
                    timeReceive = item.timeReceive, 
                    timeReturn=item.timeReturn });
            }
            return rs;
        }

        public Borrow GetBorrowByIDUsername(int id, string username)
        {
            var user = AccountDAO.Instance.GetByUsername(username);

            return db.borrows.FirstOrDefault(br => br.id == id && br.user_id == user.id);
        }

        public int ReturnProduct(int id_user, int id_product)
        {
            
            var old = db.borrows.FirstOrDefault(br => br.isReturn == false && br.product_id == id_product && id_user == br.user_id);
            var timeNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            old.isReturn = true;
            
            old.timeReturn = timeNow;
            return db.SaveChanges();
        }
    }
}