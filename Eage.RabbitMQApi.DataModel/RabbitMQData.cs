using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.RabbitMQApi.DataModel
{
    public class RabbitMQData
    {
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public int rabbitMQueueType { get; set; }
        public string rabbitMQName { get; set; }
        public string rabbitMQMes { get; set; }
    }
}
