using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using static Privileges.Privileges;

namespace SharedClock
{
  static class Program
  {
    private static int port = -1;
    static void ReadConfig()
    {
      XmlDocument doc = new XmlDocument();
      string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      doc.Load($"{path}\\SharedClock\\SharedClock.cfg");
      XmlNode node = doc.DocumentElement.SelectSingleNode("/port");
      if (node == null)
      {
        throw new ArgumentException("Invalid configuration.");
      }
      else
      {
        port = int.Parse(node.InnerText);
      }
    }
    public static int Port
    {
      get { return port; }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      GiveUpAllExceptServer();
      ReadConfig();
      Server server = new Server();
      server.Start();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Clock());
    }
  }
}
