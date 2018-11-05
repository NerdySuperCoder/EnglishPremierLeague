using EnglishPremierLeague.Common.Entities;
using System.Collections.Generic;

namespace EnglishPremierLeague.Data.Adapters
{
	/// <summary>
	/// Common interface to import data
	/// </summary>
	public interface IDataAdapter
	{
		IEnumerable<Team> GetRepository();
	}
}
