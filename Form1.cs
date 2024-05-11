using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using SimpleTCP;


//Клиентская часть

namespace OpSis9_2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client; //для взаимодействия с сервером

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            // Подключение клиента к серверу
            client.Connect(txtHost.Text, int.Parse(txtPort.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            //получение данных от сервера
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString;
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (btnConnect.Enabled == false) 
            {
                //отправляет сообщение на сервер и ожидает ответ в течение указанного времени
                client.WriteLineAndGetReply(txtMessage.Text, TimeSpan.FromSeconds(2));
            }
            else
            {
                MessageBox.Show("Сначала подключитесь к серверу.");
            }
        }
    }
}
