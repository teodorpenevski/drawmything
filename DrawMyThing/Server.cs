using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrawMyThing
{
    public class Server
    {
        public static int RoundTime = 10;
        public List<User> Users { get; set; }
        public int Drawer;
        public int Port { get; set; }
        public TcpListener Listener { get; set; }
        public Thread LobbyThread { get; set; }
        public ConcurrentQueue<ClassToSend> Queue {get;set;}
        public List<Thread> UserThreads { get; set; }

        private BinaryFormatter bf = new BinaryFormatter();
        public Server(int port=25565)
        {
            Users = new List<User>();
            Port = port;
            Drawer = -1;
        }
        public void StartLobby()
        {
            Thread LobbyThread = new Thread( () =>
            {
                Listener = new TcpListener(IPAddress.Any, Port);
                Listener.Start(8);
                AcceptClients();
            });
            this.LobbyThread = LobbyThread;
            this.LobbyThread.IsBackground = true;
            LobbyThread.Start();
        }

        void AcceptClients()
        {
            int Id = 0;
            while (true)
            {
                try
                {
                    TcpClient CurrentClient = Listener.AcceptTcpClient();
                    ClassToSend MSG = (ClassToSend)bf.Deserialize(CurrentClient.GetStream());
                    Users.Add(new User(Id++, MSG.Name, CurrentClient));

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
        }

        public void InitializeUsers()
        {
            foreach (User user in Users)
            {
                user.Initialize();
            }
        }

        public void StartGame()
        {
            Listener.Stop();
            LobbyThread.Abort();
            Thread GameThread = new Thread(()=> {
                Queue = new ConcurrentQueue<ClassToSend>();
                BlockingCollection<ClassToSend> bc = new BlockingCollection<ClassToSend>(Queue);
                StartThreads(bc);
                InitializeUsers();
                TurnRemove();
                TurnGive();
                DateTime time = DateTime.Now;
                while(true)
                {
                    SwitchRound(time, bc);

                    SendingPicture(bc);
                    //Preprakjanje
                }
            });
            GameThread.IsBackground = true;
            GameThread.Start();

        }

        public void SwitchRound(DateTime time, BlockingCollection<ClassToSend> bc)
        {
            TimeSpan ts = DateTime.Now.Subtract(time);
            if (ts.Seconds > RoundTime)
            {
                TurnRemove();
                _ = DateTime.Now;
                ClassToSend MSG = new ClassToSend();
                MSG.Type = Type.Picture;
                Thread.Sleep(200);
                while (bc.TryTake(out _)) { }
                foreach (User user in Users)
                {
                    user.ToUserMessage(MSG);
                }
                TurnGive();
            }
        }

        public void SendingPicture(BlockingCollection<ClassToSend> bc)
        {
            ClassToSend msg = null;
            bc.TryTake(out msg);
            if (msg != null)
            {
                if (msg.Type == Type.Picture)
                {
                    foreach (User user in Users)
                    {
                        if (!msg.Name.Equals(user.Name))
                            user.ToUserMessage(msg);
                    }
                }
            }
        }

        public void StartThreads(BlockingCollection<ClassToSend> bc)
        {
            UserThreads = new List<Thread>();
            foreach (User user in Users)
            {
                Thread currThread = new Thread(() =>
                {
                    user.RunServer(bc);
                });
                UserThreads.Add(currThread);
                currThread.IsBackground = true;
                currThread.Start();
            }
        }

        public void TurnGive()
        {
            ClassToSend MSG = new ClassToSend();
            MSG.Type = Type.Drawing;
            MSG.Turn = true;
            Users.ElementAt(Drawer).ToUserMessage(MSG);
        }

        public void TurnRemove()
        {
            if(Drawer==-1)
            {
                Drawer++;
                return;
            }
            ClassToSend MSG = new ClassToSend();
            MSG.Type = Type.Drawing;
            MSG.Turn = false;
            Users[Drawer].ToUserMessage(MSG);
            Drawer = (Drawer + 1) % Users.Count;
        }
        public void Stop()
        {
            if(LobbyThread.ThreadState != ThreadState.Aborted)
            {
                Listener.Stop();
                LobbyThread.Abort();
                LobbyThread = null;
            }
        }

    }
}
