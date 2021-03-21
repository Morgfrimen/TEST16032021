using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using TEST16032021ConsoleApp.Filter;
using TEST16032021ConsoleApp.Logical;
using TEST16032021ConsoleApp.Logical.Models;
using TEST16032021ConsoleApp.Reader;

namespace TEST16032021ConsoleApp
{

	internal class Program
	{
		private static readonly int _defaultEncoding = 1251;
		private static readonly Stopwatch Stopwatch = new Stopwatch();

		static Program()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Console.InputEncoding = Encoding.GetEncoding(_defaultEncoding);
			Console.OutputEncoding = Encoding.GetEncoding(_defaultEncoding);
			EventBroker.EventBroker.PrintExceptionMessage += EventBroker_PrintExceptionMessage;
		}

		private static void EventBroker_PrintExceptionMessage(Exception obj, string tag)
		{
			Console.WriteLine($"{tag}{Environment.NewLine}{obj.Message}");
			Console.ReadKey();
		}

		private static async Task Main(string[] args)
		{
			Stopwatch.Start();
		#if DEBUG
			string patch = Path.Combine(Environment.CurrentDirectory, "Test2.txt");
		#else
			Console.WriteLine("Введите путь к текстовому файлу:");
			string patch = Console.ReadLine();
		#endif
			Console.WriteLine("Старая реализация метода (медленная");
			IReader reader = new ReaderFormatTxt(patch);
			IFilter filter = new FilterStandard(reader.GetString());
			ILogical<Node> logical = new LogicalStandard();
			Node[] res = default;
			res = await logical.GetResultValueAsync(filter.GetStringArray());
			PrintResult(res, Stopwatch);
			reader = null;
			filter = null;
			logical = null;
			GC.Collect();
			Console.WriteLine("Новая реализация (быстрее)");
			Stopwatch.Restart();
			reader = new ReaderFormatTxt(patch);
			filter = new FilterStandard(reader.GetString());
			logical = new TaskLogicalStandard(reader);
			res = await logical.GetResultValueAsync(filter.GetStringArray());
			PrintResult(res, Stopwatch);
			Console.WriteLine("Нажмите любую кнопку для закрытия приложения");
			Console.ReadKey();
		}

		private static void PrintResult(Node[] nodes, Stopwatch stopwatch)
		{
			try
			{
				foreach (Node node in nodes) Console.Write($"{node.Value}, ");
			}
			catch (Exception exception)
			{
				EventBroker.EventBroker.ExceptionThrow(exception, nameof(Program));
			}

			stopwatch.Stop();
			Console.WriteLine(Environment.NewLine + $"Время: {stopwatch.Elapsed:g}");
		}
	}

}
