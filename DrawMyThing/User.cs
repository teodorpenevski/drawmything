using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NonSerialized]
        public TcpClient Client;
        [NonSerialized]
        private BinaryFormatter bf = new BinaryFormatter();
        //SERVER
        public User(int id,string name,TcpClient client)
        {
            Id = id;
            Name = name;
            Client = client;
        }

        public void Initialize()
        {
            ClassToSend msg = new ClassToSend();
            msg.Type = Type.Start;
            bf.Serialize(Client.GetStream(), msg);
        }

        public void RunServer(BlockingCollection<ClassToSend> bc)
        {
            //Initialize
            while (true)
            {
                ClassToSend msg = (ClassToSend)bf.Deserialize(Client.GetStream());
                bc.Add(msg);
            }
        }

        public void ToUserMessage(ClassToSend msg)
        {
            bf.Serialize(Client.GetStream(), msg);
        }

        //CLIENT
        public User(string Name,string Address,int port=25565)
        {
            this.Name = Name;
            Client = new TcpClient(Address,port);
            ClassToSend msg = new ClassToSend();
            msg.Name = this.Name;
            bf.Serialize(Client.GetStream(),msg);
        }
       
        public ClassToSend RunClient()
        {
             return (ClassToSend)bf.Deserialize(Client.GetStream());
        }

    }
}
