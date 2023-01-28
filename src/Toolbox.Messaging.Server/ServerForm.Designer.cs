namespace Toolbox.Messaging.Server
{
    partial class ServerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxTcp = new System.Windows.Forms.TextBox();
            this.textBoxUdp = new System.Windows.Forms.TextBox();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 2;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.layoutPanel.Controls.Add(this.textBoxTcp, 0, 0);
            this.layoutPanel.Controls.Add(this.textBoxUdp, 0, 1);
            this.layoutPanel.Controls.Add(this.textBoxMessages, 0, 2);
            this.layoutPanel.Controls.Add(this.buttonStart, 1, 0);
            this.layoutPanel.Controls.Add(this.buttonStop, 1, 1);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(898, 447);
            this.layoutPanel.TabIndex = 0;
            // 
            // textBoxTcp
            // 
            this.textBoxTcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTcp.Location = new System.Drawing.Point(3, 3);
            this.textBoxTcp.Name = "textBoxTcp";
            this.textBoxTcp.ReadOnly = true;
            this.textBoxTcp.Size = new System.Drawing.Size(762, 27);
            this.textBoxTcp.TabIndex = 0;
            // 
            // textBoxUdp
            // 
            this.textBoxUdp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUdp.Location = new System.Drawing.Point(3, 37);
            this.textBoxUdp.Name = "textBoxUdp";
            this.textBoxUdp.ReadOnly = true;
            this.textBoxUdp.Size = new System.Drawing.Size(762, 27);
            this.textBoxUdp.TabIndex = 1;
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessages.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMessages.Location = new System.Drawing.Point(3, 71);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessages.Size = new System.Drawing.Size(762, 373);
            this.textBoxMessages.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStart.Location = new System.Drawing.Point(771, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(124, 28);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // buttonStop
            // 
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStop.Location = new System.Drawing.Point(771, 37);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(124, 28);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 447);
            this.Controls.Add(this.layoutPanel);
            this.Name = "ServerForm";
            this.Text = "Messaging Server";
            this.Load += new System.EventHandler(this.ServerFormLoad);
            this.Shown += new System.EventHandler(this.ServerFormShown);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel layoutPanel;
        private TextBox textBoxTcp;
        private TextBox textBoxUdp;
        private TextBox textBoxMessages;
        private Button buttonStart;
        private Button buttonStop;
    }
}