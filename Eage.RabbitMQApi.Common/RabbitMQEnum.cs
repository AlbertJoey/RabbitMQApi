using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.RabbitMQApi.Common
{
    public enum RabbitMQEnum
    {
        //点对点机制
        PeerToPeerQueue = 0,
        //广播机制
        BroadcastQueue = 1,
        //轮换机制
        RotationQueue = 2
    }
}
