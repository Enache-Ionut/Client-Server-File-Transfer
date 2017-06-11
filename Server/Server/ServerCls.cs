using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Server
{
    public class ServerCls
    {
        public static string path;
        public static string MessageCurrent = "Stopped";

        public void StartServer()
        {
            try
            {
                MessageCurrent = "Starting...";
                
                string ip = "192.168.43.246";
                IPAddress ipAd = IPAddress.Parse(ip);

                TcpListener tcpListener = new TcpListener(ipAd, 8001);
                tcpListener.Start();

                MessageCurrent = "Functioneaza si asteapta pt fisiere";
                Socket socket = tcpListener.AcceptSocket();

                byte[] clientData = new byte[1024 * 5000];
                int receivedByteLen = socket.Receive(clientData);

                MessageCurrent = "Se primeste fisier...";

                int fNameLen = BitConverter.ToInt32(clientData, 0);
                string fName = Encoding.ASCII.GetString(clientData, 4, fNameLen);
                BinaryWriter write = new BinaryWriter(File.Open(path + "/" + fName, FileMode.Append));
                write.Write(clientData, 4 + fNameLen, receivedByteLen - 4 - fNameLen);
                MessageCurrent = "Saving file....";

                write.Close();

                MessageCurrent = "Fisierul a fost primit";

                Process compiler = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fName,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                compiler.Start();

                string outputCompiler = string.Empty;
                while (!compiler.StandardOutput.EndOfStream)
                {
                    outputCompiler += compiler.StandardOutput.ReadLine();
                }
                outputCompiler = outputCompiler.Trim();

                ASCIIEncoding asen = new ASCIIEncoding();
                socket.Send(asen.GetBytes(outputCompiler));

                socket.Close();
                tcpListener.Stop();
            }
            catch
            {
                MessageCurrent = "Eroare, fisierul nu a fost primit";
            }
        }
    }
}
