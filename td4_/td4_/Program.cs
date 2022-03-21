using System;
using td4_.Calculator;

namespace td4_
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			CalculatorSoapClient client = new CalculatorSoapClient("CalculatorSoap");
			Console.WriteLine("1 + 2 = "+client.Add(1,2));
		}
	}
}