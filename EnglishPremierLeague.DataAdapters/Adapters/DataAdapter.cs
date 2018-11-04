using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EnglishPremierLeague.Data.Adapters
{
	public abstract class DataAdapter : IDataAdapter
	{
		public abstract IEnumerable<Team> GetData(string FilePath, bool containsHeaderRow);
	}
}
