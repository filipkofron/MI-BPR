using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SharedClock
{
  class Server
  {
    private Socket socket;
    private Thread serverThread;
    public Server()
    {
      var localEndPoint = new IPEndPoint(IPAddress.Any, Program.Port);

      socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socket.Bind(localEndPoint);
      socket.Listen(16);
    }

    private void SetDateTime(string pattern, string dateTime)
    {
      DateTime dt = new DateTime();
      DateTime.TryParseExact(dateTime, pattern, null, DateTimeStyles.None, out dt);
      
    }

    private void Serve(object clientObject)
    {
      try
      {
        var client = (Socket)clientObject;

        using (StreamReader reader = new StreamReader(new NetworkStream(client), Encoding.UTF8))
        {
          string line;
          while ((line = reader.ReadLine()) != null)
          {
            client.Send(Encoding.ASCII.GetBytes(DateTime.Now.ToString(line) + "\r\n"));
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            break;
          }
        }
        
      }
      catch (Exception)
      {
      }
    }

    private void Loop()
    {
      while (true)
      {
        // Program is suspended while waiting for an incoming connection.
        Socket client = socket.Accept();
        client.ReceiveTimeout = 10000;
        client.SendTimeout = 10000;

        Thread serveThread = new Thread(Serve);
        serveThread.Start(client);
      }
    }

    public void Start()
    {
      serverThread = new Thread(Loop);
      serverThread.Start();
    }
  }
}
