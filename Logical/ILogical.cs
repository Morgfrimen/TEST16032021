using System.Threading.Tasks;

namespace TEST16032021ConsoleApp.Logical
{

	internal interface ILogical<T> where T : class
	{
		internal Task<T[]> GetResultValueAsync(string[] value);
	}

}
