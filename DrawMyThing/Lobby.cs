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
        public string ErrorMessage;
        public RemoteUser user;
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
        public int points;
        public int Counter { get; set; }
        public int RoundCounter;
        public int time = Server.RoundTime;

        public Lobby(string Name,string Address,int port=25565)//Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Address = Address;
            this.Port = port;
            createServer = false;
            Drawer = new ClassToSend();
            points = 0;
            RoundCounter = 0;
        }

        public Lobby(string Name, int port = 25565)//Server+Client Lobby
        {
            InitializeComponent();
            this.Name = Name;
            this.Port = port;
            createServer = true;
            Drawer = new ClassToSend();
            points = 0;
            RoundCounter = 0;
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
            timerPing.Enabled = true;
        }

        public void DelegateMethod()
        {
            if(DialogResult == DialogResult.Retry)
            {
                return;
            }
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
                catch (Exception ex)
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
                user = new RemoteUser(Name, Address, Port);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                this.DialogResult = DialogResult.Retry;
            }
            label5.Text = "Connected";
            button1.Hide();
        }

        public void ServerAndUserCreation()
        {
            server = new Server(Port);
            server.StartLobby();
            Thread.Sleep(100);
            user = new RemoteUser(Name, "127.0.0.1", Port);
            label5.Text = "Connected";
            if (!user.Client.Connected)
            {
                this.DialogResult = DialogResult.Retry;
            }
        }

        private void SetControls()
        {
            groupBox1.Visible = false;
            button1.Visible = false;
            txtLog.Visible = true;
            txtChat.Visible = true;
            btnGuess.Visible = true;
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = false;
            lblDrawerName.Visible = true;
        }

        private void CorrectWord(ClassToSend msg)
        {
            lblWordToGuess.Text = msg.GuessedWord;
            txtLog.Text += "You guessed the word!\r\n";
            listPlayers.Items[msg.Id].SubItems[1].Text = "Points: " + msg.Points;
            txtChat.Enabled = false;
            btnGuess.Enabled = false;
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void AnotherGuessed(ClassToSend msg)
        {
            txtLog.Text += msg.Name + " guessed the word!\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void RoundStart(ClassToSend msg)
        {
            time = Server.RoundTime;
            timer.Start();
            Counter = 0;
            RoundCounter++;
            Down = false;
            txtChat.Enabled = true;
            btnGuess.Enabled = true;
            groupBox1.Visible = msg.Turn;
            pictureBox1.Enabled = msg.Turn;
            lblWordToGuess.Text = msg.WordToGuess;
            lblDrawerName.Visible = true;
            gbChangeRound.Visible = false;
            lblChangeRound.Visible = false;
            if (msg.Turn)
            {
                lblDrawerName.Text = "You are drawing!";
            }
            else
            {
                lblDrawerName.Text = $"{msg.DrawerName} is drawing!";
            }
            Drawer.Lines.Clear();
            pictureBox1.Invalidate();
        }

        private void RoundEnd(ClassToSend msg)
        {
            timer.Stop();
            groupBox1.Visible = false;
            txtChat.Enabled = false;
            btnGuess.Enabled = false;
            lblDrawerName.Visible = false; ;
            pictureBox1.Enabled = false;
            gbChangeRound.Visible = true;
            lblChangeRound.Visible = true;
            lblWordToGuess.Text = "The word was " + msg.WordToGuess + "!";
        }
        private void Drawing(ClassToSend msg)
        {
            txtChat.Enabled = true;
            btnGuess.Enabled = true;
            groupBox1.Visible = true;
            lblWordToGuess.Text = msg.WordToGuess;
            pictureBox1.Enabled = msg.Turn;
            gbChangeRound.Visible = false;
            lblChangeRound.Visible = false;
        }

        public void MessageFilterMethod(ClassToSend msg)
        {
            switch(msg.Type)
            {
                case Type.Start:
                    {
                        SetControls();
                    }
                    break;
                case Type.Picture:
                    {
                        Drawer = msg;
                        pictureBox1.Invalidate();
                    }
                    break;
                case Type.RoundStartTest:
                    {
                        RoundStart(msg);
                    }
                    break;
                case Type.RoundEnd:
                    {
                        RoundEnd(msg);
                    }
                    break;
                case Type.Drawing:
                    {
                        Drawing(msg);
                    }
                    break;
                case Type.TimesUp:
                    {
                        MessageBox.Show("Time's up!");
                    }
                    break;
                case Type.Guessed:
                    {
                        CorrectWord(msg);
                    }
                    break;
                case Type.PointUpdate:
                    {
                        listPlayers.Items[msg.Id].SubItems[1].Text = "Points: " + msg.Points;
                    }
                    break;
                case Type.Another:
                    {
                        AnotherGuessed(msg);
                    }
                    break;
                case Type.Miss:
                    {
                        if (!msg.Name.Equals(user.Name))
                            txtLog.Text += msg.Name + ": " + msg.GuessedWord + "\r\n";
                        else txtLog.Text += "You: " + msg.GuessedWord + "\r\n";
                    }
                    break;
                case Type.LobbyUpdate:
                    {
                        user.Id = msg.Id;
                        MakeViewList(msg);
                    }
                    break;
                case Type.CloseConnection:
                    {
                        DialogResult = DialogResult.Abort;
                        this.Close();
                    }
                    break;
                default:
                    {
                        
                    }
                    break;
            }
        }

        public void MakeViewList(ClassToSend msg)
        {
            listPlayers.Items.Clear();
            listPlayers.View = View.Tile;
            listPlayers.Columns.Add("Player");
            listPlayers.Columns.Add("Points");
            var lvi = listPlayers.Items.Add(msg.Users[0].Name, msg.Users[0].Id % 4);
            lvi.SubItems.Add("Points: 0");
            listPlayers.Items[0].Group = listPlayers.Groups[0];
            for (int i = 1; i < msg.Users.Count; i++)
            {
                var tmp = listPlayers.Items.Add(msg.Users[i].Name, msg.Users[i].Id % 4);
                tmp.SubItems.Add("Points: 0");
                listPlayers.Items[msg.Users[i].Id].Group = listPlayers.Groups[0];
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
                Line p1 = new Line(CurrentColor, Int32.Parse(brushSize.Value.ToString()));
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
                if(Counter%10==0)
                {
                    Drawer.RoundId = RoundCounter;
                    SendMessage(Drawer);
                }
                Counter++;
                pictureBox1.Invalidate();
            }
        }

        private void SendMessage(ClassToSend msg)
        {
            try
            {
                bf.Serialize(user.Client.GetStream(), msg);
            }
            catch(Exception ex)
            {
                DialogResult = DialogResult.Abort;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Down = false;
            Drawer.RoundId = RoundCounter;
            SendMessage(Drawer);
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

        private void btnRed_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Yellow;
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Black;
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentColor = colorDialog.Color;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Drawer.Clear();
            Drawer.RoundId = RoundCounter;
            SendMessage(Drawer);
            pictureBox1.Invalidate();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            if(txtChat.Text.Trim().Equals(""))
            {
                txtChat.Text = "";
                return;
            }
            ClassToSend msg = new ClassToSend();
            msg.Type = Type.Guess;
            msg.Id = user.Id;
            msg.GuessedWord = txtChat.Text.Trim();
            msg.RoundId = RoundCounter;
            SendMessage(msg);
            txtChat.Text = "";
        }


        private void txtLog_TextChanged_1(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "Time left: " + time;
            time -= 1;
        }

        private void timerPing_Tick(object sender, EventArgs e)
        {
            SendMessage(new ClassToSend() { Type = Type.Ping });
        }
    }
}
