using EnglishPremierLeague.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EnglishPremierLeague.DataAdapters
{
	public abstract class DataAdapter : IDataAdapter
	{
		public abstract IEnumerable<Team> GetData(string FilePath, bool containsHeaderRow);
	}
}
