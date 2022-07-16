using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ODM.Models
{
    public class ManagementContext: DbContext
    {
        /*
            enable-migrations
            add-migration "name_migration"
            update-database
         */
        public ManagementContext() : base("LibraryManagementConnectionString") {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Request> requests { get; set; }

        public DbSet<Borrow> borrows { get; set; }
    }
}