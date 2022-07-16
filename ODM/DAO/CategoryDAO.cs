using ODM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        private ManagementContext db = new ManagementContext();
        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public List<Category> GetList()
        {
            return db.categories.ToList<Category>();
        }

        public Category GetByID(int id)
        {
            return db.categories.FirstOrDefault(c => c.id == id);
        }

        public int Add(Category category)
        {
            db.categories.Add(category);
            return db.SaveChanges();
        }

        public int Delete(int idRemove)
        {
            var item = db.categories.FirstOrDefault(c => c.id == idRemove);
            if (item == null)
            {
                return 0;
            }
            db.categories.Remove(item);
            return db.SaveChanges();
        }

        public int Edit(Category category)
        {
            var oldItem = db.categories.FirstOrDefault(c => c.id == category.id);
            if (oldItem == null)
            {
                return 0;
            }
            oldItem.name = category.name;
            return db.SaveChanges();
        }

        public SelectList GetSelectList()
        {
            return new SelectList(db.categories, "id", "name");
        }
    }
}