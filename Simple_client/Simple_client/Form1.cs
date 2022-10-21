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

namespace Simple_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] bytes = new byte[1024];
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                // Create a TCP/IP  socket.  
                Socket senders = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    senders.Connect(remoteEP);

                    listBox1.Items.Add("Socket connected to {0}"+senders.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("richesta un numero randominco;");

                    // Send the data through the socket.  
                    int bytesSent = senders.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = senders.Receive(bytes);
                    listBox1.Items.Add("Echoed test = {0}"+"    "+
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.  
                    senders.Shutdown(SocketShutdown.Both);
                    senders.Close();
                    

                }
                catch (ArgumentNullException ane)
                {
                    listBox1.Items.Add("ArgumentNullException : {0}"+ ane.ToString());
                }
                catch (SocketException se)
                {
                    listBox1.Items.Add("SocketException : {0}"+ se.ToString());
                }
                catch (Exception ed)
                {
                    listBox1.Items.Add("Unexpected exception : {0}"+ed.ToString());
                }

            }
            catch (Exception ed)
            {
                listBox1.Items.Add(ed.ToString());
            }
           

        }
    }
}
