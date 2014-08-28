using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Security.Helpers
{
    internal static class SecurityUtils
    {
        private const string PermissionDelimiter = ".";

        public static string CreatePermissionAuthString(string type, string accessMode)
        {
            return type + PermissionDelimiter + accessMode;
        }

        public static TimeSpan CreateTimespan(int seconds)
        {
            int secs = seconds % 60;
            seconds /= 60;
            int mins = seconds % 60;
            seconds /= 60;
            int hours = seconds % 24;
            seconds /= 24;
            int days = seconds;

            return new TimeSpan(days, hours, mins, secs);
        }
    }
}
