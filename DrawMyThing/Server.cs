using DrawMyThing.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DrawMyThing
{
    public class Server
    {
        public static string wordToGuess { get; set; }
        public static int RoundTime = 100;
        public List<ServerUser> Users { get; set; }
        public int Drawer { get; set; }
        public int Port { get; set; }
        public TcpListener Listener { get; set; }
        public Thread LobbyThread { get; set; }
        public Thread GameThread { get; set; }
        public ConcurrentQueue<ClassToSend> Queue { get; set; }
        public List<Thread> UserThreads { get; set; }
        public int Points { get; set; }
        public int playersGuessing { get; set; }
        public int RoundCounter { get; set; }
        public bool PlayerLeft = false;
        public DateTime PingTime;
        private BinaryFormatter bf = new BinaryFormatter();
        private Random random = new Random();
        public Server(int port = 25565)
        {
            Users = new List<ServerUser>();
            Port = port;
            Drawer = -1;
            Points = 20;
            RoundCounter = 0;
        }
        public void StartLobby()
        {
            Thread LobbyThread = new Thread(() =>
           {
               Listener = new TcpListener(IPAddress.Any, Port);
               Listener.Start(8);
               AcceptClients();
           });
            this.LobbyThread = LobbyThread;
            this.LobbyThread.IsBackground = true;
            LobbyThread.Start();
        }

        public Task SendMessage(ServerUser user, ClassToSend msg)
        {
            if(user.ConnectionClosed)
            {
                return new Task(() => { });
            }
            Task t = new Task(new Action(() => {
                user.s.WaitOne();
                try
                {
                    bf.Serialize(user.Client.GetStream(), msg);
                }
                catch (Exception e)
                {
                    PlayerLeft = true;
                    user.ConnectionClosed = true;
                    Console.WriteLine(e.Message);
                }
                user.s.Release(1);
            }));
            t.Start();
            return t;
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
                    ClassToSend msg = new ClassToSend();
                    msg.Id = Id;
                    Users.Add(new ServerUser(Id++, MSG.Name, CurrentClient));
                    msg.Users = Users;
                    msg.Type = Type.LobbyUpdate;
                    foreach (var user in Users)
                    {
                        SendMessage(user, msg);
                    }

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

            }
        }

        public void InitializeUsers()
        {
            ClassToSend msg = new ClassToSend();
            msg.Type = Type.Start;
            foreach (ServerUser user in Users)
            {
                SendMessage(user, msg);
            }
        }

        public void StartGame()
        {
            Listener.Stop();
            LobbyThread.Abort();
            GameThread = new Thread(() =>
            {
                Queue = new ConcurrentQueue<ClassToSend>();
                BlockingCollection<ClassToSend> bc = new BlockingCollection<ClassToSend>(Queue);
                StartThreads(bc);
                InitializeUsers();
                RoundEnd();
                RoundStart();
                playersGuessing = Users.Count() - 1;
                DateTime time = DateTime.Now;
                PingTime = DateTime.Now;
                while (true)
                {
                    SwitchRound(ref time, bc);
                    Ping();
                    if(PlayerLeft)
                    {
                        foreach(ServerUser u in Users)
                        {
                            ClassToSend msg = new ClassToSend();
                            msg.Type = Type.CloseConnection;
                            SendMessage(u, msg);
                        }
                        break;
                    }
                    SendingPicture(bc);
                    //Preprakjanje
                }
            });
            GameThread.IsBackground = true;
            GameThread.Start();
        }

        public void Ping()
        {
            TimeSpan ts = DateTime.Now.Subtract(PingTime);
            if(ts.Seconds > 5)
            {
                foreach(ServerUser u in Users)
                {
                    SendMessage(u, new ClassToSend() { Type = Type.Ping });
                }
                PingTime = DateTime.Now;
            }
        }
        public void SwitchRound(ref DateTime time, BlockingCollection<ClassToSend> bc)
        {
            TimeSpan ts = DateTime.Now.Subtract(time);
            if ((ts.Seconds+60*ts.Minutes) > RoundTime || playersGuessing == 0)
            {
                playersGuessing = Users.Count() - 1;
                
                RoundEnd();
                Thread.Sleep(3000);
                RoundStart();
                time = DateTime.Now;
            }
        }

        public void RoundEnd()
        {
            Points = 20;
            if (Drawer == -1)
            {
                Drawer++;
                return;
            }
            ClassToSend msg = new ClassToSend();
            msg.Type = Type.RoundEnd;
            msg.WordToGuess = wordToGuess;
            List<Task> tasks = new List<Task>();
            foreach (ServerUser user in Users)
            {
                tasks.Add(SendMessage(user, msg));
            }
            Task.WaitAll(tasks.ToArray());
            Drawer = (Drawer + 1) % Users.Count;
        }

        public void RoundStart()
        {
            RoundCounter++;
            wordToGuess = GenerateWord().Trim();
            ClassToSend msg = new ClassToSend();
            msg.Type = Type.RoundStartTest;
            msg.WordToGuess = HideWord(wordToGuess);
            msg.Turn = false;
            msg.DrawerName = Users[Drawer].Name;
            List<Task> tasks = new List<Task>();
            foreach (ServerUser user in Users)
            {
                if (user.Id == Drawer)
                {
                    ClassToSend DrawerMsg = new ClassToSend(msg);
                    DrawerMsg.Turn = true;
                    DrawerMsg.WordToGuess = wordToGuess;
                    tasks.Add(SendMessage(user, DrawerMsg));
                }
                else
                {
                    tasks.Add(SendMessage(user, msg));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void SendingPicture(BlockingCollection<ClassToSend> bc)
        {
            ClassToSend msg = null;
            bc.TryTake(out msg);
            if (msg != null && RoundCounter==msg.RoundId)
            {
                if (msg.Type == Type.Picture)
                {
                    foreach (ServerUser user in Users)
                    {
                        if (!msg.Name.Equals(user.Name))
                            SendMessage(user, msg);
                    }
                }
                ActionToGuess(msg);
            }
        }

        public void ActionToGuess(ClassToSend msg)
        {
            if (msg.Type == Type.Guess)
            {
                if (msg.GuessedWord.Equals(wordToGuess))
                {
                    if (msg.Id == Drawer)
                        return;
                    playersGuessing--;
                    foreach (ServerUser user in Users)
                    {

                        if (user.Id.Equals(msg.Id))
                        {
                            ClassToSend newMsg = new ClassToSend(msg);

                            user.points += Points;
                            Points = (int) Math.Ceiling(Points / 2.0);
                            UpdatePoints(user, newMsg);
                            newMsg.Type = Type.Guessed;
                            newMsg.Points = user.points;
                            SendMessage(user, newMsg);
                        }
                        else
                        {
                            ClassToSend newMsg = new ClassToSend(msg);

                            newMsg.Type = Type.Another;
                            SendMessage(user, newMsg);
                        }
                    }
                }
                else
                {
                    msg.Type = Type.Miss;
                    foreach (var user in Users)
                    {
                        SendMessage(user, msg);
                    }
                }
            }
        }

        public void UpdatePoints(ServerUser user, ClassToSend msg)
        {
            foreach (ServerUser uss in Users)
            {
                if (uss.Id == user.Id)
                {
                    continue;
                }

                ClassToSend newMsg = new ClassToSend(msg);

                newMsg.Type = Type.PointUpdate;
                newMsg.Points = user.points;
                newMsg.Id = user.Id;
                SendMessage(uss, newMsg);
            }
        }
        public void StartThreads(BlockingCollection<ClassToSend> bc)
        {
            UserThreads = new List<Thread>();
            foreach (ServerUser user in Users)
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
        public void Stop()
        {
            if (UserThreads != null)
            { 
                foreach (var user in UserThreads)
                {
                    user.Abort();
                }
            }
            foreach(ServerUser u in Users)
            {
                u.Client.Close();
            }

            if(LobbyThread.ThreadState != ThreadState.Aborted)
            {
                Listener.Stop();
                LobbyThread.Abort();
                LobbyThread = null;
            }

            if (GameThread != null && GameThread.ThreadState != ThreadState.Aborted)
            {
                GameThread.Abort();
                GameThread = null;
            }

        }
        public string HideWord(string word)
        {
            string tmp = "";
            tmp += word[0];
            for (int i = 1; i < word.Length; i++)
            {
                tmp += " - ";
            }
            return tmp;
        }
        public string GenerateWord()
        {
            string resource_data = Properties.Resources.words;
            List<string> words = resource_data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int randomLine = random.Next(0, words.Count);
            Console.WriteLine(randomLine);
            return words.ElementAt(randomLine);
        }

    }
}
