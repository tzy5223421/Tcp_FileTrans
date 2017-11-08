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
        private AbstractServer server;
        private string serverSendFileName;
        private BackgroundWorker BkgserverSendFile;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            server = new AbstractServer(6000);
            server.dltClientData += new AbstractServer.dltRcvClientData(ServerDataCallBack);
            BkgserverSendFile = new BackgroundWorker();
            BkgserverSendFile.WorkerReportsProgress = true;
            BkgserverSendFile.WorkerSupportsCancellation = true;
            BkgserverSendFile.DoWork += BkgserverSendFile_DoWork;
            BkgserverSendFile.ProgressChanged += BkgserverSendFile_ProgressChanged;
        }

        private void BkgserverSendFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  throw new NotImplementedException();
            float percent = ((float)e.ProgressPercentage);
            this.Invoke(new Action(() => { this.pgb_server.Value = (int)percent; }));
        }

        private void ShowInfo(string str)
        {
            this.Invoke(new Action(() => { this.richTextBox1.AppendText(str); }));
        }

        private void BkgserverSendFile_DoWork(object sender, DoWorkEventArgs e)
        {
            //   throw new NotImplementedException();
            string command = e.Argument as string;
            if (command == "SendFileInfo")
            {
                FileStream fs = new FileStream(serverSendFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                string fileInfo = "FileInfo" + "@" + fs.Name.Substring(fs.Name.LastIndexOf("\\") + 1) + "|" + fs.Length;
                server.SendData(fileInfo);
                fs.Close();
            }
            if (command == "SendFile")
            {
                int maxBufferLenght = 1024;
                try
                {
                    FileStream fs = new FileStream(serverSendFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    long filelength = fs.Length;
                    long leftlength = filelength;//未读取部分
                    int readlength = 0;//已读取部分
                    byte[] buffer = new byte[1024];
                    if (filelength <= maxBufferLenght)
                    {
                        fs.Read(buffer, 0, (int)filelength);
                        byte[] bytes = System.Text.Encoding.Default.GetBytes("Accpet@");
                        byte[] sendbyte = new byte[bytes.Length + filelength];
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            sendbyte[i] = bytes[i];
                        }
                        for (int i = bytes.Length; i < sendbyte.Length; i++)
                        {
                            sendbyte[i] = buffer[i - bytes.Length];
                        }
                        server.SendData(sendbyte);
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
                            //byte[] bytes_Head = System.Text.Encoding.Default.GetBytes("Accpet@");
                            //byte[] bytes_End = System.Text.Encoding.Default.GetBytes("@End");
                            //byte[] sendbyte = new byte[bytes_Head.Length + readlength + bytes_End.Length];
                            //for (int i = 0; i < bytes_Head.Length; i++)
                            //{
                            //    sendbyte[i] = bytes_Head[i];
                            //}
                            //for (int i = bytes_Head.Length; i < sendbyte.Length - bytes_End.Length; i++)
                            //{
                            //    sendbyte[i] = buffer[readlength - (sendbyte.Length - i - bytes_End.Length)];
                            //}
                            //for (int i = bytes_Head.Length + readlength; i < sendbyte.Length; i++)
                            //{
                            //    sendbyte[i] = bytes_End[Math.Abs(sendbyte.Length - i - bytes_End.Length)];
                            //}
                            server.SendData(buffer);
                            float per = (float)((((float)filelength - (float)leftlength) / (float)filelength) * 100);
                            BkgserverSendFile.ReportProgress((int)per, (float)(leftlength / filelength));
                            leftlength -= readlength;
                        }
                    }
                    fs.Flush();
                    fs.Close();
                    server.SendData("@SendOver");
                }
                catch (Exception ex) { }
            }
        }
        string saveFileName = null;
        FileStream saveFileStream = null;
        public void ServerDataCallBack(int len, byte[] buffer)
        {
            string command = System.Text.Encoding.Default.GetString(buffer, 0, len);
            string[] str = null;
            if (command.Contains("@"))
            {
                str = command.Split('@');

                if (str[0] == "FileInfo")
                {
                    string[] FileInfo = str[1].Split('|');
                    ShowInfo("客户端请求发送文件：" + FileInfo[0] + "，文件大小：" + FileInfo[1] + "Byte" + DateTime.Now);
                    saveFileName = FileInfo[0];
                    if (MessageBox.Show("是否接收文件?", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //  server.SendData("AccpetFile");
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.FileName = saveFileName;
                        sf.Filter = "All files(*.*)|*.*";
                        sf.AddExtension = true;
                        this.Invoke(new Action(() =>
                        {
                            DialogResult result = sf.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                saveFileStream = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                                server.SendData("AccpetFile");
                            }
                        }));
                    }
                }
                if (command.Contains("SendOver"))
                {
                    int strlength = str.Length;
                    if (strlength == 1)
                    {
                        saveFileStream.Flush();
                        saveFileStream.Close();
                        MessageBox.Show("文件接收完毕");
                    }
                    else if (str[strlength - 1] == "SendOver")
                    {
                        string data = command.Substring(0, command.Length - 9);
                        byte[] filedata = System.Text.Encoding.Default.GetBytes(data);
                        saveFileStream.Write(filedata, 0, filedata.Length);
                        saveFileStream.Flush();
                        saveFileStream.Close();
                        MessageBox.Show("文件接收完毕");
                    }
                }
                if (str[0] == "Accpet" && str[str.Length - 1] == "End")
                {
                    byte[] Command_Head = System.Text.Encoding.Default.GetBytes(str[0] + "@");
                    byte[] Command_End = System.Text.Encoding.Default.GetBytes("@" + str[str.Length - 1]);
                    saveFileStream.Write(buffer, Command_Head.Length, len - Command_End.Length);
                }
            }
            else
            {
                if (command == "AccpetFile")
                {
                    //if (sf.ShowDialog() == DialogResult.OK)
                    // {
                    BkgserverSendFile.RunWorkerAsync("SendFile");
                    // }
                }
                else
                {
                    saveFileStream.Write(buffer, 0, len);
                }

            }
            //BkgserverSendFile.RunWorkerAsync(command);
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            server.StartSocket();
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
                if (MessageBox.Show("是否传输文件？", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BkgserverSendFile.RunWorkerAsync("SendFileInfo");
                }
            }
        }
    }
}
