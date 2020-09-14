namespace SCServer
{
    partial class SimConnectServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimConnectServer));
            this.button1 = new System.Windows.Forms.Button();
            this.pulse = new System.Windows.Forms.Timer(this.components);
            this.trayIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.hideButton = new System.Windows.Forms.Button();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.debugMode = new System.Windows.Forms.CheckBox();
            this.debugOutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.targetIp = new System.Windows.Forms.TextBox();
            this.reconbutton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pulse
            // 
            this.pulse.Enabled = true;
            this.pulse.Interval = 66;
            this.pulse.Tick += new System.EventHandler(this.pulse_Tick);
            // 
            // trayIco
            // 
            this.trayIco.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIco.Icon")));
            this.trayIco.Text = "STKP SimConnectServer";
            this.trayIco.Visible = true;
            this.trayIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIco_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(379, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "This application is used by SimToolkitPro to get information from your simulator." +
    " Minimising it will send it to the notification tray.";
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(287, 116);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(47, 23);
            this.hideButton.TabIndex = 2;
            this.hideButton.Text = "Hide";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.DarkRed;
            this.statusPanel.Location = new System.Drawing.Point(111, 120);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(15, 16);
            this.statusPanel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sim Connection Status";
            // 
            // debugMode
            // 
            this.debugMode.AutoSize = true;
            this.debugMode.Location = new System.Drawing.Point(12, 120);
            this.debugMode.Name = "debugMode";
            this.debugMode.Size = new System.Drawing.Size(93, 17);
            this.debugMode.TabIndex = 5;
            this.debugMode.Text = "Debug Output";
            this.debugMode.UseVisualStyleBackColor = true;
            this.debugMode.CheckedChanged += new System.EventHandler(this.debugMode_CheckedChanged);
            this.debugMode.CheckStateChanged += new System.EventHandler(this.debugMode_CheckStateChanged);
            // 
            // debugOutput
            // 
            this.debugOutput.Location = new System.Drawing.Point(12, 148);
            this.debugOutput.Multiline = true;
            this.debugOutput.Name = "debugOutput";
            this.debugOutput.ReadOnly = true;
            this.debugOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.debugOutput.Size = new System.Drawing.Size(376, 164);
            this.debugOutput.TabIndex = 6;
            this.debugOutput.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Broadcast Target";
            // 
            // targetIp
            // 
            this.targetIp.Location = new System.Drawing.Point(104, 87);
            this.targetIp.Name = "targetIp";
            this.targetIp.Size = new System.Drawing.Size(183, 20);
            this.targetIp.TabIndex = 9;
            this.targetIp.Text = "255.255.255.255";
            // 
            // reconbutton
            // 
            this.reconbutton.Location = new System.Drawing.Point(293, 85);
            this.reconbutton.Name = "reconbutton";
            this.reconbutton.Size = new System.Drawing.Size(95, 23);
            this.reconbutton.TabIndex = 10;
            this.reconbutton.Text = "Reconnect";
            this.reconbutton.UseVisualStyleBackColor = true;
            this.reconbutton.Click += new System.EventHandler(this.Reconnect);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 35);
            this.label4.TabIndex = 11;
            this.label4.Text = "Set broadcast target to the IP address of the device running SimToolkitPro if 255" +
    ".255.255.255 doesn\'t work.";
            // 
            // SimConnectServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 324);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reconbutton);
            this.Controls.Add(this.targetIp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.debugOutput);
            this.Controls.Add(this.debugMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SimConnectServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimConnectServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.SimConnectServer_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer pulse;
        private System.Windows.Forms.NotifyIcon trayIco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox debugMode;
        private System.Windows.Forms.TextBox debugOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox targetIp;
        private System.Windows.Forms.Button reconbutton;
        private System.Windows.Forms.Label label4;
    }
}

