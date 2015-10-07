using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tingle_WebForms.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<FormContext>
    {
        protected override void Seed(FormContext context)
        {
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP100", Description = "Mill Error", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP200", Description = "Customer Error", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP300", Description = "Tingle Error", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP400", Description = "Install Date", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP500", Description = "Can't wait on production date", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP777", Description = "General", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP800", Description = "Direct Order", Status = 1 });
            context.ExpediteCodes.Add(new ExpediteCode { Code = "EXP911", Description = "Immediate Attn", Status = 1 });

            context.TForms.Add(new TForm { FormName = "Expedited Order Form", Status = 1, FormCreator = "Admin", Notes = "Expedited Order Form Notes", FormUrl="ExpeditedOrderForm.aspx" });

            context.Statuses.Add(new Status { StatusText = "In Progress" });
            context.Statuses.Add(new Status { StatusText = "On Hold" });
            context.Statuses.Add(new Status { StatusText = "Completed" });

            context.UserRoles.Add(new UserRoles { RoleName = "User", RoleDescription = "User can submit forms." });
            context.UserRoles.Add(new UserRoles { RoleName = "ReportsUser", RoleDescription = "User can view reports, but not edit them." });
            context.UserRoles.Add(new UserRoles { RoleName = "ReportsAdmin", RoleDescription = "User can view and edit reports." });
            context.UserRoles.Add(new UserRoles { RoleName = "SuperUser", RoleDescription = "User has full administrative rights." });

            UserRoles adminRole = context.UserRoles.Where(ur => ur.RoleName == "SuperUser").FirstOrDefault();

            context.SystemUsers.Add( new SystemUsers { UserName = "Bob-PC\\Bob", UserRole = adminRole, DisplayName = "Bob" });
            context.SystemUsers.Add(new SystemUsers { UserName = "wctingle\\ledfordl", UserRole = adminRole, DisplayName = "Larry Ledford" });
            context.SystemUsers.Add(new SystemUsers { UserName = "wctingle\\ledforda", UserRole = adminRole, DisplayName = "Andrew Ledford" });

            base.Seed(context);
        }
        
    }


}
