using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tcp_Protocol;
using System.Net;
using System.IO;
using System.Threading;

namespace TcpFileTrans
{
    public partial class Form1 : Form
    {
        private AbstractClient client;
        private AbstractServer server;
        private string clientSendFileName;
        private string serverSendFileName;
        private BackgroundWorker BkgserverSendFile;
        private BackgroundWorker BkgclientSendFile;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            client = new AbstractClient();
            client.InitSocket(IPAddress.Parse("127.0.0.1"), 6000);
            server = new AbstractServer(6000);
            client.dltDataCallBack += new AbstractClient.dltDeviceAccpetDataCallBack(ClientDataCallBack);
            server.dltClientData += new AbstractServer.dltRcvClientData(ServerDataCallBack);
            BkgserverSendFile = new BackgroundWorker();
            BkgserverSendFile.WorkerReportsProgress = true;
            BkgserverSendFile.WorkerSupportsCancellation = true;
            BkgserverSendFile.DoWork += BkgserverSendFile_DoWork;
            BkgserverSendFile.ProgressChanged += BkgserverSendFile_ProgressChanged;

            BkgclientSendFile = new BackgroundWorker();
            BkgclientSendFile.WorkerSupportsCancellation = true;
            BkgclientSendFile.WorkerReportsProgress = true;
            BkgclientSendFile.DoWork += BkgclientSendFile_DoWork;
            BkgclientSendFile.ProgressChanged += BkgclientSendFile_ProgressChanged;
        }

        private void BkgclientSendFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BkgclientSendFile_DoWork(object sender, DoWorkEventArgs e)
        {
            //    throw new NotImplementedException();
        }

        private void BkgserverSendFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  throw new NotImplementedException();
            float percent = ((float)e.ProgressPercentage);
            this.Invoke(new Action(() => { this.pgb_server.Value = (int)percent; }));
        }

        FileStream accpetfs;
        private void BkgserverSendFile_DoWork(object sender, DoWorkEventArgs e)
        {
            //   throw new NotImplementedException();
            int maxBufferLenght = 1024;
            try
            {
                FileStream fs = new FileStream(serverSendFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                accpetfs = new FileStream("H:/GitHubProject/test.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                long filelength = fs.Length;
                long leftlength = filelength;//未读取部分
                int readlength = 0;//已读取部分
                byte[] buffer = new byte[1024];
                if (filelength <= maxBufferLenght)
                {
                    fs.Read(buffer, 0, (int)filelength);
                    server.SendData(buffer);
                }
                else
                {
                    while (leftlength != 0)
                    {
                        if (leftlength < maxBufferLenght)
                        {
                            buffer = new byte[leftlength];
                            readlength = fs.Read(buffer, 0, Convert.ToInt32(leftlength));
                        }
                        else
                        {
                            buffer = new byte[maxBufferLenght];
                            readlength = fs.Read(buffer, 0, maxBufferLenght);
                        }
                        server.SendData(buffer);
                        float per = (float)((((float)filelength - (float)leftlength) / (float)filelength) * 100);
                        BkgserverSendFile.ReportProgress((int)per, (float)(leftlength / filelength));
                        leftlength -= readlength;
                    }
                }
                fs.Flush();
                fs.Close();
                server.SendData("SendOver");

            }
            catch (Exception ex) { }
        }
        private int ClientAccepConunt = 0;
        public void ClientDataCallBack(int len, byte[] buffer)
        {
            try
            {
                if (System.Text.ASCIIEncoding.Default.GetString(buffer, 0, len).Equals("SendOver"))
                {
                    ClientAccepConunt = 0;
                    accpetfs.Close();
                }
                else
                {
                    accpetfs.Write(buffer, 0, len);
                    accpetfs.Flush();
                }
            }
            catch (Exception ex) { }
        }

        public void ServerDataCallBack(int len, byte[] buffer) { }

        private void btn_server_Click(object sender, EventArgs e)
        {
            server.StartSocket();
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            client.StartSocket();
        }

        private void btn_serveropen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                serverSendFileName = dialog.FileName;
            }
            if (MessageBox.Show("是否传输文件？") == DialogResult.OK)
            {
                BkgserverSendFile.RunWorkerAsync();
            }
        }
    }
}
