using System.Threading.Tasks;

namespace TEST16032021ConsoleApp.Logical
{

	internal interface ILogical
	{
		internal Task<LogicalStandard.Node[]> GetResultValueAsync(string[] value);
	}

}
