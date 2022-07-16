namespace ODM.Migrations
{
    using ODM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ODM.Models.ManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ODM.Models.ManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.roles.AddOrUpdate(new Role { id = "admin", name = "Quản lý" });
            context.roles.AddOrUpdate(new Role { id = "user", name = "Người dùng" });

            //context.users.AddOrUpdate(new User { id = 2, username = "admin", fullName = "Phan Hải Đăng", password = "$2a$11$RB8cIlIu5MhZfJ.gC8T2fueRZNdRA1EioxWAAdFeQSwhTuswi8IQ6", role_id = "admin", gender = true, birthday = "2000-01-04", email = "", avatar = "", started = "2020-01-01" });
        }
    }
}
