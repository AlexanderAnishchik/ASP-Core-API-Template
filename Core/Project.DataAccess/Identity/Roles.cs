using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Identity
{
    public static class Roles
    {
        private static readonly string[] roles = { "Admin","Basic" };
        public static String Admin { get { return roles[0]; } }
        public static String Basic { get { return roles[1]; } }
        public static IEnumerable<string> All { get { return roles; } }
    }
}
