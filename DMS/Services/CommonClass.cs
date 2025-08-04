using DMS.Data;
using DMS.Models;

namespace DMS.Services
{
    public class CommonClass
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _iconfig;
        public CommonClass(ApplicationDbContext db, IConfiguration iconfig)
        {
            _db = db;
            _iconfig = iconfig;
        }
        public string GetPermissions()
        {
            return "ClockInOut,Dashboard,Company,Roles Management,Lead Management,Client Tags,Email Templates,Accounts,Unit,Manage Users,Contract,Budget,Employees,Crews,Production,Material Catalogue,Clients,Vendors,Projects,Accepted Projects,Job Costing,TimeSheet,Scheduling,Courses Management,Enrollment";
        }
    }
    public static class RolePermissionsHelper
    {
        public static bool HasPermission(string roleId, string moduleName, string permissionType, ApplicationDbContext _db)
        {
            var permissions = _db.RolePermissions.Where(rp => rp.RoleId == roleId).ToList(); //  Fetch from DB first, then process in memory

            return permissions.Any(r =>
                r.ModelloName.Split(',').Contains(moduleName) && //  Check if module exists in the list
                GetBooleanProperty(r, permissionType) //  Use reflection to access the boolean property
            );
        }

        private static bool GetBooleanProperty(RolePermissions permission, string propertyName)
        {
            var property = typeof(RolePermissions).GetProperty(propertyName);
            return property != null && property.PropertyType == typeof(bool) && (bool)property.GetValue(permission);
        }
    }

    //public static class SidebarMenuHelper
    //{
    //    public static List<string> GetSidebarMenuNames()
    //    {
    //        return new List<string>
    //        {
    //            "Dashboard",
    //            "User",
    //            "Projects",
    //            "PaymentPlans",


    //        };
    //    }
    //}
    public static class SidebarMenuHelper
    {
        public static List<string> GetSidebarMenuNames()
        {
            return new List<string>
            {
                "Dashboard",
                "Accounts",
                "Projects",
                "PaymentPlns",
                "Installments",
                "RolePermisions"
            };



        }
    }
}
