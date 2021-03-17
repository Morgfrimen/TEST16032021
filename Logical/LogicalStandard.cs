using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TEST16032021ConsoleApp.Logical
{

	internal sealed class LogicalStandard : ILogical
	{
		private const uint MaxResultPrint = 10;
		private readonly uint _countMaxValue;
		private object _locker = new object();
		internal LogicalStandard() => _countMaxValue = MaxResultPrint;
		uint ILogical.CountMaxValue => _countMaxValue;

		internal class Node
		{
			internal Node(uint count, string value)
			{
				this.count = count;
				this.value = value;
			}
			internal readonly uint count;
			internal readonly string value;
		}

		Task<Node[]> ILogical.GetResultValueAsync(string[] value)
		{
			 Task<Node[]> resultTask = new Task<Node[]>(new Func<Node[]>(() =>
				{
					IEnumerable<string> distinctValue = value.Distinct();
					List<Node> countMaxItemValue = new List<Node>();
					Task.WaitAll(distinctValue.Select(s => Task.Run(() =>
											{
												uint count = (uint)GetStringsLock(ref value).Count(item => item == s);
												AddItemListLock(countMaxItemValue, new Node(count, s));
											})).ToArray());
					return countMaxItemValue.OrderByDescending(item => item.count).Take((int)MaxResultPrint).ToArray();
				}));
			resultTask.Start();
			return resultTask;
		}

		private string[] GetStringsLock(ref string[] valueStrings)
		{
			lock (valueStrings) return valueStrings;
		}

		private void AddItemListLock<T>(IList<T> list, T value)
		{
			lock (_locker) list.Add(value);
		}
	}

}
