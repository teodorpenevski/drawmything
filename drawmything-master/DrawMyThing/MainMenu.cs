using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawMyThing
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //TODO: port implement
            Lobby l = new Lobby(tbName.Text,tbConnectAddress.Text);
            this.Hide();
            l.ShowDialog();
            this.Show();
            if (l.DialogResult == DialogResult.Abort)
            {
                MessageBox.Show("An Error has occurred");
            }
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            Lobby l = new Lobby(tbName.Text);
            this.Hide();
            l.ShowDialog();
            this.Show();
            if(l.DialogResult == DialogResult.Abort)
            {
                MessageBox.Show("An Error has occurred");
            }
        }
    }
}
