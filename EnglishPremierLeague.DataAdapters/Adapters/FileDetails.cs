using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters
{
	public class FileDetails : IFileDetails
	{
		public FileDetails(string filePath, bool containsHeader)
		{
			FilePath = filePath;
			ContainsHeader = containsHeader;
		}

		public string FilePath { get; }
		public bool ContainsHeader { get; }

	}
}
