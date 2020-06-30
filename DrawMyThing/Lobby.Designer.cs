namespace DrawMyThing
{
    partial class Lobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Players", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lobby));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listPlayers = new System.Windows.Forms.ListView();
            this.playerIcons = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnGuess = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRed = new System.Windows.Forms.Button();
            this.brushSize = new System.Windows.Forms.TrackBar();
            this.btnClear = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblWordToGuess = new System.Windows.Forms.Label();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnBlack = new System.Windows.Forms.Button();
            this.btnColors = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDrawerName = new System.Windows.Forms.Label();
            this.gbChangeRound = new System.Windows.Forms.GroupBox();
            this.lblChangeRound = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerPing = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brushSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbChangeRound.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(203, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(674, 566);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // listPlayers
            // 
            listViewGroup1.Header = "Players";
            listViewGroup1.Name = "Players";
            this.listPlayers.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listPlayers.HideSelection = false;
            this.listPlayers.LargeImageList = this.playerIcons;
            this.listPlayers.Location = new System.Drawing.Point(12, 44);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(185, 335);
            this.listPlayers.TabIndex = 2;
            this.listPlayers.UseCompatibleStateImageBehavior = false;
            this.listPlayers.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // playerIcons
            // 
            this.playerIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("playerIcons.ImageStream")));
            this.playerIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.playerIcons.Images.SetKeyName(0, "char1.png");
            this.playerIcons.Images.SetKeyName(1, "char2.png");
            this.playerIcons.Images.SetKeyName(2, "char3.png");
            this.playerIcons.Images.SetKeyName(3, "char4.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lobby name";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(410, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 103);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(891, 514);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(197, 20);
            this.txtChat.TabIndex = 7;
            this.txtChat.Visible = false;
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(979, 540);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(103, 38);
            this.btnGuess.TabIndex = 8;
            this.btnGuess.Text = "Send";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Visible = false;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DrawMyThing.Properties.Resources.drawmything_logo2;
            this.pictureBox2.Location = new System.Drawing.Point(253, 25);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(530, 285);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 565);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.Red;
            this.btnRed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRed.ForeColor = System.Drawing.Color.Red;
            this.btnRed.Location = new System.Drawing.Point(68, 21);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(25, 22);
            this.btnRed.TabIndex = 13;
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // brushSize
            // 
            this.brushSize.LargeChange = 10;
            this.brushSize.Location = new System.Drawing.Point(6, 71);
            this.brushSize.Maximum = 100;
            this.brushSize.Minimum = 1;
            this.brushSize.Name = "brushSize";
            this.brushSize.Size = new System.Drawing.Size(104, 45);
            this.brushSize.SmallChange = 10;
            this.brushSize.TabIndex = 14;
            this.brushSize.TickFrequency = 10;
            this.brushSize.Value = 1;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.ImageIndex = 0;
            this.btnClear.ImageList = this.imageList1;
            this.btnClear.Location = new System.Drawing.Point(146, 67);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 50);
            this.btnClear.TabIndex = 15;
            this.btnClear.UseMnemonic = false;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "clearIcon.png");
            this.imageList1.Images.SetKeyName(1, "s46x8.png");
            // 
            // lblWordToGuess
            // 
            this.lblWordToGuess.AutoSize = true;
            this.lblWordToGuess.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWordToGuess.Location = new System.Drawing.Point(405, 540);
            this.lblWordToGuess.Name = "lblWordToGuess";
            this.lblWordToGuess.Size = new System.Drawing.Size(217, 27);
            this.lblWordToGuess.TabIndex = 16;
            this.lblWordToGuess.Text = "The guessing word";
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnYellow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYellow.ForeColor = System.Drawing.Color.Red;
            this.btnYellow.Location = new System.Drawing.Point(99, 21);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(25, 22);
            this.btnYellow.TabIndex = 13;
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.Blue;
            this.btnBlue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlue.ForeColor = System.Drawing.Color.Red;
            this.btnBlue.Location = new System.Drawing.Point(37, 21);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(25, 22);
            this.btnBlue.TabIndex = 13;
            this.btnBlue.UseVisualStyleBackColor = false;
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.BackColor = System.Drawing.Color.Black;
            this.btnBlack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBlack.ForeColor = System.Drawing.Color.Red;
            this.btnBlack.Location = new System.Drawing.Point(6, 21);
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(25, 22);
            this.btnBlack.TabIndex = 13;
            this.btnBlack.UseVisualStyleBackColor = false;
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnColors
            // 
            this.btnColors.BackColor = System.Drawing.Color.White;
            this.btnColors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColors.ForeColor = System.Drawing.Color.Black;
            this.btnColors.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnColors.ImageIndex = 1;
            this.btnColors.ImageList = this.imageList1;
            this.btnColors.Location = new System.Drawing.Point(130, 15);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(40, 40);
            this.btnColors.TabIndex = 13;
            this.btnColors.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnColors.UseVisualStyleBackColor = false;
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRed);
            this.groupBox1.Controls.Add(this.btnYellow);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnBlue);
            this.groupBox1.Controls.Add(this.brushSize);
            this.groupBox1.Controls.Add(this.btnBlack);
            this.groupBox1.Controls.Add(this.btnColors);
            this.groupBox1.Location = new System.Drawing.Point(880, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 123);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(883, 139);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtLog.Size = new System.Drawing.Size(205, 358);
            this.txtLog.TabIndex = 18;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged_1);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(7, 425);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(119, 29);
            this.lblTime.TabIndex = 19;
            this.lblTime.Text = "Time left: ";
            // 
            // lblDrawerName
            // 
            this.lblDrawerName.AutoSize = true;
            this.lblDrawerName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrawerName.Location = new System.Drawing.Point(491, 31);
            this.lblDrawerName.Name = "lblDrawerName";
            this.lblDrawerName.Size = new System.Drawing.Size(0, 27);
            this.lblDrawerName.TabIndex = 20;
            this.lblDrawerName.Visible = false;
            // 
            // gbChangeRound
            // 
            this.gbChangeRound.Controls.Add(this.lblChangeRound);
            this.gbChangeRound.Location = new System.Drawing.Point(359, 210);
            this.gbChangeRound.Name = "gbChangeRound";
            this.gbChangeRound.Size = new System.Drawing.Size(351, 100);
            this.gbChangeRound.TabIndex = 21;
            this.gbChangeRound.TabStop = false;
            this.gbChangeRound.Text = "Round";
            this.gbChangeRound.Visible = false;
            // 
            // lblChangeRound
            // 
            this.lblChangeRound.AutoSize = true;
            this.lblChangeRound.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeRound.Location = new System.Drawing.Point(38, 41);
            this.lblChangeRound.Name = "lblChangeRound";
            this.lblChangeRound.Size = new System.Drawing.Size(274, 27);
            this.lblChangeRound.TabIndex = 0;
            this.lblChangeRound.Text = "Prepare For Next Round";
            this.lblChangeRound.Visible = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerPing
            // 
            this.timerPing.Interval = 5000;
            this.timerPing.Tick += new System.EventHandler(this.timerPing_Tick);
            // 
            // Lobby
            // 
            this.AcceptButton = this.btnGuess;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 590);
            this.Controls.Add(this.gbChangeRound);
            this.Controls.Add(this.lblDrawerName);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblWordToGuess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGuess);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listPlayers);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Lobby_FormClosed);
            this.Load += new System.EventHandler(this.Lobby_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Lobby_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brushSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbChangeRound.ResumeLayout(false);
            this.gbChangeRound.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listPlayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnGuess;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.TrackBar brushSize;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblWordToGuess;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnBlack;
        private System.Windows.Forms.Button btnColors;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ImageList playerIcons;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDrawerName;
        private System.Windows.Forms.GroupBox gbChangeRound;
        private System.Windows.Forms.Label lblChangeRound;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerPing;
    }
}