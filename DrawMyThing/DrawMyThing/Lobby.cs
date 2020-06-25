using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawMyThing
{
    public partial class Lobby : Form
    {
        public User user;
        public Server server;
        public Thread UserThread;
        public delegate void MessageFilter(ClassToSend msg);
        public MessageFilter myDelegate;
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public bool createServer;
        public static BinaryFormatter bf = new BinaryFormatter();

        public Lobby(string Name,string Address,int port=25565)//Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Address = Address;
            this.Port = port;
            createServer = false;
        }

        public Lobby(string Name, int port = 25565)//Server+Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Port = port;
            createServer = true;

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.StartGame();
        }

        private void Lobby_Load(object sender, EventArgs e)
        {
            //TUKA se
            if(createServer)
            {
                server = new Server(Port);
                server.StartLobby();
                Thread.Sleep(100);
                user = new User(Name, "127.0.0.1", Port);
                label5.Text = "SUCC";
                if (!user.Client.Connected)
                {
                    this.DialogResult = DialogResult.Abort;
                }
            }
            else
            {
                try
                {
                    user = new User(Name, Address, Port);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    this.DialogResult = DialogResult.Abort;
                }
                label5.Text = "Connected";
                button1.Hide();
            }
            myDelegate = new MessageFilter(MessageFilterMethod);
            UserThread = new Thread(()=> {
                try {
                    while(true)
                    {
                        OnMessageRecieveCall(user.RunClient());
                    }  
                }
                catch(IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            UserThread.Start();
        }

        private void OnMessageRecieveCall(ClassToSend Msg)
        {
            //Dali kje raboti???
            label5.Invoke(myDelegate, new Object[] { Msg });
        }

        public void MessageFilterMethod(ClassToSend msg)
        {
            switch(msg.Type)
            {
                case Type.Start:
                    {
                        button1.Visible = false;
                        numericUpDown1.Visible = true;
                        textBox1.Visible = true;
                        textBox2.Visible = true;
                        button2.Visible = true;
                        pictureBox2.Visible = false;
                        pictureBox1.Visible = true;
                        pictureBox1.Enabled = false;
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
        private void Lobby_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(user!=null)
                user.Client.Close();
            if(server!=null)
                server.Stop();
        }
    }
}
