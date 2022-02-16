using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;

namespace BasicServerHTTPlistener
{
	public class MyMethods
	{
		public string GET(string param1, string param2)
		{
			return "<html><body> Hello " + param1 + " et " + param2 + "</body></html>";
			//return "<html><body> Hello " + String.Join(" et ", paramss.Values) + "</body></html>";
		}

		public string Lancage(string param1)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = "ping.exe"; // Specify exe name.
			start.Arguments = param1; // Specify arguments.
			start.UseShellExecute = false; 
			start.RedirectStandardOutput = true;

			string retVal = "";
			
			using (Process process = Process.Start(start))
			{
				//
				// Read in all the text from the process with the StreamReader.
				//
				using (StreamReader reader = process.StandardOutput)
				{
					retVal += reader.ReadToEnd();
					//Console.WriteLine(result);
					//Console.ReadLine();
				}
			}

			return retVal;
		}

		public string LScript(string param1)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = "python"; // Specify exe name.
			start.Arguments = "../../../LScript.py "+param1; // Specify arguments.
			start.UseShellExecute = false; 
			start.RedirectStandardOutput = true;

			string retVal = "";
			
			using (Process process = Process.Start(start))
			{
				//
				// Read in all the text from the process with the StreamReader.
				//
				using (StreamReader reader = process.StandardOutput)
				{
					retVal += reader.ReadToEnd();
					//Console.WriteLine(result);
					//Console.ReadLine();
				}
			}

			return retVal;
		}

		public string incr(int param1_val)
		{
			return "{ number : " + (param1_val + 1) + " }";
		}
	}
}