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
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanel.Controls.Add(this.textBoxTcp, 0, 0);
            this.layoutPanel.Controls.Add(this.textBoxUdp, 0, 1);
            this.layoutPanel.Controls.Add(this.textBoxMessages, 0, 2);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(800, 450);
            this.layoutPanel.TabIndex = 0;
            // 
            // textBoxTcp
            // 
            this.textBoxTcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTcp.Location = new System.Drawing.Point(3, 3);
            this.textBoxTcp.Name = "textBoxTcp";
            this.textBoxTcp.ReadOnly = true;
            this.textBoxTcp.Size = new System.Drawing.Size(794, 27);
            this.textBoxTcp.TabIndex = 0;
            // 
            // textBoxUdp
            // 
            this.textBoxUdp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUdp.Location = new System.Drawing.Point(3, 37);
            this.textBoxUdp.Name = "textBoxUdp";
            this.textBoxUdp.ReadOnly = true;
            this.textBoxUdp.Size = new System.Drawing.Size(794, 27);
            this.textBoxUdp.TabIndex = 1;
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessages.Location = new System.Drawing.Point(3, 71);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessages.Size = new System.Drawing.Size(794, 376);
            this.textBoxMessages.TabIndex = 2;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}