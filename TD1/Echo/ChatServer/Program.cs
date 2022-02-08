using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Echo
{
	class EchoServer
	{
		[Obsolete]
		static void Main(string[] args)
		{
			Console.CancelKeyPress += delegate { System.Environment.Exit(0); };

			TcpListener ServerSocket = new TcpListener(5000);
			ServerSocket.Start();

			Console.WriteLine("Server started.");
			while (true)
			{
				TcpClient clientSocket = ServerSocket.AcceptTcpClient();
				handleClient client = new handleClient();
				client.startClient(clientSocket);
			}
		}
	}

	public class handleClient
	{
		TcpClient clientSocket;

		public void startClient(TcpClient inClientSocket)
		{
			this.clientSocket = inClientSocket;
			Thread ctThread = new Thread(Echo);
			ctThread.Start();
		}


		private void Echo()
		{
			NetworkStream stream = clientSocket.GetStream();
			var reader = new StreamReader(stream);
			var writer = new StreamWriter(stream);

			string str = reader.ReadLine();
			var strArray = str.Split(' ');
			var filename = strArray[1];
			if (filename[0] == '/')
			{
				filename = filename.Substring(1);
			}

			filename = Path.Combine(Environment.GetEnvironmentVariable("HTTP_ROOT") ?? "root", filename);
			if (Directory.Exists(filename))
			{
				filename += "/index.html";
			}
			if (strArray[0] == "GET")
			{
				writer.WriteLine("HTTP/1.1 200 OK");
				writer.WriteLine("Content-Type: text/html");
				writer.WriteLine("");
				writer.Flush();
				File.OpenRead(filename).CopyTo(stream);
			}
		}
	}
}