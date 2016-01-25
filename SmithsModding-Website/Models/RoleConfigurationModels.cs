using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmithsModding_Website.Models
{
    class RoleConfigurationModels
    {
        public IList<IdentityRole> Roles { get; set; }
    }
}
