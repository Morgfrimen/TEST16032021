using System;
using System.IO;

namespace TEST16032021CinsoleApp.Reader
{

	internal sealed class ReaderFormatTxt : IReader
	{
		private readonly string _pathFolderTextFile;
		internal ReaderFormatTxt(string pathFolderTextFile) => _pathFolderTextFile = pathFolderTextFile;

		public string GetString()
		{
			string value = String.Empty;
			using (StreamReader streamReader = new StreamReader(_pathFolderTextFile)) value = streamReader.ReadToEnd();
			return value;
		}
	}

}
