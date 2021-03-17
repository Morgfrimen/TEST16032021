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
		private static readonly Stopwatch Stopwatch = new Stopwatch();

		static Program()
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.UTF8;
			EventBroker.EventBroker.PrintExceptionMessage += EventBroker_PrintExceptionMessage;
		}

		private static void EventBroker_PrintExceptionMessage(Exception obj,string tag)
		{
			Console.WriteLine($"{tag}{Environment.NewLine}{obj.Message}");
		}

		private static async Task Main(string[] args)
		{
			Stopwatch.Start();
			string patch = Path.Combine(Environment.CurrentDirectory, "Test2.txt");
			IReader reader = new ReaderFormatTxt(patch);
			IFilter filter = new FilterStandard(reader.GetString());
			ILogical logical = new LogicalStandard();
			LogicalStandard.Node[] res = default;
			try
			{
				res = await logical.GetResultValueAsync(filter.GetStringArray());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			PrintResult(res, Stopwatch);
		}

		private static void PrintResult(LogicalStandard.Node[] nodes, Stopwatch stopwatch)
		{
			foreach (LogicalStandard.Node node in nodes) Console.Write($"{node.Value}, ");
			stopwatch.Stop();
			Console.WriteLine(Environment.NewLine + $"Время: {stopwatch.Elapsed:g}");
		}
	}

}
