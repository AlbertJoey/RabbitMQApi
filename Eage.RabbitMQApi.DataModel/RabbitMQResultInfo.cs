using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.RabbitMQApi.DataModel
{
  public  class RabbitMQResultInfo
    {
        public int isSuccess { get; set; }
        public string msg { get; set; }
        public RabbitMQResultInfo(int isSuccess, string msg)
        {
            this.isSuccess = isSuccess;
            this.msg = msg;
        }
        public RabbitMQResultInfo()
        {

        }
    }
}
