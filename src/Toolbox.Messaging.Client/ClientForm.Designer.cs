namespace Toolbox.Messaging.Client
{
    partial class ClientForm
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
            this.components = new System.ComponentModel.Container();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelServer = new System.Windows.Forms.Label();
            this.contextMenuServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelHello = new System.Windows.Forms.Label();
            this.labelSayHello = new System.Windows.Forms.Label();
            this.buttonUse = new System.Windows.Forms.Button();
            this.buttonPostHello = new System.Windows.Forms.Button();
            this.buttonPostSayHello = new System.Windows.Forms.Button();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxHello = new System.Windows.Forms.TextBox();
            this.textBoxSayHello = new System.Windows.Forms.TextBox();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPostData = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 3;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.layoutPanel.Controls.Add(this.labelServer, 0, 0);
            this.layoutPanel.Controls.Add(this.labelHello, 0, 1);
            this.layoutPanel.Controls.Add(this.labelSayHello, 0, 2);
            this.layoutPanel.Controls.Add(this.buttonUse, 2, 0);
            this.layoutPanel.Controls.Add(this.buttonPostHello, 2, 1);
            this.layoutPanel.Controls.Add(this.buttonPostSayHello, 2, 2);
            this.layoutPanel.Controls.Add(this.textBoxServer, 1, 0);
            this.layoutPanel.Controls.Add(this.textBoxHello, 1, 1);
            this.layoutPanel.Controls.Add(this.textBoxSayHello, 1, 2);
            this.layoutPanel.Controls.Add(this.textBoxMessages, 0, 4);
            this.layoutPanel.Controls.Add(this.label1, 0, 3);
            this.layoutPanel.Controls.Add(this.buttonPostData, 2, 3);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 5;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(891, 337);
            this.layoutPanel.TabIndex = 0;
            // 
            // labelServer
            // 
            this.labelServer.ContextMenuStrip = this.contextMenuServer;
            this.labelServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelServer.Location = new System.Drawing.Point(3, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(114, 34);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server";
            this.labelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuServer
            // 
            this.contextMenuServer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuServer.Name = "contextMenuServer";
            this.contextMenuServer.Size = new System.Drawing.Size(61, 4);
            // 
            // labelHello
            // 
            this.labelHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHello.Location = new System.Drawing.Point(3, 34);
            this.labelHello.Name = "labelHello";
            this.labelHello.Size = new System.Drawing.Size(114, 34);
            this.labelHello.TabIndex = 1;
            this.labelHello.Text = "Hello";
            this.labelHello.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSayHello
            // 
            this.labelSayHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSayHello.Location = new System.Drawing.Point(3, 68);
            this.labelSayHello.Name = "labelSayHello";
            this.labelSayHello.Size = new System.Drawing.Size(114, 34);
            this.labelSayHello.TabIndex = 2;
            this.labelSayHello.Text = "SayHello";
            this.labelSayHello.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonUse
            // 
            this.buttonUse.ContextMenuStrip = this.contextMenuServer;
            this.buttonUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUse.Location = new System.Drawing.Point(774, 3);
            this.buttonUse.Name = "buttonUse";
            this.buttonUse.Size = new System.Drawing.Size(114, 28);
            this.buttonUse.TabIndex = 3;
            this.buttonUse.Text = "Use";
            this.buttonUse.UseVisualStyleBackColor = true;
            this.buttonUse.Click += new System.EventHandler(this.ButtonUseClick);
            // 
            // buttonPostHello
            // 
            this.buttonPostHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPostHello.Enabled = false;
            this.buttonPostHello.Location = new System.Drawing.Point(774, 37);
            this.buttonPostHello.Name = "buttonPostHello";
            this.buttonPostHello.Size = new System.Drawing.Size(114, 28);
            this.buttonPostHello.TabIndex = 4;
            this.buttonPostHello.Text = "Post";
            this.buttonPostHello.UseVisualStyleBackColor = true;
            this.buttonPostHello.Click += new System.EventHandler(this.ButtonPostHelloClick);
            // 
            // buttonPostSayHello
            // 
            this.buttonPostSayHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPostSayHello.Enabled = false;
            this.buttonPostSayHello.Location = new System.Drawing.Point(774, 71);
            this.buttonPostSayHello.Name = "buttonPostSayHello";
            this.buttonPostSayHello.Size = new System.Drawing.Size(114, 28);
            this.buttonPostSayHello.TabIndex = 5;
            this.buttonPostSayHello.Text = "Post";
            this.buttonPostSayHello.UseVisualStyleBackColor = true;
            this.buttonPostSayHello.Click += new System.EventHandler(this.ButtonPostSayHelloClick);
            // 
            // textBoxServer
            // 
            this.textBoxServer.ContextMenuStrip = this.contextMenuServer;
            this.textBoxServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxServer.Location = new System.Drawing.Point(123, 3);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(645, 27);
            this.textBoxServer.TabIndex = 6;
            // 
            // textBoxHello
            // 
            this.textBoxHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHello.Location = new System.Drawing.Point(123, 37);
            this.textBoxHello.Name = "textBoxHello";
            this.textBoxHello.Size = new System.Drawing.Size(645, 27);
            this.textBoxHello.TabIndex = 7;
            this.textBoxHello.Text = "Server";
            // 
            // textBoxSayHello
            // 
            this.textBoxSayHello.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSayHello.Location = new System.Drawing.Point(123, 71);
            this.textBoxSayHello.Name = "textBoxSayHello";
            this.textBoxSayHello.Size = new System.Drawing.Size(645, 27);
            this.textBoxSayHello.TabIndex = 8;
            this.textBoxSayHello.Text = "MyName";
            // 
            // textBoxMessages
            // 
            this.layoutPanel.SetColumnSpan(this.textBoxMessages, 3);
            this.textBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessages.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMessages.Location = new System.Drawing.Point(3, 139);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessages.Size = new System.Drawing.Size(885, 195);
            this.textBoxMessages.TabIndex = 9;
            this.textBoxMessages.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 34);
            this.label1.TabIndex = 10;
            this.label1.Text = "Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonPostData
            // 
            this.buttonPostData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPostData.Enabled = false;
            this.buttonPostData.Location = new System.Drawing.Point(774, 105);
            this.buttonPostData.Name = "buttonPostData";
            this.buttonPostData.Size = new System.Drawing.Size(114, 28);
            this.buttonPostData.TabIndex = 11;
            this.buttonPostData.Text = "Post";
            this.buttonPostData.UseVisualStyleBackColor = true;
            this.buttonPostData.Click += new System.EventHandler(this.ButtonPostDataClick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 337);
            this.Controls.Add(this.layoutPanel);
            this.Name = "ClientForm";
            this.Text = "Messaging Client";
            this.Load += new System.EventHandler(this.ClientFormLoad);
            this.Shown += new System.EventHandler(this.ClientFormShown);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel layoutPanel;
        private Label labelServer;
        private Label labelHello;
        private Label labelSayHello;
        private Button buttonUse;
        private Button buttonPostHello;
        private Button buttonPostSayHello;
        private TextBox textBoxServer;
        private TextBox textBoxHello;
        private TextBox textBoxSayHello;
        private TextBox textBoxMessages;
        private Label label1;
        private Button buttonPostData;
        private ContextMenuStrip contextMenuServer;
    }
}