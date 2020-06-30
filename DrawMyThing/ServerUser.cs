using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class ServerUser : User
    {
        [NonSerialized]
        public Semaphore s;
        public int points { get; set; }

        public ServerUser(int id, string name, TcpClient client)
        {
            Id = id;
            Name = name;
            Client = client;
            points = 0;
            s = new Semaphore(1, 1);
            ConnectionClosed = false;
        }
        public void RunServer(BlockingCollection<ClassToSend> bc)
        {
            //Initialize
            try
            {
                while (true)
                {
                    ClassToSend msg = (ClassToSend)bf.Deserialize(Client.GetStream());
                    if (msg.Type == Type.Ping)
                    {
                        continue;
                    }
                    msg.Id = this.Id;
                    msg.Name = this.Name;
                    bc.Add(msg);
                }
            }
            catch (Exception e)
            {
            }
        }

    }
}
