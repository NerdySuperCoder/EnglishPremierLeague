using EnglishPremierLeague.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnglishPremierLeague.DataAdapters.DATAdapter
{
	public class DATAdapter : DataAdapter
	{
		public override IEnumerable<Team> GetData(string FilePath, bool containsHeaderRow)
		{
			throw new NotImplementedException();
		}
	}
}
