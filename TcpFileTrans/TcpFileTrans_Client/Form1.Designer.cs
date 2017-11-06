namespace TcpFileTrans_Client
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
            this.btn_server = new System.Windows.Forms.Button();
            this.btn_serveropen = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pgb_server = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btn_server
            // 
            this.btn_server.Location = new System.Drawing.Point(12, 12);
            this.btn_server.Name = "btn_server";
            this.btn_server.Size = new System.Drawing.Size(75, 23);
            this.btn_server.TabIndex = 2;
            this.btn_server.Text = "客户端连接";
            this.btn_server.UseVisualStyleBackColor = true;
            this.btn_server.Click += new System.EventHandler(this.btn_server_Click);
            // 
            // btn_serveropen
            // 
            this.btn_serveropen.Location = new System.Drawing.Point(93, 12);
            this.btn_serveropen.Name = "btn_serveropen";
            this.btn_serveropen.Size = new System.Drawing.Size(75, 23);
            this.btn_serveropen.TabIndex = 3;
            this.btn_serveropen.Text = "打开文件";
            this.btn_serveropen.UseVisualStyleBackColor = true;
            this.btn_serveropen.Click += new System.EventHandler(this.btn_serveropen_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 41);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(397, 262);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // pgb_server
            // 
            this.pgb_server.Location = new System.Drawing.Point(12, 309);
            this.pgb_server.Name = "pgb_server";
            this.pgb_server.Size = new System.Drawing.Size(397, 23);
            this.pgb_server.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 340);
            this.Controls.Add(this.pgb_server);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_serveropen);
            this.Controls.Add(this.btn_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_server;
        private System.Windows.Forms.Button btn_serveropen;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ProgressBar pgb_server;
    }
}

