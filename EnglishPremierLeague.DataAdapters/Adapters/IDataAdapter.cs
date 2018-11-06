using EnglishPremierLeague.Common.Entities;
using System.Collections.Generic;

namespace EnglishPremierLeague.Data.Adapters
{
	#region IDataAdapter Methods
	/// <summary>
	/// Common interface to import data
	/// </summary>
	public interface IDataAdapter
	{
		IEnumerable<Team> GetRepository();
	} 
	#endregion
}
