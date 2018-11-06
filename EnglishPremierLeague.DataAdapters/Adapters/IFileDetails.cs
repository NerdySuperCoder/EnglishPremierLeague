namespace EnglishPremierLeague.Data.Adapters
{
	public interface IFileDetails
	{
		#region IFileDetails Methods
		string FilePath { get; }
		bool ContainsHeader { get; } 
		#endregion
	}
}
