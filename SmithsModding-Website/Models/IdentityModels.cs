using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace SmithsModding_Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>());

            //Add the claims for the user based of its roles.
            foreach(IdentityUserRole userrole in this.Roles)
            {
                ApplicationRole role = await roleManager.FindByIdAsync(userrole.RoleId);
                
                foreach(AspNetRolesPermissions rolePermission in role.RolePermissions)
                {
                    userIdentity.AddClaim(new Claim(rolePermission.Permission.ClaimID, "true"));
                }
            }

            return userIdentity;
        }

        public ICollection<NewsItem> getNews { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ICollection<AspNetRolesPermissions> RolePermissions { get; set; }
    }

    public class AspNetPermission
    {

        public string Id { get; set; }

        [Required]
        public string ClaimID { get; set; }

        [Required]
        public ApplicationUser LastEditor { get; set; }

        [Required]
        public DateTime LastEditedOn { get; set; }
    }

    public class AspNetRolesPermissions
    {
        public string Id { get; set; }

        [Required]
        public ApplicationRole Role { get; set; }

        [Required]
        public AspNetPermission Permission { get; set; }

        [Required]
        public ApplicationUser AddedBy { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }
    }

}