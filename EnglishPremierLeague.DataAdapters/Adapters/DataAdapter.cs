using EnglishPremierLeague.Common.Entities;
using System.Collections.Generic;

namespace EnglishPremierLeague.Data.Adapters
{
	public abstract class DataAdapter : IDataAdapter
	{
		public abstract IEnumerable<Team> GetRepository();
	}
}
