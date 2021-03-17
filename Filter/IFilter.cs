namespace TEST16032021ConsoleApp.Filter
{

	internal interface IFilter
	{
		internal string DefaultStringValue { get; }
		internal string[] GetStringArray();
	}

}
