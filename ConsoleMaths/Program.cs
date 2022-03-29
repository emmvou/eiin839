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
			MathsServices.MathsOperationsClient client = new MathsServices.MathsOperationsClient();
			Console.WriteLine("1 + 2 = "+client.Add(1,2));
			Console.WriteLine("1 - 2 = " + client.Substract(1, 2));
			Console.WriteLine("1 * 2 = " + client.Multiply(1, 2));
			Console.ReadLine();
		}
	}
}
