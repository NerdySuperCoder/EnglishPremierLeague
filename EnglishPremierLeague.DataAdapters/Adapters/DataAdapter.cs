using EnglishPremierLeague.Common.Entities;
using System.Collections.Generic;

namespace EnglishPremierLeague.Data.Adapters
{
	public abstract class DataAdapter : IDataAdapter
	{
		#region Abstract methods
		public abstract IEnumerable<Team> GetRepository();
		#endregion
	}
}
