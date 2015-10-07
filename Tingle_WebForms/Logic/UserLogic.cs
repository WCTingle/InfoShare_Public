using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;

namespace Tingle_WebForms.Logic
{
    public class UserLogic
    {
        FormContext ctx = new FormContext();

        public SystemUsers GetCurrentUser(System.Security.Principal.IPrincipal user)
        {
            try
            {
                SystemUsers sUser = ctx.SystemUsers.Where(u => u.UserName == user.Identity.Name).FirstOrDefault();

                if (sUser != null)
                {
                    return sUser;
                }
                else
                {
                    UserRoles uRole = ctx.UserRoles.Where(ur => ur.RoleName == "User").FirstOrDefault();

                    ctx.SystemUsers.Add(new SystemUsers { 
                        UserName = user.Identity.Name, 
                        DisplayName = user.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1), 
                        UserRole = uRole,
                        EmailAddress = user.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1),
                        Points = 0,
                        Greeting = "",
                        UserStatus = ctx.UserStatuses.FirstOrDefault(x => x.StatusText == "Online")
                    });
                    
                    ctx.SaveChanges();

                    SystemUsers newUser = ctx.SystemUsers.Where(u => u.UserName == user.Identity.Name).FirstOrDefault();
                    


                    if (newUser != null)
                    {

                        return newUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public bool HasAccess(SystemUsers user, string formName)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    return ctx.FormPermissions.Any(x => x.Enabled == true && x.FormName == formName && x.UserRole.UserRoleId == user.UserRole.UserRoleId);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool HasBeenAssigned(SystemUsers user, Int32 formId, string formName)
        {
            try
            {
                using (FormContext context = new FormContext())
                {
                    return context.UserAssignments.Any(x => x.User.SystemUserID == user.SystemUserID && x.RelatedFormId == formId && x.Form.FormName == formName);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HasBeenRequested(SystemUsers user, Int32 formId, string formName)
        {
            try
            {
                using (FormContext context = new FormContext())
                {
                    return context.UserRequests.Any(x => x.User.SystemUserID == user.SystemUserID && x.RelatedFormId == formId && x.Form.FormName == formName);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}