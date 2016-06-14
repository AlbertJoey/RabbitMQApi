using Eage.RabbitMQApi.Common;
using Eage.RabbitMQApi.DataModel;
using Eage.RabbitMQApi.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;

namespace RabbitMQApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public RabbitMQResultInfo PostRabbitMes(RabbitMQData rabbitmqdata)
        {
            try
            {
                //判断调用者和密码是否匹配
                bool IsSuccess = false;
                XmlDocument doc = new XmlDocument();
                string filen2 = System.Web.HttpContext.Current.Server.MapPath("/User.xml");
                doc.Load(filen2);
                //读取根节点的所有子节点，放到xn0中 
                XmlNodeList FirstNode2 = doc.SelectSingleNode("Users").ChildNodes;
                foreach (XmlNode item in FirstNode2)
                {
                    string userName = item.Attributes["userName"].Value;
                    string userToken = item.Attributes["userToken"].Value;
                    if (rabbitmqdata.UserName == userName && rabbitmqdata.UserCode == userToken)
                    {
                        IsSuccess = true;
                        break;
                    }
                }
                if (!IsSuccess)
                {
                    return new RabbitMQResultInfo()
                    {
                        isSuccess = (int)RabbitMQInfoEnum.Error,
                        msg = "口令错误！"
                    };
                }
                else
                {
                    switch (rabbitmqdata.rabbitMQueueType)
                    {
                        case (int)RabbitMQEnum.PeerToPeerQueue:
                            PeerToPeerQueue(rabbitmqdata);
                            break;
                        case (int)RabbitMQEnum.BroadcastQueue:
                            BroadcastQueue(rabbitmqdata);
                            break;
                        case (int)RabbitMQEnum.RotationQueue:
                            RotationQueue(rabbitmqdata);
                            break;
                        default:
                            break;
                    }
                    return new RabbitMQResultInfo()
                    {
                        isSuccess = (int)RabbitMQInfoEnum.Success,
                        msg = "接收成功！"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RabbitMQResultInfo()
                {
                    isSuccess = (int)RabbitMQInfoEnum.Exception,
                    msg = "系统异常："+ex.Message
                };
            }
        }
        //点对点
        private void PeerToPeerQueue(RabbitMQData rabbitmqdata)
        {
            var conn= MesQueue.GetFactoryConn;
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: rabbitmqdata.rabbitMQName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                string message = rabbitmqdata.rabbitMQMes;

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: rabbitmqdata.rabbitMQName,
                                     basicProperties: null,
                                     body: body);
            }
        }
        //广播机制
        private void BroadcastQueue(RabbitMQData rabbitmqdata)
        {
            var conn= MesQueue.GetFactoryConn;
            using (var channel = conn.CreateModel())
            {

                channel.ExchangeDeclare(exchange: rabbitmqdata.rabbitMQName, type: "fanout");

                var message = rabbitmqdata.rabbitMQMes;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: rabbitmqdata.rabbitMQName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }
        //轮换机制
        private void RotationQueue(RabbitMQData rabbitmqdata)
        {
            var conn= MesQueue.GetFactoryConn;
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: rabbitmqdata.rabbitMQName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var message = rabbitmqdata.rabbitMQMes;

                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();

                properties.SetPersistent(true);

                channel.BasicPublish(exchange: "",
                                     routingKey: rabbitmqdata.rabbitMQName,
                                     basicProperties: properties,
                                     body: body);
            }
        }
    }
}