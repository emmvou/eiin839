using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMaths
{

	internal class Program
	{
		public static void Main(string[] args)
		{
			MathService.MathsOperationsClient client = new MathService.MathsOperationsClient();
			Console.WriteLine("1 + 2 = "+client.Add(1,2));
			Console.ReadLine();
		}
	}
}
