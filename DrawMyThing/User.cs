using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
        protected BinaryFormatter bf = new BinaryFormatter();
        public bool ConnectionClosed;

    }
}
