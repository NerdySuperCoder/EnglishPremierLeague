using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.Data.Adapters
{
	public interface IFileDetails
	{
		string FilePath { get; }
		bool ContainsHeader { get; }
	}
}
