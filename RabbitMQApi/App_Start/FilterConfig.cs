using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace RabbitMQApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

    }
}