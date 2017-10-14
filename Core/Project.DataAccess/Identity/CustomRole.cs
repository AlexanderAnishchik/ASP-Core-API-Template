using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Identity
{
    public class CustomRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
