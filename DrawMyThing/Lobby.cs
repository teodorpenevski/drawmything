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
        public ClassToSend Drawer;
        public Line CurrentLine;
        public Color CurrentColor = Color.Black;
        public bool Down;
        public int Counter { get; set; }

        public Lobby(string Name,string Address,int port=25565)//Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Address = Address;
            this.Port = port;
            createServer = false;
            Drawer = new ClassToSend();
        }

        public Lobby(string Name, int port = 25565)//Server+Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Port = port;
            createServer = true;
            Drawer = new ClassToSend();
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
                ServerAndUserCreation();
            }
            else
            {
                UserCreation();
            }
            DelegateMethod();
        }

        public void DelegateMethod()
        {
            myDelegate = new MessageFilter(MessageFilterMethod);
            UserThread = new Thread(() => {
                try
                {
                    while (true)
                    {
                        ClassToSend msg = user.RunClient();
                        label5.Invoke(myDelegate, new Object[] { msg });
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            UserThread.Start();
        }

        public void UserCreation()
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

        public void ServerAndUserCreation()
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
                case Type.Picture:
                    {
                        Drawer = msg;
                        pictureBox1.Invalidate();
                    }
                    break;
                case Type.Drawing:
                    {
                        pictureBox1.Enabled = msg.Turn;
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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Down = true;
                Line p1 = new Line(CurrentColor, Int32.Parse(numericUpDown1.Value.ToString()));
                CurrentLine = p1;
                p1.addPoint(new Point(e.X, e.Y));
                Drawer.AddLine(p1);
                Drawer.Type = Type.Picture;
                Drawer.Id = user.Id;
                Drawer.Name = user.Name;

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Down)
            {
                CurrentLine.addPoint(new Point(e.X, e.Y));
                if(Counter%5==0)
                {
                    bf.Serialize(user.Client.GetStream(), Drawer);
                }
                Counter++;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Down = false;
            bf.Serialize(user.Client.GetStream(), Drawer);
            pictureBox1.Invalidate();
        }

        private void Lobby_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Drawer.Draw(e.Graphics);
        }
    }
}
