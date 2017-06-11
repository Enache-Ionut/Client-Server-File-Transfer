using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Server_Client_File_Transfer
{
    class ClientCls
    {
        public static string MessageCurrent = "Idle";

        public static void SendFile(string fName)
        {
            try
            {
                string ip = "192.168.43.246";
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(ip, 8001);

                
                string path = string.Empty;
                fName = fName.Replace("\\", "/");

                while (fName.IndexOf("/") > -1)
                {
                    path += fName.Substring(0, fName.IndexOf("/") + 1);
                    fName = fName.Substring(fName.IndexOf("/") + 1);
                }

                byte[] fNameByte = Encoding.ASCII.GetBytes(fName);
                //if (fNameByte.Length > 850 * 1024)
                //{
                //    MesajCurrent = "Fisieru e mai mare decat 850 kb";
                //    return;
                //}

                MessageCurrent = "Buffering...";
                byte[] fileData = File.ReadAllBytes(path + fName);
                byte[] clientData = new byte[4 + fNameByte.Length + fileData.Length];
                byte[] fNameLen = BitConverter.GetBytes(fNameByte.Length);

                fNameLen.CopyTo(clientData, 0);
                fNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fNameByte.Length);

                MessageCurrent = "Conectare la server...";

                Stream stream = tcpClient.GetStream();

                MessageCurrent = "Fisierul se trimite...";

                stream.Write(clientData, 0, clientData.Length);

                MessageCurrent = "Fisierul a fost trimis..";

                byte[] response = new byte[100];
                int length = stream.Read(response, 0, 100);

                string output = string.Empty;
                for( int i = 0; i<length; ++i)
                {
                    output += Convert.ToChar(response[i]);
                }

                MessageBox.Show(output);

                MessageCurrent = "Deconectare...";
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare : " + ex.Message);
                MessageCurrent = "Eraore transfer";
            }
        }
    }
}
