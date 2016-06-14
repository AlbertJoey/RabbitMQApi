using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eage.RabbitMQApi.Common;

namespace Eage.RabbitMQApi.Service
{
   public static class MesQueue
    {
        private static IConnection _getfactoryconn;
        private static readonly object SyncObject = new object();
        public static IConnection GetFactoryConn
        {
            get
            {
                if (null == _getfactoryconn)
                {
                    lock (SyncObject)
                    {
                        var factory =  new ConnectionFactory() { HostName = GetConfigInfo.getRabbitMQIpAdress, Password = GetConfigInfo.getRabbitMQPassword, UserName =GetConfigInfo.getRabbitMQName, Port = Convert.ToInt32(GetConfigInfo.getRabbitMQPort), AutomaticRecoveryEnabled = true };

                        _getfactoryconn = factory.CreateConnection();
                    }
                }
                return _getfactoryconn;
            }
        }
    }
}
