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

                //public string GetPermissions()
                //{
                //    return "ClockInOut,Dashboard,Company,Roles Management,Lead Management,Client Tags,Email Templates,Accounts,Unit,Manage Users,Contract,Budget,Employees,Crews,Production,Material Catalogue,Clients,Vendors,Projects,Accepted Projects,Job Costing,TimeSheet,Scheduling,Courses Management,Enrollment";
                //}
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


    public static class SidebarMenuHelper
    {
        public static List<string> GetSidebarMenuNames()
        {
            return new List<string>
            {

                "Users",
                "Projects",
                "PaymentPlans",
                "Installments",
                "Units",
                "Country",
                "City",
                "UnitTypes",

            };



        }
    }
    public class SweetAlert
    {
        // Define constant values for title and type
        private const string AlertTitle = "Success";
        private const string AlertType = "info"; // default type

        public static string ShowSweetAlert(string message)
        {
            return $@"
            <script>
                Swal.fire('{AlertTitle}', '{message}', '{AlertType}');
            </script>";
        }
    }
    public class SweetAlertDanger
    {
        private const string AlertTitle = "Error";
        private const string AlertType = "info"; // default type


        public static string ShowSweetAlert(string message)
        {
            string escapedMessage = message.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");
            return $@"
                 <script>
                     document.addEventListener('DOMContentLoaded', function() {{
                         Swal.fire('{AlertTitle}', '{escapedMessage}', '{AlertType}');
                     }});
                 </script>";
        }

    }
    public class SweetDangerAlert
    {
        private const string DefaultAlertType = "info"; // Default alert type

        public static string ShowSweetAlert(string title, string message)
        {
            string escapedTitle = title.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");
            string escapedMessage = message.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");

            return $@"
                <script>
                    document.addEventListener('DOMContentLoaded', function() {{
                        Swal.fire('{escapedTitle}', '{escapedMessage}', '{DefaultAlertType}');
                    }});
                </script>";
        }
    }
}
