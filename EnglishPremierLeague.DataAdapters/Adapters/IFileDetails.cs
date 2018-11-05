namespace EnglishPremierLeague.Data.Adapters
{
	public interface IFileDetails
	{
		string FilePath { get; }
		bool ContainsHeader { get; }
	}
}
