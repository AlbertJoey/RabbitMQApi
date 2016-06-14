using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.RabbitMQApi.Common
{
    public class GetConfigInfo
    {
        public static string getRabbitMQName
        {
            get
            {
                string _getrabbitmqname = ConfigurationManager.AppSettings["getRabbitMQName"];
                return _getrabbitmqname;
            }
        }
        public static string getRabbitMQPassword
        {
            get
            {
                string _getrabbitmqpassword = ConfigurationManager.AppSettings["getRabbitMQPassword"];
                return _getrabbitmqpassword;
            }
        }
        public static string getRabbitMQIpAdress
        {
            get
            {
                string _getrabbitmqipadress = ConfigurationManager.AppSettings["getRabbitMQIpAdress"];
                return _getrabbitmqipadress;
            }
        }
        public static string getRabbitMQPort
        {
            get
            {
                string _getrabbitmqport = ConfigurationManager.AppSettings["getRabbitMQPort"];
                return _getrabbitmqport;
            }
        }
    }
}
