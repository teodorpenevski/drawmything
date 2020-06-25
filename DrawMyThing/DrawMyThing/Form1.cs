using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.IO;

namespace DrawMyThing
{
    public delegate void OnActionRecieve(object source, RecievedArgs e);

    public partial class Form1 : Form
    {
        public event OnActionRecieve Recieve;
        private ClientReciever client;
        private Drawer d;
        private bool down;
        private Point prev;
        private BinaryFormatter bf;
        private Thread ClientThread;
        public Form1(int port=9001)
        {
            InitializeComponent();
            bf = new BinaryFormatter();
            d = new Drawer(); 
            TcpClient client = new TcpClient();     
            client.Connect(IPAddress.Loopback,port);
            int Id = (int)bf.Deserialize(client.GetStream());
            Recieve += new OnActionRecieve(ActionRecieve);
            this.client = new ClientReciever(Id, client,Recieve);
            ClientThread = new Thread(this.client.Run);
            ClientThread.IsBackground = true;
            ClientThread.Start();
        }
        public void ActionRecieve(object sender,RecievedArgs e)
        {
            d.AddNewAction(e.Argument);
            pictureBox.Invalidate();
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
            prev = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                Action a = new Action(prev, e.Location, Color.Black, 4);
                a.Id = client.Id;
                d.AddNewAction(a);
                bf.Serialize(client.client.GetStream(), a);
                prev = e.Location;
                Invalidate(true);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            d.Draw(e.Graphics);
            /*Bitmap bm = new Bitmap(pictureBox.Width,pictureBox.Height);
            using (Graphics g = Graphics.FromImage(bm))
            {
                d.Draw(g);
            }
            if(pictureBox.Image!=null)
            {
                pictureBox.Image.Dispose();
            }
            pictureBox.Image = bm;*/
            label1.Text = (Int32.Parse(label1.Text)+1).ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientThread.Abort();
            client.client.Close();
        }
    }

    public class RecievedArgs
    {
        public Action Argument;
        public RecievedArgs(Action a)
        {
            this.Argument = a;
        }
    }

    class ClientReciever
    {
        public int Id { get; set; }
        public TcpClient client;
        public event OnActionRecieve e;
        public ClientReciever(int id, TcpClient c, OnActionRecieve r)
        {
            e = r;
            this.Id = id;
            this.client = c;
        }
        public void Run()
        {
            BinaryFormatter bf = new BinaryFormatter();
            while (true)
            {
                e(this, new RecievedArgs((Action)(bf.Deserialize(client.GetStream()))));
            }
        }
    }
}
