using ODM.Models;
using ODM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODM.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        private ManagementContext db = new ManagementContext();
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public User GetByUsername(string username)
        {
            return db.users.FirstOrDefault(u => u.username == username);
        }

        public int Add(User user)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            db.users.Add(user);
            return db.SaveChanges();
        }



        public string GetRoleByUsername(string username)
        {
            var rs = db.users.FirstOrDefault(u => u.username == username).role_id;
            return rs;
        }

        public int ChangePass(string username, string newPass)
        {
            var oldItem = db.users.FirstOrDefault(u => u.username == username);
            oldItem.password = newPass;
            db.Configuration.ValidateOnSaveEnabled = false;
            return db.SaveChanges();
        }

        public int ChangeImage(string username, string path)
        {
            var oldItem = db.users.FirstOrDefault(u => u.username == username);
            oldItem.avatar = path;
            db.Configuration.ValidateOnSaveEnabled = false;
            return db.SaveChanges();
        }

        public int ChangePassword(int id, string newPass, string role)
        {
            var oldItem = db.users.FirstOrDefault(u => u.id == id);
            oldItem.password = newPass;
            /*oldItem.role_id = role;*/
            db.Configuration.ValidateOnSaveEnabled = false;
            return db.SaveChanges();
        }
        
        public List<Role> GetRoles()
        {
            return db.roles.ToList(); 
        }

        public List<User> GetUsers()
        {
            return db.users.Include("Role").ToList();
        }

        public User GetUserByID(int id)
        {
            return db.users.FirstOrDefault(u => u.id == id);
        }

        public User GetUserByEmail(string input)
        {
            return db.users.FirstOrDefault(u => u.email == input);
        }

        public List<AccountFilerViewModel> FilterUser(string keyword)
        {
            List<AccountFilerViewModel> rs = new List<AccountFilerViewModel>();
            List<User> tmp = db.users.Where(u => u.email.Contains(keyword) || u.username.Contains(keyword)).ToList();
            foreach(var i in tmp)
            {
                rs.Add(new AccountFilerViewModel { fullName = i.fullName, username =  i.username, email = i.email });
            }
            return rs;
        }

        public int EditUser(EditUserViewModel user)
        {
            var exist = db.users.FirstOrDefault(u => u.id == user.id);
            exist.fullName = user.fullName;
            exist.birthday = user.birthday;
            exist.gender = user.gender;
            exist.role_id = user.role_id;
            db.Configuration.ValidateOnSaveEnabled = false;
            return db.SaveChanges();
        }

        public int Delete(int id)
        {
            var exist = db.users.FirstOrDefault(u => u.id == id);
            db.users.Remove(exist);
            return db.SaveChanges();
        }
    }
}