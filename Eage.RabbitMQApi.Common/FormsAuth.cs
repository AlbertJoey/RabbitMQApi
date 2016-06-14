using Eage.RabbitMQApi.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Eage.RabbitMQApi.Common
{
   public static class FormsAuth
    {
       public static void SignIn(string loginName, object userData, int expireMin)
       {
           var data = JsonConvert.SerializeObject(userData);

           var ticket = new FormsAuthenticationTicket(2, loginName, DateTime.Now, DateTime.Now.AddDays(100), true, data);

           var cookieValue = FormsAuthentication.Encrypt(ticket);

           var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
           {
               HttpOnly = true,
               Secure = FormsAuthentication.RequireSSL,
               Domain = FormsAuthentication.CookieDomain,
               Path = FormsAuthentication.FormsCookiePath
           };
           if (expireMin > 0)
               cookie.Expires = DateTime.Now.AddDays(expireMin);
           var context = HttpContext.Current;
           if (context == null)
           {
               throw new InvalidOperationException();
           }

           context.Response.Cookies.Remove(cookie.Name);
           context.Response.Cookies.Add(cookie);
       }
       public static void SignOut()
       {
           FormsAuthentication.SignOut();
       }
       public static T GetUserData<T>() where T : class,new()
       {
           var UserData = new T();
           try
           {
               var context = HttpContext.Current;
               var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
               var ticket = FormsAuthentication.Decrypt(cookie.Value);
               UserData = JsonConvert.DeserializeObject<T>(ticket.UserData);
           }
           catch (Exception)
           {
               
               throw;
           }
           return UserData;
       }
    }
}
