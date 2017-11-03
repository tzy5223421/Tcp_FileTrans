namespace TcpFileTrans
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_serveropen = new System.Windows.Forms.Button();
            this.btn_server = new System.Windows.Forms.Button();
            this.btn_client = new System.Windows.Forms.Button();
            this.btn_clientopen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.pgb_server = new System.Windows.Forms.ProgressBar();
            this.pgb_client = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btn_serveropen
            // 
            this.btn_serveropen.Location = new System.Drawing.Point(84, 12);
            this.btn_serveropen.Name = "btn_serveropen";
            this.btn_serveropen.Size = new System.Drawing.Size(75, 23);
            this.btn_serveropen.TabIndex = 0;
            this.btn_serveropen.Text = "打开文件";
            this.btn_serveropen.UseVisualStyleBackColor = true;
            this.btn_serveropen.Click += new System.EventHandler(this.btn_serveropen_Click);
            // 
            // btn_server
            // 
            this.btn_server.Location = new System.Drawing.Point(3, 12);
            this.btn_server.Name = "btn_server";
            this.btn_server.Size = new System.Drawing.Size(75, 23);
            this.btn_server.TabIndex = 1;
            this.btn_server.Text = "服务器开启";
            this.btn_server.UseVisualStyleBackColor = true;
            this.btn_server.Click += new System.EventHandler(this.btn_server_Click);
            // 
            // btn_client
            // 
            this.btn_client.Location = new System.Drawing.Point(578, 12);
            this.btn_client.Name = "btn_client";
            this.btn_client.Size = new System.Drawing.Size(75, 23);
            this.btn_client.TabIndex = 2;
            this.btn_client.Text = "客户端连接";
            this.btn_client.UseVisualStyleBackColor = true;
            this.btn_client.Click += new System.EventHandler(this.btn_client_Click);
            // 
            // btn_clientopen
            // 
            this.btn_clientopen.Location = new System.Drawing.Point(660, 11);
            this.btn_clientopen.Name = "btn_clientopen";
            this.btn_clientopen.Size = new System.Drawing.Size(75, 23);
            this.btn_clientopen.TabIndex = 3;
            this.btn_clientopen.Text = "打开文件";
            this.btn_clientopen.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "文件地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "文件地址";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 58);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(397, 262);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(578, 58);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(397, 262);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            // 
            // pgb_server
            // 
            this.pgb_server.Location = new System.Drawing.Point(5, 327);
            this.pgb_server.Name = "pgb_server";
            this.pgb_server.Size = new System.Drawing.Size(397, 23);
            this.pgb_server.TabIndex = 6;
            // 
            // pgb_client
            // 
            this.pgb_client.Location = new System.Drawing.Point(578, 327);
            this.pgb_client.Name = "pgb_client";
            this.pgb_client.Size = new System.Drawing.Size(397, 23);
            this.pgb_client.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 362);
            this.Controls.Add(this.pgb_client);
            this.Controls.Add(this.pgb_server);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_clientopen);
            this.Controls.Add(this.btn_client);
            this.Controls.Add(this.btn_server);
            this.Controls.Add(this.btn_serveropen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_serveropen;
        private System.Windows.Forms.Button btn_server;
        private System.Windows.Forms.Button btn_client;
        private System.Windows.Forms.Button btn_clientopen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ProgressBar pgb_server;
        private System.Windows.Forms.ProgressBar pgb_client;
    }
}

