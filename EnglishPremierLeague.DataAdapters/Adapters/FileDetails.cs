namespace EnglishPremierLeague.Data.Adapters
{
	public class FileDetails : IFileDetails
	{
		#region Constructor
		public FileDetails(string filePath, bool containsHeader)
		{
			FilePath = filePath;
			ContainsHeader = containsHeader;
		}
		#endregion

		#region Public properties
		public string FilePath { get; }
		public bool ContainsHeader { get; } 
		#endregion

	}
}
