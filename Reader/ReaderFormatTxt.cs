using System.IO;

namespace TEST16032021ConsoleApp.Reader
{

	internal sealed class ReaderFormatTxt : IReader
	{
		private readonly string _pathFolderTextFile;
		internal ReaderFormatTxt(string pathFolderTextFile) => _pathFolderTextFile = pathFolderTextFile;

		string IReader.GetString()
		{
			string value;
			using (StreamReader streamReader = new StreamReader(_pathFolderTextFile)) value = streamReader.ReadToEnd();
			return value;
		}
	}

}
