using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using TEST16032021ConsoleApp.Filter;
using TEST16032021ConsoleApp.Logical;
using TEST16032021ConsoleApp.Reader;

namespace TEST16032021ConsoleApp
{

	internal class Program
	{
		static Program()
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.UTF8;
		}
		private static async Task Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IReader reader = new ReaderFormatTxt(Path.Combine(Environment.CurrentDirectory, "Test2.txt"));
			IFilter filter = new FilterStandard(reader.GetString());
			string[] result = filter.GetStringArray();
			ILogical logical = new LogicalStandard();
			var res = await logical.GetResultValueAsync(result);
			PrintResult(res, stopwatch);
		}

		private static void PrintResult(LogicalStandard.Node[] nodes, Stopwatch stopwatch)
		{
			foreach (LogicalStandard.Node node in nodes)
			{
				Console.Write($"{node.value}, ");
			}

			stopwatch.Stop();
			Console.WriteLine(Environment.NewLine + $"Время: {stopwatch.Elapsed:g}");
		}
	}

}
