using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEST16032021ConsoleApp.Logical
{

	internal sealed class LogicalStandard : ILogical
	{
		private const int MaxResultPrint = 10;

		internal class Node
		{
			internal Node(int count, string value)
			{
				this.Count = count;
				this.Value = value;
			}

			internal readonly int Count;
			internal readonly string Value;
		}

		Task<Node[]> ILogical.GetResultValueAsync(string[] value)
		{
			Task<Node[]> resultTask = new Task<Node[]>(() => GetNodes(value));
			resultTask.Start();
			return resultTask;
		}

		private Node[] GetNodes(string[] value)
		{
			try
			{
				List<Node> countMaxItemValue = new List<Node>();
				Task.WaitAll(value.Distinct().Select(s => Task.Run(() =>
																	{
																		int count = value.Count(item => item == s);
																		countMaxItemValue.Add(new Node(count, s));
																	})).ToArray());
				return countMaxItemValue.OrderByDescending(item => item.Count).Take(MaxResultPrint).ToArray();
			}
			catch (Exception exception)
			{
				EventBroker.EventBroker.ExceptionThrow(exception, nameof(LogicalStandard));
				return null;
			}
		}
	}

}
