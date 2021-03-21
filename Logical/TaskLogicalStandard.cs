using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TEST16032021ConsoleApp.Logical.Models;
using TEST16032021ConsoleApp.Reader;

namespace TEST16032021ConsoleApp.Logical
{

	internal sealed class TaskLogicalStandard : ILogical<Node>
	{
		private readonly IReader _reader;
		private readonly ConcurrentBag<Node> _listSynchronizedCollection;

		internal TaskLogicalStandard(IReader reader)
		{
			_reader = reader;
			_listSynchronizedCollection = new ConcurrentBag<Node>();
		}

		private void AddRange<T>(ConcurrentBag<T> list, IEnumerable<T> range)
		{
			foreach (T VARIABLE in range) list.Add(VARIABLE);
		}

		private async Task<Node[]> RunLogical(string[] value) => await Task.Run(async () =>
																				{
																					ILogical<Node> logical = new LogicalStandard();
																					return await logical.GetResultValueAsync(value);
																				});

		async Task<Node[]> ILogical<Node>.GetResultValueAsync(string[] value)
		{
			int maxCount = _reader.CountStr.Length / (Environment.ProcessorCount / 2);
			int skip = default;
			for (int index = 0; index < Environment.ProcessorCount / 2; index++)
			{
				Node[] node = await RunLogical(value.Skip(skip).Take(maxCount - 1).ToArray());
				skip += maxCount;
				AddRange(_listSynchronizedCollection, node);
			}

			return _listSynchronizedCollection.GroupBy(item => item.Value).Select(item => new Node(item.Sum(i => i.Count), item.Key)).OrderByDescending(item => item.Count).Take(10).ToArray();
		}
	}

}
