using System.Threading.Tasks;

namespace TEST16032021ConsoleApp.Logical
{

	internal interface ILogical
	{
		internal uint CountMaxValue { get; }
		internal Task<LogicalStandard.Node[]> GetResultValueAsync(string[] value);
	}

}
