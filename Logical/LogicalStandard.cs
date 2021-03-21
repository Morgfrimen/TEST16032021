using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TEST16032021ConsoleApp.Logical.Models;

namespace TEST16032021ConsoleApp.Logical
{

	internal sealed class LogicalStandard : ILogical<Node>
	{
		private const int MaxResultPrint = 10;

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

		Task<Node[]> ILogical<Node>.GetResultValueAsync(string[] value)
		{
			Task<Node[]> resultTask = new Task<Node[]>(() => GetNodes(value));
			resultTask.Start();
			return resultTask;
		}
	}

}
