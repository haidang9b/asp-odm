using ODM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODM.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        private ManagementContext db = new ManagementContext();
        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public List<Product> GetList()
        {
            return db.products.ToList<Product>();
        }

        public Product GetByID(int id)
        {
            return db.products.Include("Category").FirstOrDefault(p => p.id == id);
        }

        public List<Product> GetListByCategory(int idCategory)
        {
            return db.products.Include("Category").Where(p => p.category_id == idCategory).ToList<Product>();
        }

        public int Add(Product product)
        {
            product.installDay = new DateTime().ToString();
            db.products.Add(product);
            return db.SaveChanges();
        }

        public int Edit(Product product)
        {
            var oldItem = db.products.FirstOrDefault(p => p.id == product.id);
            if (oldItem == null)
            {
                return 0;
            }
            oldItem.name = product.name;
            oldItem.description = product.description;
            /*oldItem.image = product.image;*/
            oldItem.category_id = product.category_id;
            return db.SaveChanges();
        }

        public int Delete(int idRemove)
        {
            var item = db.products.FirstOrDefault(p => p.id == idRemove);
            if (item == null)
            {
                return 0;
            }
            db.products.Remove(item);
            return db.SaveChanges();
        }

        public int SetStatus(int id, bool status)
        {
            var oldItem = db.products.FirstOrDefault(p => p.id == id);
            if (oldItem == null)
            {
                return 0;
            }
            oldItem.status = status;
            return db.SaveChanges();
        }
    }
}