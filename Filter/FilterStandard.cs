using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TEST16032021ConsoleApp.Filter
{

	internal class FilterStandard : IFilter
	{
		private const string DefaultPattern = @"(\w{3})\w*\W";
		private readonly string _defaultStringValue;

		internal FilterStandard(string stringValue) => _defaultStringValue = stringValue;
		string IFilter.DefaultStringValue => _defaultStringValue;

		string[] IFilter.GetStringArray()
		{
			Regex regex = new Regex(DefaultPattern, RegexOptions.Compiled & RegexOptions.CultureInvariant);
			MatchCollection matchCollection = regex.Matches(_defaultStringValue);
			List<string> result = new List<string>();
			foreach (Match match in matchCollection)
			{
				result.Add(match.Groups[1].Value);
			}

			return result.ToArray();
		}
	}

}
