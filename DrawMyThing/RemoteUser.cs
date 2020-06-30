using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class RemoteUser : User
    {

        public RemoteUser(string Name, string Address, int port = 25565)
        {
            this.Name = Name;
            Client = new TcpClient(Address, port);
            ClassToSend msg = new ClassToSend();
            msg.Name = this.Name;
            bf.Serialize(Client.GetStream(), msg);
        }

        public ClassToSend RunClient()
        {
            return (ClassToSend)bf.Deserialize(Client.GetStream());
        }


    }
}
