namespace TEST16032021ConsoleApp.Logical.Models
{

	internal record Node
	{
		internal readonly int Count;
		internal readonly string Value;

		internal Node(int count, string value)
		{
			Count = count;
			Value = value;
		}
	}

}
