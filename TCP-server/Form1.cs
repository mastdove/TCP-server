using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
           /*textBox3.Invoke((MethodInvoker)delegate () {
                textBox3.Text += e.MessageString;
               e.Reply(string.Format("You said: {0}" ,  e.MessageString));
            }
            );*/

            //Корень сервера
            
            textBox3.Invoke((MethodInvoker)delegate () {
                textBox3.Text += e.MessageString;
                string g = e.MessageString;
                g = g.Substring(0, g.Length - 1);
                string gh = "", d = "";
                DirectoryInfo dir = new DirectoryInfo(g);
                foreach (var item in dir.GetDirectories())
                {
                    gh += item.Name + " ";
                    foreach (var it in item.GetDirectories())
                        d += it.Name + "";
                    
                }


                e.Reply(string.Format(gh + d, e.MessageString ));
            }
            );
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text += "Server starting ";
            System.Net.IPAddress ip =  System.Net.IPAddress.Parse(textBox1.Text);
            server.Start(ip, Convert.ToInt32(textBox2.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }
}
