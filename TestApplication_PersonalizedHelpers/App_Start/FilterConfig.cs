using System.Web;
using System.Web.Mvc;

namespace TestApplication_PersonalizedHelpers
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}