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

namespace TcpFileTrans_Client
{
    public partial class Form1 : Form
    {
        private AbstractClient client;
        private BackgroundWorker BkgClientFile;
        private string clientSendFileName;
        private FileStream SaveFileStream;
        private string saveFileName = null;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            client = new AbstractClient();
            client.InitSocket(IPAddress.Parse("127.0.0.1"), 6000);
            client.dltDataCallBack = new AbstractClient.dltDeviceAccpetDataCallBack(DeviceAccpetData);
            BkgClientFile = new BackgroundWorker();
            BkgClientFile.WorkerSupportsCancellation = true;
            BkgClientFile.WorkerReportsProgress = true;
            BkgClientFile.DoWork += BkgClientFile_DoWork;
            BkgClientFile.ProgressChanged += BkgClientFile_ProgressChanged;
        }

        private void BkgClientFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BkgClientFile_DoWork(object sender, DoWorkEventArgs e)
        {
            // throw new NotImplementedException();
            string command = e.Argument as string;
            if (command == "SendFileInfo")
            {
                FileStream fs = new FileStream(clientSendFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                string fileInfo = "FileInfo" + "@" + fs.Name.Substring(fs.Name.LastIndexOf("\\") + 1) + "|" + fs.Length;
                client.SendData(fileInfo);
                fs.Close();
            }
            if (command == "SendFile")
            {
                int maxBufferLenght = 1024;
                try
                {
                    FileStream fs = new FileStream(clientSendFileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    long filelength = fs.Length;
                    long leftlength = filelength;//未读取部分
                    int readlength = 0;//已读取部分
                    byte[] buffer = new byte[1024];
                    if (filelength <= maxBufferLenght)
                    {
                        fs.Read(buffer, 0, (int)filelength);
                        byte[] bytes = System.Text.ASCIIEncoding.Default.GetBytes("Accpet|");
                        byte[] sendbyte = new byte[bytes.Length + filelength];
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            sendbyte[i] = bytes[i];
                        }
                        for (int i = bytes.Length; i < sendbyte.Length; i++)
                        {
                            sendbyte[i] = buffer[i - 7];
                        }
                        //   string sendData = "Accpet|" + System.Text.ASCIIEncoding.Default.GetString(buffer);
                        client.SendData(sendbyte);
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
                            //  string sendData = "Accpet|" + System.Text.ASCIIEncoding.Default.GetString(buffer);
                            //byte[] bytes = System.Text.ASCIIEncoding.Default.GetBytes("Accpet|");
                            //byte[] sendbyte = new byte[bytes.Length + readlength];
                            //for (int i = 0; i < bytes.Length; i++)
                            //{
                            //    sendbyte[i] = bytes[i];
                            //}
                            //for (int i = bytes.Length; i < sendbyte.Length; i++)
                            //{
                            //    sendbyte[i] = buffer[i - 7];
                            //}
                            client.SendData(buffer);
                            float per = (float)((((float)filelength - (float)leftlength) / (float)filelength) * 100);
                            BkgClientFile.ReportProgress((int)per, (float)(leftlength / filelength));
                            leftlength -= readlength;
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    fs.Flush();
                    fs.Close();
                    client.SendData("SendOver@");
                }
                catch (Exception ex) { }
            }
        }

        public void ShowInfo(string str)
        {
            this.Invoke(new Action(() => { this.richTextBox1.AppendText(str); }));
        }

        public void DeviceAccpetData(int len, byte[] buffer)
        {
            string command = System.Text.ASCIIEncoding.Default.GetString(buffer, 0, len);
            string[] str = null;
            if (command.Contains("@"))
            {
                str = command.Split('@');

                if (str[0] == "FileInfo")
                {
                    string[] FileInfo = str[1].Split('|');
                    ShowInfo("服务端请求发送文件：" + FileInfo[0] + "，文件大小：" + FileInfo[1] + "Byte" + DateTime.Now);
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
                                SaveFileStream = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                                client.SendData("AccpetFile");
                            }
                        }));
                    }
                }
                if (str[0] == "SendOver")
                {
                    SaveFileStream.Flush();
                    SaveFileStream.Close();
                    MessageBox.Show("文件接收完毕");
                }
            }
            else
            {
                if (command == "AccpetFile")
                {
                    //if (sf.ShowDialog() == DialogResult.OK)
                    // {
                    BkgClientFile.RunWorkerAsync("SendFile");
                    // }
                }
                else
                {
                    SaveFileStream.Write(buffer, 0, len);
                    SaveFileStream.Flush();
                }
            }
        }

        private void btn_serveropen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                clientSendFileName = dialog.FileName;
                if (MessageBox.Show("是否传输文件？", null, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BkgClientFile.RunWorkerAsync("SendFileInfo");
                }
            }
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            client.StartSocket();
        }
    }
}
