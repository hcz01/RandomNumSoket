using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace SimpleServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string data = null;
        byte[] bytes = new Byte[1024];

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);
            Socket listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    listBox1.Items.Add("Waiting for a connection...");
                   
                    
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf(";") > -1)
                        {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    MessageBox.Show("richesta ricevuto :"+data);
                    Random rnd = new Random();
                    float random = rnd.Next();
                    listBox1.Items.Add("crea un numero randomico " + random);
                    
                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(random.ToString());

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception ed)
            {
                MessageBox.Show(ed.ToString());
               
            }

        }
    }
}
